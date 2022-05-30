using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    public static GridBuilder instance;

    [SerializeField] public Transform[] transformsTurretArray;
    [SerializeField] public Transform selectedTransform;   
    [SerializeField] public Transform enviromentTransform;
    [SerializeField] public Transform[] enviromentTransformArray;

    private Grid<GridObject> grid;

    private bool isPlaceModeOn = false;
    private bool isEnoughGold;
    private bool isSelected;
    private Transform followTransform;
    private bool isBuildPhase = true;
    private int gridWidth = 20;
    private int gridHeight = 20;
    private float cellSize = 10f;

    private void Awake()
    {
        //singelton
        if(instance != null)
        {           
            return;
        }
        instance = this;

        //create/set a grid
        gridWidth = 20;
        gridHeight = 20;
        cellSize = 10f;
        Vector3 origin = new Vector3(0, 0, 0);
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, origin, (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));
        
    }

    private void Start()
    {
        //Initialize the Grid with our Enviroment-Objects
        SetEnviroment(GetEnviromentGridPositionList());      
    }

    public class GridObject
    {
        private Grid<GridObject> grid;
        private int x;
        private int z;
        private Transform transform;      

        public GridObject(Grid<GridObject> grid , int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetTransform(Transform transform)
        {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, z);
        }

        public void CkearTransform()
        {
            transform = null;
        }

        public bool CanBuild()
        {
            return transform == null;
        }
    
    }

    private void Update()
    {
        if(isBuildPhase == false)
        {
            return;
        }

        //if we selected a tower and we are not already placing/hovering the selectet tower around      
        if (isSelected && isPlaceModeOn == false)
        {
            isSelected = false;
            isPlaceModeOn = true;
            //convert worlds mousepostion to our grid coordinates
            grid.GetXZ(Controller.GetMouseWorldPosition(), out int x, out int z);            
            //place the selected tower at our mouseposition
            followTransform = Instantiate(selectedTransform, grid.GetWorldPosition(x, z), Quaternion.identity);
        }

        if (isPlaceModeOn)
        {
            if(followTransform != null)
            {
                //set tower ui
                SelectTower.Show_UI(followTransform.gameObject);

                //move/drag the selected tower towards our mouseposition and smoth it out
                Vector3 targetPosition = Controller.GetMouseWorldPosition();
                float mouseoffset = 5f;
                targetPosition = new Vector3(targetPosition.x - mouseoffset, 1, targetPosition.z - mouseoffset);              
                followTransform.position = Vector3.Lerp(followTransform.position, targetPosition, Time.deltaTime * 30f);
            }
        }

        if (Input.GetMouseButtonDown(0) && isPlaceModeOn)
        {
            //Check if Mouse is outside of the map
            if (!CheckForValidInput(Controller.GetMouseWorldPosition()))
            {
                return;
            }
            //Get the mouse position and convert it to a grid position
            grid.GetXZ(Controller.GetMouseWorldPosition(), out int x, out int z);
            

            //The vectors/cells around, to set them too
            List<Vector2Int> gridPositionList = selectedTransform.GetComponent<TowerStats>().GetGridPositionList(new Vector2Int(x, z));

            //reseting on next click
            bool canBuild = true;

            //Test all position in the list
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    //Cant build
                    canBuild = false;
                    break;
                }
            }

            //Get a gridobject on the current Position
            GridObject gridObject = grid.GetGridObject(x, z);
            //check if a gridobject is already there. if not we can build
            if (canBuild && isEnoughGold)
            {
                //Store the placed object
                Transform builtTransform = Instantiate(selectedTransform, grid.GetWorldPosition(x, z), Quaternion.identity);              

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(builtTransform);
                }
                
                //Set the placed object on our grid
                gridObject.SetTransform(builtTransform);
                //reduce player gold by towercost
                Player.Gold -= selectedTransform.GetComponent<Tower>().buildCost;
            }
            else
            {
                if (isEnoughGold)
                {
                    //Notify the player visually there is no space
                    Debug.Log("Cant Build here!");
                }
                else
                {
                    //Notify the player visually there is not enough gold
                    Debug.Log("Not enough gold!");
                }
                
            }
                       
            isPlaceModeOn = false;
            if(followTransform != null)
            {
                Destroy(followTransform.gameObject);
            }
            
        }
    }

    
    private void SetEnviroment(List<Vector2Int> enviromentGridPositionList)
    {
        foreach (Vector2Int gridPosition in enviromentGridPositionList)
        {
            grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(enviromentTransform);
        }

    }
   
    //Seting up a List. These are the places where we cant build becouse there are already taken
    public List<Vector2Int> GetEnviromentGridPositionList()
    {
        List<Vector2Int> temp = new List<Vector2Int>();

        foreach (Transform enviromentTransform in enviromentTransformArray)
        {
            //convert to grid coordinates
            grid.GetXZ(enviromentTransform.position, out int x, out int z);
            //Add to the list
            if (enviromentTransform.GetComponent<EnviromentStats>().GetGridPositionList(new Vector2Int(x, z)) != null)
            {
                temp.AddRange(enviromentTransform.GetComponent<EnviromentStats>().GetGridPositionList(new Vector2Int(x, z)));
            }

        }

        return temp;
    }
    private bool CheckForValidInput(Vector3 vec)
    {
        bool isValid;
        // we are outside of the grid
        if(vec.y < 0 || vec.x < 0 || vec.z < 0 || vec.y > gridHeight*cellSize || vec.x > gridWidth*cellSize || vec.z > gridHeight*cellSize)
        {
            isValid = false;
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }
   

    public void SetSelectedTower(int buildIndex)
    {
        selectedTransform = transformsTurretArray[buildIndex];
        //Check if the player has enough money
        if(Player.Gold >= selectedTransform.GetComponent<Tower>().buildCost)
        {
            //has enough money
            isEnoughGold = true;
            isSelected = true;
        }
        else
        {
            //Not enough money making sure he cant build it
            isEnoughGold = false;
            isSelected = true;
        }       
    }

    public void SetBuildphase()
    {
        isBuildPhase = !isBuildPhase;
    }
}

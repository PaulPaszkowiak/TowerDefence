using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private bool isGameOver;

    public GameObject ui_GameOver;

    [SerializeField] GameObject[] gameObjectsPrefabArray;

    private Storage[] storage;

    public struct Storage
    {
        public int health { get; set; }
        public float speed { get; set; }
    }

    private void Awake()
    {      
        isGameOver = false;
        storage = new Storage[gameObjectsPrefabArray.Length];
        StateKeeper();
    }

  
    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;

        if(Player.Lives <= 0)
        {
            EndGame();
        }
    }

    //store health and speed from our enemys
    private void StateKeeper()
    {
        for (int i = 0; i < gameObjectsPrefabArray.Length; i++)
        {           
            storage[i].health = gameObjectsPrefabArray[i].GetComponent<Enemy>().health;
            storage[i].speed = gameObjectsPrefabArray[i].GetComponent<Enemy>().speed;
        }
    }
    private void EndGame()
    {
        isGameOver = true;
        ui_GameOver.SetActive(true);
    }

    //Just for in editor testing purpuse
    //restoring the initial states. Other wise the Datachanges on the Prefeabs carry over
    private void RestorePrefebs()
    {
        for (int i = 0; i < storage.Length; i++)
        {
            gameObjectsPrefabArray[i].GetComponent<Enemy>().health = storage[i].health;          
            gameObjectsPrefabArray[i].GetComponent<Enemy>().speed = storage[i].speed;
        }
    }

    public void Restart()
    {
        RestorePrefebs();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApp()
    {
        Application.Quit();
    }


    
}

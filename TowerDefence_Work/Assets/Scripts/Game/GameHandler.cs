using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameHandler : MonoBehaviour
{
    //public static GameHandler instance;

    private bool isGameOver;

    public GameObject ui_GameOver;

    [SerializeField] GameObject[] gameObjectsPrefabArray;

    private Storage[] storage;

    public TMP_Text textGold;
    public TMP_Text textLives;

    public struct Storage
    {
        public int health { get; set; }
        public float speed { get; set; }
    }

    //Subscribe to Events
    private void OnEnable()
    {
        GameEvents.instance.OnPlayerUpdate += UpdatePlayer;
        GameEvents.instance.OnPlayerLiveUpdate += UpdatePlayerLives;
        GameEvents.instance.OnPlayerGoldUpdate += UpdatePlayerGold;
    }

    //Unsubscribe to Events
    private void OnDisable()
    {
        GameEvents.instance.OnPlayerUpdate -= UpdatePlayer;
        GameEvents.instance.OnPlayerLiveUpdate -= UpdatePlayerLives;
        GameEvents.instance.OnPlayerGoldUpdate -= UpdatePlayerGold;
    }

    //caching before start
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
    }

    //Update the player lives and gold
    private void UpdatePlayer(int healthAmount , int goldAmount)
    {
        Player.Lives += healthAmount;
        Player.Gold += goldAmount;
        if (Player.Lives <= 0)
        {
            EndGame();
        }

        PlayerUIUpdate();
    }
    //Update the player gold
    private void UpdatePlayerGold(int goldAmount)
    {        
        Player.Gold += goldAmount;
        GoldUIUpdate();
    }
    //Update the player Lives
    private void UpdatePlayerLives(int healthAmount)
    {       
        Player.Lives += healthAmount;
        if (Player.Lives <= 0)
        {
            EndGame();
        }

        LiveUIUpdate();
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

    //Switch to the GameoverUI
    public void EndGame()
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

    //Reload the sceen with the initial state
    public void Restart()
    {
        RestorePrefebs();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Quit
    public void QuitApp()
    {
        Application.Quit();
    }

    //UI-Update
    public void PlayerUIUpdate()
    {
        textGold.text = Player.Gold.ToString();
        textLives.text = Player.Lives.ToString();
    }
    //UI-Update
    private void LiveUIUpdate()
    {
        textLives.text = Player.Lives.ToString();
    }
    //UI-Update
    private void GoldUIUpdate()
    {
        textGold.text = Player.Gold.ToString();
    }

}

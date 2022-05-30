using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;
    public TMP_Text textWaveDuration;
    public TMP_Text textWaveIndex;
    public GameObject startButton;

    public int waveIndex = 0;
    private int waveCounter = 1;
    public float countdown = 2f;
    public float spawnDuration = 30f;   
    private float spawnRadius = 10f;
    private float editDuration = 0f;

    private bool isPlayerReady = false;
    private float waveTime;

    private void Start()
    {
        waveIndex = 0;
        waveCounter = 1;
        spawnDuration = 30f;
        waveTime = spawnDuration;
        countdown = 2f;      
    }

    private void Update()
    {       
        if (isPlayerReady == false || Player.Lives <= 0)
        {          
            return;
        }

        if(spawnDuration <= 0 && EnemiesAlive <= 0)
        {
            SetBuildphase();          
        }

        if(countdown <= 0f && spawnDuration >=0f)
        {          
            SpawnWave(waveIndex);           
        }

        //reduce time values
        spawnDuration -= Time.deltaTime;
        countdown -= Time.deltaTime;

        //UI-update
        editDuration = Mathf.Clamp(spawnDuration, 0, Mathf.Infinity);
        textWaveDuration.text = string.Format("{0:00.00}",editDuration);
        textWaveIndex.text = "Wave: " + waveCounter.ToString();
       
    }
   
    private void SpawnWave(int waveIndex)
    {
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.size; i++)
        {           
            SpawnEnemy(wave.enemy);
        }

        countdown = wave.rate;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        int height = 5;
        Vector3 centerOfSpawn = spawnPoint.position;
        Vector3 randomSpawn = centerOfSpawn + (Vector3)(spawnRadius * UnityEngine.Random.insideUnitCircle);
        if (enemy.GetComponent<Enemy>().isFlying)
        {
            height = 30;
        }
        else
        {
            height = 5;
        }
        Vector3 withFixedHeight = new Vector3(randomSpawn.x, height, randomSpawn.z);
        Instantiate(enemy,withFixedHeight,spawnPoint.rotation);

        EnemiesAlive++;
    }

    public void SetPlayerIsReady()
    {       
        isPlayerReady = !isPlayerReady;
        startButton.SetActive(false);
        GridBuilder.instance.SetBuildphase();
    }

    public void SetBuildphase()
    {
        //seting up for the next round
        isPlayerReady = !isPlayerReady;
        countdown = 2f;
        spawnDuration = waveTime;
        waveIndex++;
        waveCounter++;

        //loop through our enmys and increase the power
        if (waveIndex >= waves.Length)
        {
            waveIndex = 0;           

            foreach (var wave in waves)
            {
                wave.enemy.GetComponent<Enemy>().IncreaseEnemyStregth(waveCounter);
            }
        }

        GridBuilder.instance.SetBuildphase();
        startButton.SetActive(true);       
    }

}

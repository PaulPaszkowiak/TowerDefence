using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{
    private static SelectTower Instance { get; set; }
    private Vector3 offset = new Vector3(-20, 5, -10);
    private GameObject towerGoTemp;
    private float rangeMult = 0.25f;
    private void Awake()
    {
        Instance = this;
    }

    public static void Show_UI(GameObject gameObjectTower)
    {
        Instance.ShowUI(gameObjectTower);
    }
   
    public void ShowUI(GameObject gameObjectTower)
    {
        towerGoTemp = gameObjectTower;
        gameObject.SetActive(true);
        //UI folow the Object/Tower with a offset.
        transform.position = gameObjectTower.transform.position+offset;
        //set the range
        transform.Find("Range_Sprite").localScale = Vector3.one * towerGoTemp.GetComponent<Tower>().GetRange()* rangeMult;
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void UpgradeRange()
    {
        //call the upgrade range methode
        towerGoTemp.GetComponent<Tower>().UpgradeRange();
        //set the range
        transform.Find("Range_Sprite").localScale = Vector3.one * towerGoTemp.GetComponent<Tower>().GetRange() * rangeMult;
    }

    public void UpgradeDamage()
    {
        //call the tower upgrade dmg methode
        towerGoTemp.GetComponent<Tower>().UpgradeDamage();
    }
}

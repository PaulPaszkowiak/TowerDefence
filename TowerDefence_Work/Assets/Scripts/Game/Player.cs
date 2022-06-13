using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static int Gold;
    public int startGold = 100;

    public static int Lives;
    public int startLives = 10;


    // Start is called before the first frame update
    void Start()
    {
        Gold = startGold;
        Lives = startLives;
    }


}

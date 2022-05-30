using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static int Gold;
    public int startGold = 100;

    public static int Lives;
    public int startLives = 10;

    public TMP_Text textGold;
    public TMP_Text textLives;


    // Start is called before the first frame update
    void Start()
    {
        Gold = startGold;
        Lives = startLives;
    }

    private void Update()
    {
        PlayerUIUpdate();
    }

    private void PlayerUIUpdate()
    {
        textGold.text = Gold.ToString();
        textLives.text = Lives.ToString();
    }
}

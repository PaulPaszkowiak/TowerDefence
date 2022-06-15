using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        
        //singelton
        if (instance != null)
        {
            return;
        }
        
        instance = this;
    }

    //Event for updating player lives and healt and the ui
    public event Action<int,int> OnPlayerUpdate;
    public void PlayerUpdate(int healthAmount, int goldAmount)
    {  
        OnPlayerUpdate?.Invoke(healthAmount,goldAmount);
    }

    //Event for updating player lives and the ui
    public event Action<int> OnPlayerLiveUpdate;
    public void PlayerLiveUpdate(int healthAmount)
    {
        OnPlayerLiveUpdate?.Invoke(healthAmount);
    }

    //Event for updating player gold and the ui
    public event Action<int> OnPlayerGoldUpdate;
    public void PlayerGoldUpdate(int goldAmount)
    {
        OnPlayerGoldUpdate?.Invoke(goldAmount);
    }

    //Event for creating an ui popup notification
    public event Action<string,Vector3,Color,int> OnPopUp;
    public void PopUp(string popUpText, Vector3 pos, Color textColor, int iconIndex)
    {
        OnPopUp?.Invoke(popUpText, pos, textColor, iconIndex);
    }


}

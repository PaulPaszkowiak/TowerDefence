using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TMP_Text tmp_Text;
    public GameObject currentSpriteIcon;
    public Sprite[] sprites;
    private float lifetime = 1f;
    private float timer;   
    private Vector3 startPos;
    private Vector3 targetPos;
    private float offset = 20f;


    // Start is called before the first frame update
    void Start()
    {
        //look at camera
        transform.LookAt(2 * transform.position - Camera.main.transform.position);             
        startPos = transform.position;        
        targetPos = startPos + new Vector3(0,offset,0);
        transform.localScale = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fadeTimer = lifetime / 2f;

        if (timer > lifetime)
        {
            Destroy(gameObject);
        }
        else
        {
            //Fade out
            if (timer > fadeTimer)
                tmp_Text.color = Color.Lerp(tmp_Text.color, Color.clear, (timer - fadeTimer) / (lifetime - fadeTimer));
        }

        //Adjust the scale and Position, depending on the lifetime
        transform.localPosition = Vector3.Lerp(startPos, targetPos, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.5f, Mathf.Sin(timer / lifetime));
    }

    //Setting up the popup
    public void SetPopUp(string popUpText, Color textColor, int iconIndex)
    {        
        tmp_Text.text = popUpText;
        tmp_Text.color = textColor;        
        currentSpriteIcon.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[iconIndex];
    }
}

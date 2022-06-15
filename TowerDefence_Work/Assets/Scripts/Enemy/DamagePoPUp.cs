using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePoPUp : MonoBehaviour
{
    public TMP_Text dmg_Text;
    private float lifetime = 1.5f;
    private float timer;
    private float minDist = 1f;
    private float maxDist = 2f;
    private Vector3 startPos;
    private Vector3 targetPos;
   

    // Start is called before the first frame update
    void Start()
    {
        //look at camera
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        //Randomize rotation and position
        float dir = Random.rotation.eulerAngles.z;       
        startPos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = startPos + (Quaternion.Euler(0,0,dir) * new Vector3(dist,dist,0f));
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
                dmg_Text.color = Color.Lerp(dmg_Text.color, Color.clear, (timer - fadeTimer) / (lifetime - fadeTimer));
        }
           
        //Adjust the scale and Position, depending on the lifetime
        transform.localPosition = Vector3.Lerp(startPos, targetPos, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one*0.5f, Mathf.Sin(timer / lifetime));
    }

    //Set the text and color of the popup
    public void SetDamageText(int damage,bool isCriticalHit)
    {
        if (isCriticalHit)
        {
            dmg_Text.text = damage.ToString();
            dmg_Text.color = Color.red;
        }
        else
        {
            dmg_Text.text = damage.ToString();
            dmg_Text.color = Color.yellow;
        }
        
    }
}

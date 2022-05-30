using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float speed = 55f;

    // Update is called once per frame
    void Update()
    {
        SimpleCamMOvement();
        SimpleCamZoom();
    }
   
    private void SimpleCamMOvement()
    {
        if (Input.GetKey("w"))      
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        
        if (Input.GetKey("s"))       
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        
        if (Input.GetKey("d"))      
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        
        if (Input.GetKey("a"))      
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);        
    }

    private void SimpleCamZoom()
    {
        if (Input.GetKey("q"))
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        if (Input.GetKey("e"))
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.Self);
    }
}

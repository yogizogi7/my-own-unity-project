using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemcan : MonoBehaviour
{
    public float rotatespeed;
    
    // Update is called once per frame

    void Awake()
    {
       
    }
    void Update()
    {
        transform.Rotate(Vector3.up*rotatespeed*Time.deltaTime,Space.World);
    }

   

}

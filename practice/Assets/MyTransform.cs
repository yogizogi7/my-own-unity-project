using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTransform : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 scaleChange;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += scaleChange;
    }
}

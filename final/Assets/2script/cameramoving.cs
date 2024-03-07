﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramoving : MonoBehaviour
{
    Transform playerTransform;
    Vector3 offset;
    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerTransform.position+offset;
    }
}

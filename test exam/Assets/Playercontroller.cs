using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    private Rigidbody playerRb;
    [Header("Set in Inspector(edit mode only")]
    public float gravitymodifier = 2.0f;
    [Header("Set in Inspector")]
    public float jumpforce = 15.0f;
    [Header("Set Dynamically")]
    public bool isonground = true;
    public bool gameover = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb= GetComponent<Rigidbody>();
        Physics.gravity *= gravitymodifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&isonground) {
            playerRb.AddForce(Vector3.up * jumpforce,ForceMode.Impulse);
            isonground = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isonground = true;
        }
        else if(collision.gameObject.CompareTag("obstacle")){
            gameover = true;
            Debug.Log("game over");
        }
    }
}

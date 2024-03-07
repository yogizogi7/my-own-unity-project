using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20;
    private Playercontroller playercontrollerscript;
    private float leftbound = -10;
    // Start is called before the first frame update
    void Start()
    {
        playercontrollerscript = GameObject.Find("Player").GetComponent<Playercontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontrollerscript.gameover == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (transform.position.x < leftbound && gameObject.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

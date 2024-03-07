using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerball : MonoBehaviour
{
    AudioSource audio1;
    public int itemcount;
    public float jumppower;
    bool isjump;
    public gamemanagerlogic manager;
    Rigidbody rigid;
    void Awake()
    {
        audio1 = GetComponent<AudioSource>();
        isjump = false;
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isjump==false)
        {
            isjump = true;
            rigid.AddForce(new Vector3(0,jumppower,0), ForceMode.Impulse);
        };    
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            isjump = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            itemcount++;
            audio1.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemcount);
        }
        else if (other.tag == "Finish")
        {
            if(itemcount==manager.totalitemcount)
            {
                if (manager.stage == 1)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(manager.stage + 1);
                }
            }
            else
            {
                SceneManager.LoadScene(manager.stage);
            }
        }
    }
}

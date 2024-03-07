using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class gamemanagerlogic : MonoBehaviour
{
    public int totalitemcount;
    public int stage;
    public Text stagecounttext;
    public Text playercounttext;
    void Awake()
    {
        stagecounttext.text = "/ " + totalitemcount;
    }
    public void GetItem(int count)
    {
        playercounttext.text = count.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(stage);
        }
    }
}

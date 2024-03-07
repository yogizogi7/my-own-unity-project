using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int Health;
    public PlayerMove player;
    public GameObject[] stages;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;
    // Start is called before the first frame update
    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }
    public void NextStage()
    {
        if (stageIndex < stages.Length-1)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "Stage" + (stageIndex+1);
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("게임 클리어");
            UIRestartBtn.SetActive(true);
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Clear!";
        }
        totalPoint = stagePoint;
        stagePoint = 0;
    }
    public void HealthDown()
    {
        if (Health > 1)
        {
            Health--;
            UIhealth[Health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            player.OnDie();
            UIRestartBtn.SetActive(true);
        }
    }
   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
            if (Health > 1)
            {
                PlayerReposition();
            }
            HealthDown();
        }
    }
    void PlayerReposition()
    {
        player.transform.position = new Vector3(-6, 1, -1);
        player.velocityZero();
    }
    public void Restart()
    {
        Time.timeScale=1;
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    GameObject centerText;
    [SerializeField]
    GameObject timeText;
    [SerializeField]
    GameObject powerText;
    [SerializeField]
    GameObject rankText;
    [SerializeField]
    GameObject ball;

    BallController ballController;

    bool isGameOff;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        isGameOff = false;
        ballController = ball.GetComponent<BallController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectsWithTag("Block").Length<=0) GameClear();
        else if (GameObject.FindGameObjectsWithTag("Ball").Length <= 0) GameOver();

        if (isGameOff)
        {
            if (Input.GetKeyDown(KeyCode.R)) Restart();
        }
        else
        {
            time +=Time.deltaTime;
            timeText.GetComponent<Text>().text = "Time: " + time.ToString("f2") + "sec";

            int atackPower = ballController.power;
            if (ballController.isSmash)
            {
                atackPower *= 2;
                powerText.GetComponent<Text>().color = Color.red;
            }
            else
            {
                powerText.GetComponent<Text>().color = Color.white;
            }

            powerText.GetComponent<Text>().text = "Power: " + atackPower.ToString()+"/100";

        }
    }

    public void GameOver()
    {
        centerText.GetComponent<Text>().text = "GameOver...\nRetry:Press[R]";
        centerText.SetActive(true);

        timeText.SetActive(false);

        isGameOff = true;
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameClear()
    {
        centerText.GetComponent<Text>().text = "Clear!\nRetry:Press[R]";
        centerText.SetActive(true);

        string rank;
        if (time < 30f) rank = "S";
        else if (time < 40f) rank = "A";
        else if(time < 50f) rank = "B";
        else if(time < 60f) rank = "C";
        else rank = "D";

        rankText.GetComponent<Text>().text = "Rank "+rank;
        rankText.SetActive(true);

        isGameOff = true;
    }
}

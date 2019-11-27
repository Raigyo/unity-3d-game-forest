using UnityEngine;
using UnityEngine.UI;
using TMPro;

//ScoreAndTimeController : counter and score management / detect if lost or win

public class ScoreAndTimeController : MonoBehaviour
{
    [SerializeField] private float mainTimer;
    [SerializeField] private GameObject goRubbish;

    //Counter
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
    private int minutesFinal;
    private int secondsFinal;

    //Scores
    private int count;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI countText;
    private int goRubbishCount;

    public GameObject gameController;


    void Start()
    {
        timer = mainTimer;

        //Cound the number of gans un the GO Rubbish
        goRubbishCount = goRubbish.GetComponentsInChildren<Transform>().GetLength(0) - 1;
        goRubbishCount = goRubbishCount / 2; //divided by 2 because all prefabs have a children with icon
        //Displays counter of can grabbed
        count = 0;
        countText.text = "Cans: " + count.ToString() + "/" + goRubbishCount;
    }

    void Update()
    {
        //Launches coundown
        if (timer > 1.0f && canCount)
        {
            timer -= Time.deltaTime;
            //int hours = Mathf.FloorToInt(timer / 3600F);
            int minutes = Mathf.FloorToInt((timer % 3600) / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            //string formattedTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = "Time: " + formattedTime;
            if (timer < 60.0f)
            {
                timerText.color = Color.red;
            }  
        }
        //Stops coundown => player has lost if zero
        else if (timer <= 1.0f && !doOnce)
        {                      
            canCount = false;
            doOnce = true;
            //timerText.text = "Time: 00:00";
            timer = 0.0f;
            GameOver();
        }
    }
    //Update score and detect if the player has won
    public void SetCountText()
    {
        count++;
        countText.text = "Cans: " + count.ToString() + "/" + goRubbishCount;
        if (goRubbishCount == count)
        {
            float finalTime = mainTimer - timer;
            minutesFinal = Mathf.FloorToInt((finalTime % 3600) / 60);
            secondsFinal = Mathf.FloorToInt(finalTime % 60);
            print("Win!");
            Invoke("DelayWin", 2);
        }
    }
    //Send the information of win in GameController.cs
    void DelayWin()
    {
        gameController.GetComponent<GameController>().WinGame(count, minutesFinal, secondsFinal);
    }

    //Send the information of lost in GameController.cs
    void GameOver()
    {
        //wait 2 seconds to leave the last animation of picking up
        Invoke("DelayLost", 2);
    }

    void DelayLost()
    {
        gameController.GetComponent<GameController>().LostGame(count);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;
    public Text outOfTime;
    public bool timerActive = true;

    private void Start()
    {
        // Starts the timer automatically
        if (timerActive)
        {
            timerIsRunning = true;
        }
        timeText.text = "";
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                //Debug.Log("Time has run out!");
                outOfTime.text = "You Ran Out Of Time!";
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer(bool startOrStop)
    {
        if (startOrStop == true)
        {
            timerIsRunning = false;
        } else
        {
            timerIsRunning = true;
        }
        
    }
}

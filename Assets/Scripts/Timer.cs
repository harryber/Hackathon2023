using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float seconds = 10f; // set the number of seconds to countdown here
    public TMP_Text timerText; // reference to a text component to display the countdown
    public bool start = false;
    public bool ready = true;

    private float timeRemaining; // the amount of time remaining in the countdown

    void Start()
    {
        SetTimeRemaining(seconds); // set the initial time remaining to the number of seconds specified
    }

    public void SetTimeRemaining(float time)
    {
        timeRemaining = time;
    }

    void Update()
    {
        if (start) Count();
    }

    void Count()
    {
        timeRemaining -= Time.deltaTime; // subtract the elapsed time since the last frame from the time remaining

       

        if (timeRemaining <= 0)
        {
            // countdown is complete, do something here (e.g. trigger an event or load a new scene)
            ready = true;
            start = false;
            timerText.text = "Ready";
        }
        else
        {
            ready = false;
            // update the text component to display the remaining time rounded to 2 decimal places
            timerText.text = Mathf.RoundToInt(timeRemaining).ToString();
        }
    }
}
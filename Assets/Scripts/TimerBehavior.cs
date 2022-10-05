using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerBehavior : MonoBehaviour
{
    public bool IsActive {get; set;}

    public float timeLeft;          // time in seconds
    public TMP_Text timerText;

    private float initialTime; 

    // Start is called before the first frame update
    void Start()
    {
        IsActive = true;
        initialTime = 60;   
        timeLeft = initialTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive && timeLeft > 0)
            timeLeft -= Time.deltaTime;
        
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int timeLeftRounded = (int) Mathf.Ceil(timeLeft);
        string timeLeftAsString;

        if (timeLeftRounded >= 60f)
        {
            timeLeftAsString = $"1:{(timeLeftRounded % 60f).ToString("00")}";
        } else
        {
            timeLeftAsString = $"0:{timeLeftRounded.ToString("00")}";
        }

        timerText.text = timeLeftAsString;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{
    public bool IsActive {get; set;}

    public float timeLeft;          // time in seconds

    private float initialTime; 

    // Start is called before the first frame update
    void Start()
    {
        IsActive = false;
        initialTime = 60;   
        timeLeft = initialTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive && timeLeft > 0)
            timeLeft -= Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Aika : MonoBehaviour
{
    public float timeRemanining = 60;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public Animator playerAnim;

    void Start()
    {
        timeIsRunning = true;
    }

    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemanining > 0)
            {
                timeRemanining -= Time.deltaTime;
                DisplayTime(timeRemanining);
            }
            else
            {
                timeRemanining = 0;
                timeIsRunning = false;
                playerAnim.SetBool("Death", false);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay -= 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public TMP_Text timeCounter;

    private float elapsedTime;
    private float pauseTime = 0;
    private bool timeGoing;
    private TimeSpan timePlaying;

	private void Awake()
	{
        instance = this;
	}
	void Start()
    {
        timeCounter.text = "Time: 00:00.00";
        timeGoing = false;
	}

    public void BeginTimer()
    {
		timeGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
	}
    public void EndTimer()
    {
        timeGoing = false;
    }
    private IEnumerator UpdateTimer()
    {
        if (pauseTime > 0)
        {
            pauseTime -= Time.deltaTime;
        }else
        {
            while (timeGoing)
            {
                elapsedTime += Time.deltaTime;
                timePlaying = TimeSpan.FromSeconds(elapsedTime);
                string timePlayingStr = "Time: " + timePlaying.ToString("mm' : 'ss' . 'ff");
                timeCounter.text = timePlayingStr;
            
                yield return null;
            }
        }
       
    }
    public void PauseTimer(float pauseLength)
    {
        pauseTime = pauseLength * 0.3f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class RollerGameManager : Singleton<RollerGameManager>
{
	[Header("Events")]
	[SerializeField] EventRouter startGameEvent;
	[SerializeField] EventRouter stopGameEvent;
	[SerializeField] EventRouter winGameEvent;

	[Header("UI")]
	[SerializeField] Slider healthMeter;
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] TMP_Text timerUI;
    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject titleUI;

	[Header("Objects")]
	[SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerStart;
	
    [Header("Extra")]
	[SerializeField] AudioSource gameMusic;
    [SerializeField] TimerController gameTimer;

    public enum State
    {
        TITLESCREEN,
        START_GAME,
        PLAY_GAME,
        GAME_OVER
    }
    State state = State.TITLESCREEN;
    float stateTimer = 0;
    private void Start()
    {
		winGameEvent.onEvent += SetGameWin;
	}
	private void Update()
	{
        switch (state)
        {
            case State.TITLESCREEN:
                titleUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                state= State.START_GAME;
                break;
            case State.START_GAME:
				titleUI.SetActive(false);
				Cursor.lockState = CursorLockMode.Locked;
				Instantiate(playerPrefab, playerStart.position, playerStart.rotation);
				state = State.PLAY_GAME;
				gameMusic.Play();
				startGameEvent.Notify();
				break;
            case State.PLAY_GAME:
                gameTimer.BeginTimer();
                

				break;
            case State.GAME_OVER:
                stateTimer -= Time.deltaTime;
                if (stateTimer > 0)
                {
                    gameoverUI.SetActive(false);
                    state = State.TITLESCREEN;
                }
                break;
            default:
                break;
        }
    }
	public void setHealth(int health)
    {
        healthMeter.value += Mathf.Clamp(health, 0, 100);
        if (healthMeter.value > 100) healthMeter.value = 100;
    }

    public void setScore(int points)
    {
        scoreUI.text= "Score: " + points.ToString();
    }

    public void setGameOver()
    {
		stopGameEvent.Notify();
		//gameoverUI.SetActive(true);
		//gameMusic.Stop();
		//state = State.GAME_OVER;
		//stateTimer = 2;
	}
    public void OnClickGame()
    {
        state = State.PLAY_GAME;
    }

	public void SetGameWin()
	{
		Debug.Log("Win!!!");
	}
    public void PauseTimer(float pauseLength)
    {
        gameTimer.PauseTimer(pauseLength);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceGameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreUI;
	[SerializeField] private GameObject gameoverUI;

	int score = 0;
	public void addPoints(int points)
	{
		score += points;
		scoreUI.text = score.ToString();
	}

	public void setGameOver()
	{
		gameoverUI.SetActive(true);
	}

}

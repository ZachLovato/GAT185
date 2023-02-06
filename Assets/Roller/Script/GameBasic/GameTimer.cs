using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float LevelTimer = 0;
    private float PauseTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
		LevelTimer = 0;
		PauseTimer = 0;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	private void FixedUpdate()
	{
        if (PauseTimer > 0)
        {
            PauseTimer -= Time.deltaTime;
        }
        else
        {
			LevelTimer += Time.deltaTime;
		}
	}
}

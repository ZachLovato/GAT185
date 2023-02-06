using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class TimerPause : Interactable
{
	[SerializeField, Range(1,10)] private float pauseLength = 3;
    // Start is called before the first frame update
    void Start()
    {
		GetComponent<CollisionEvent>().onEnter += OnInteract;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public override void OnInteract(GameObject go)
	{
		if (go.TryGetComponent<PlayerRoller>(out PlayerRoller player))
		{
			player.PauseTimer(pauseLength);
		}

		if (interactFX == null) Instantiate(interactFX, transform.position, Quaternion.identity);
		if (destroyOnInteract) Destroy(gameObject);
	}
}

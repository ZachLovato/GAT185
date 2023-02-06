using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class RollerJumpBoost : Interactable
{
	[SerializeField, Range(.5f,4)] private float boost = 1;

	// Start is called before the first frame update
	void Start()
    {
		GetComponent<CollisionEvent>().onEnter += OnInteract;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	public override void OnInteract(GameObject target)
	{
		if (target.TryGetComponent<PlayerRoller>(out PlayerRoller player))
		{
			player.SetBoost(boost);
		}

		if (interactFX == null) Instantiate(interactFX, transform.position, Quaternion.identity);
		if (destroyOnInteract) Destroy(gameObject);
	}
}

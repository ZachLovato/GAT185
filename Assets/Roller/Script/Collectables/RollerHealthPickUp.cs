using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class RollerHealthPickUp : Interactable
{
	[SerializeField] private float heal;
	private void Start()
	{
		GetComponent<CollisionEvent>().onEnter += OnInteract;
	}
	public override void OnInteract(GameObject target)
	{
		if (target.TryGetComponent<RollerHealth>(out RollerHealth health))
		{
			health.OnApplyHealth(heal);
		}

		if (interactFX == null) Instantiate(interactFX, transform.position, Quaternion.identity);
		if (destroyOnInteract) Destroy(gameObject);
	}



}

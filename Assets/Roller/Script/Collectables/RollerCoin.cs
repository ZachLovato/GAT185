using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class RollerCoin : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
        //onEnter += OnCoinPickUp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract(GameObject go)
    {
        if (go.TryGetComponent<PlayerRoller>(out PlayerRoller player))
        {
            player.AddPoints(100);
        }

        if (interactFX == null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if (destroyOnInteract) Destroy(gameObject);
    }
}

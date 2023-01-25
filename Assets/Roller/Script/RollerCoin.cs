using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCoin : Collidable
{
    [SerializeField] GameObject pickupFX;
    // Start is called before the first frame update
    void Start()
    {
        onEnter += OnCoinPickUp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCoinPickUp(GameObject go)
    {
        if (go.TryGetComponent<PlayerRoller>(out PlayerRoller player))
        {
            player.AddPoints(100);
        }

        Debug.Log("Pick Up I");
        Instantiate(pickupFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

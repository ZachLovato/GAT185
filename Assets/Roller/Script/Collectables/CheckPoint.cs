using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class CheckPoint : Interactable
{
    [SerializeField] private GameObject ResetLocation;
    public override void OnInteract(GameObject go)
    {
        if (go.TryGetComponent<PlayerRoller>(out PlayerRoller player))
        {
            //Debug.Log("check Point");
            player.SetResetLocation(ResetLocation.transform);
        }

        if (interactFX == null) Instantiate(interactFX, transform.position, Quaternion.identity);
        //if (destroyOnInteract) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

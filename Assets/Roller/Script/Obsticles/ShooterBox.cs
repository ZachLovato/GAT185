using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBox : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField, Range(.1f, 10f)] private float ShotDelay;
    private float ShotTimer;


    // Start is called before the first frame update
    void Start()
    {
        ShotTimer = ShotDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShotTimer < 0) 
        {
            GameObject bull = GameObject.Instantiate(Bullet);
            bull.transform.position = transform.position + Vector3.forward;
            bull.transform.rotation = transform.rotation;
            ShotTimer = ShotDelay;
        }
    }

    private void FixedUpdate()
    {
        ShotTimer -= Time.deltaTime;
        //Debug.Log(ShotTimer);
    }
}

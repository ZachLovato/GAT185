using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField, Range(.1f, 10)] private float ShotVelocity = 1;
    [SerializeField] private float destoyTime = 3;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (destoyTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        destoyTime -= Time.deltaTime;
		transform.Translate(Vector3.forward * ShotVelocity * Time.deltaTime);
    }
}

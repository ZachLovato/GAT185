using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRoller : MonoBehaviour
{
    [SerializeField, Range(2,20)] private float maxForce = 5;
    [SerializeField, Range(10,50)] private float jumpForce = 25;
    private Vector3 force;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        force = direction * maxForce;

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
        }
    }

	private void FixedUpdate()
	{
        rb.AddForce(force);    
    }
}

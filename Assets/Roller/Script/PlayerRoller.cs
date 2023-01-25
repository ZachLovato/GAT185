using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRoller : MonoBehaviour
{
    [SerializeField] private Transform view;
    [SerializeField, Range(2,20)] private float maxForce = 5;
    [SerializeField, Range(2,50)] private float jumpForce = 2;

    private int score = 0;
    private Vector3 force;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = Camera.main.transform;
        Camera.main.GetComponent<RollerCamera>().setTarget(transform);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);

        force = viewSpace * (direction * maxForce);

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
        }

        RollerGameManager.Instance.setHealth(50);
    }

	private void FixedUpdate()
	{
        rb.AddForce(force);    
    }

    public void AddPoints(int points)
    {
        score += points;
        RollerGameManager.Instance.setScore(score);
    }
}

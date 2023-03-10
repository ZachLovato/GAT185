using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRoller : MonoBehaviour
{
    [SerializeField] private Transform view;
    [SerializeField, Range(2,20)] private float maxForce = 5;
    [SerializeField, Range(2,50)] private float jumpForce = 2;
    [SerializeField] private float groundRayLength = 1;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform resetLocation;
    //[SerializeField] private AudioSource[] sounds;

    private int score = 0;
    private Vector3 force;
    private Rigidbody rb;

    public float jumpBoost = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        view = Camera.main.transform;
        Camera.main.GetComponent<RollerCamera>().setTarget(transform);

        GetComponent<RollerHealth>().onDamage += OnDamage;
        GetComponent<RollerHealth>().onDamage += OnHeal;
        GetComponent<RollerHealth>().onDeath = OnDeath;
		RollerGameManager.Instance.setHealth(0);

        resetLocation.position = Vector3.zero;
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);

        force = viewSpace * (direction * maxForce);

		Ray ray = new Ray(transform.position, Vector3.down);
		bool onGround = Physics.Raycast(ray, groundRayLength, groundLayer);
		Debug.DrawRay(transform.position, ray.direction * groundRayLength);

		if (onGround && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * jumpForce * jumpBoost, ForceMode.Impulse);

            if (jumpBoost != 1) jumpBoost = 1;
            //sounds[0].Play();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Respawn");
            transform.position = resetLocation.position;
        }

        //RollerGameManager.Instance.setHealth(50);
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
    public void SetBoost(float boost)
    {
        jumpBoost = boost;
    }
	public void OnHeal()
	{
		RollerGameManager.Instance.setHealth((int)GetComponent<RollerHealth>().health);
	}

	public void OnDamage()
    {
        Debug.Log("oww");
        RollerGameManager.Instance.setHealth((int)GetComponent<RollerHealth>().health);
    }

    public void OnDeath()
    {
        RollerGameManager.Instance.setGameOver();
        Destroy(gameObject);
    }
    public void PauseTimer(float pause)
    {
        RollerGameManager.Instance.PauseTimer(pause);
    }

    public void SetResetLocation(Transform reset)
    {
        resetLocation = reset;
    }
}

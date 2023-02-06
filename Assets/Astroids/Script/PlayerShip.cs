using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField, Range(10f, 50), Tooltip("Controls Player's speed")] float speed = 15;
    [SerializeField, Tooltip("Controls Player's rotate speed")] float rotationRate = 180;
    public GameObject[] preFab;
    public Transform[] bulletSpawnLocation;
    int bulletSpwn = 1;
    int weaponChoice = 0;

    private void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) weaponChoice = 0;
        if (Input.GetKey(KeyCode.Alpha2)) weaponChoice = 1;

        Vector3 direction = Vector3.zero;
        direction.z = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.zero;
        rotation.y = Input.GetAxis("Horizontal");

        Quaternion rotate = Quaternion.Euler(rotation * rotationRate * Time.deltaTime);

        transform.rotation *= rotate;

        

        transform.Translate(direction * speed * Time.deltaTime);

        // ship rotate left rightwith direction




        if (Input.GetButtonDown ("Fire1") && weaponChoice == 1)
        {
            GameObject go = Instantiate(preFab[0], bulletSpawnLocation[2].position, bulletSpawnLocation[2].rotation);
        }
		if (weaponChoice == 0 && Input.GetButton("Fire1"))
		{
			GameObject go = Instantiate(preFab[1], bulletSpawnLocation[bulletSpwn].position, bulletSpawnLocation[bulletSpwn].rotation);
			bulletSpwn = (bulletSpwn + 1) % 2;
		}



	}
    public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			//FindObjectOfType<SpaceGameManager>()?.setGameOver();
		}
	}	
}
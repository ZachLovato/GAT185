using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(10f, 50), Tooltip("Controls Player's speed")] float speed = 15;
    [SerializeField, Tooltip("Controls Player's rotate speed")] float rotationRate = 180;
    public GameObject preFab;
    public Transform[] bulletSpawnLocation;
    int bulletSpwn = 1;

    private void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.z = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.zero;
        rotation.y = Input.GetAxis("Horizontal");

        Quaternion rotate = Quaternion.Euler(rotation * rotationRate * Time.deltaTime);

        transform.rotation *= rotate;
        transform.Translate(direction * speed * Time.deltaTime);
        //transform.position += direction * speed * Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            //GetComponent<AudioSource>().Play();
            //Destroy(go, 2);
            GameObject go = Instantiate(preFab, bulletSpawnLocation[bulletSpwn].position, bulletSpawnLocation[bulletSpwn].rotation); ;

            //switch (bulletSpwn)
            //{
            //    case 0:
            //        go = Instantiate(preFab, bulletSpawnLocation[0].position, bulletSpawnLocation[0].rotation);
            //        break;
            //    case 1:
            //        go = Instantiate(preFab, bulletSpawnLocation[1].position, bulletSpawnLocation[1].rotation);
            //        break;
            //}

            bulletSpwn = (bulletSpwn + 1) % 2;
        }

    }
}
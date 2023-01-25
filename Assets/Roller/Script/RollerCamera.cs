using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(1, 20)] private float distance;
    [SerializeField, Range(20, 80)] private float pitch = 20;
    [SerializeField, Range(0.1f, 5)] private float sensitivity = 1;
    
    private float yaw = 0;

    // Update is called once per frame
    public void setTarget(Transform target)
    {
        this.target = target;
    }
    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;

        Quaternion qyaw = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion qpitch = Quaternion.AngleAxis(pitch, Vector3.right);
        Quaternion rotation = qyaw * qpitch;

        Vector3 offset = rotation * Vector3.back * distance;

        transform.position = target.position + offset;
        transform.rotation = Quaternion.LookRotation(-offset);
    }
}

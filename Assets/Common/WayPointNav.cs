using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointNav : MonoBehaviour
{
    public WayPoint wayPoint { get; set; }

	private void Start()
	{
		GameObject go = WayPoint.GetNearestGameObjectWithTag(transform.position,"Waypoint");
		wayPoint = go.GetComponent<WayPoint>();
	}
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private WayPoint[] waypoints;

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent<WayPointNav>(out WayPointNav waypointNavigator))
		{
			// if current navigator waypoint is this waypoint, set new random waypoint
			if (waypointNavigator.wayPoint == this)
			{
				waypointNavigator.wayPoint = waypoints[Random.Range(0, waypoints.Length)];
			}
		}
	}

	public static GameObject GetNearestGameObjectWithTag(Vector3 position, string tag)
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
		gameObjects = gameObjects.OrderBy(go => (go.transform.position - position).sqrMagnitude).ToArray();

		return (gameObjects.Length > 0) ? gameObjects[0] : null;
	}

}

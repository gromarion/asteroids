using UnityEngine;
using System.Collections;

public class BoundsController : MonoBehaviour {

	public void Update () {
		Vector3 viewport_point = Camera.main.WorldToViewportPoint(transform.position);
		Vector3 new_position = transform.position;
		if (viewport_point.x > 1 || viewport_point.x < 0) {
			new_position.x = - new_position.x;
		}
		if (viewport_point.y > 1 || viewport_point.y < 0) {
			new_position.y = - new_position.y;
		}
		transform.position = new_position;
	}
}

using UnityEngine;
using System.Collections;

public class BoundsController : MonoBehaviour {

	public const float MAP_SIZE = 5f;

	public void Update () {
//		Vector3 viewport_point = Camera.main.WorldToViewportPoint(transform.position);
		Vector3 modelPosition = transform.position * .5f + Vector3.one * .5f * MAP_SIZE;

		modelPosition.x = Mathf.Repeat(modelPosition.x, MAP_SIZE);
		modelPosition.y = Mathf.Repeat(modelPosition.y, MAP_SIZE);

		transform.position = modelPosition * 2f - Vector3.one * MAP_SIZE;
	}
}

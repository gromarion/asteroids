using UnityEngine;
using System.Collections;

public class BoundsController : MonoBehaviour {

	public static BoundsController instance;
	public static float MAP_SIZE = 12f;
	private static Vector2 REFERENCE_ASPECT = new Vector2 (1024f, 768f);
	private float camera_horizontal, camera_vertical;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
	}

	public void Update () {
		camera_horizontal = MAP_SIZE * Screen.width/REFERENCE_ASPECT.x;
		camera_vertical = MAP_SIZE * (REFERENCE_ASPECT.y / REFERENCE_ASPECT.x) * Screen.height/REFERENCE_ASPECT.y;
		float model_position_x = transform.position.x * 0.5f + 0.5f * camera_horizontal;
		float model_position_y = transform.position.y * 0.5f + 0.5f * camera_vertical;

		model_position_x = Mathf.Repeat(model_position_x, camera_horizontal) * 2f - camera_horizontal;
		model_position_y = Mathf.Repeat(model_position_y, camera_vertical) * 2f - camera_vertical;

		transform.position = new Vector3(model_position_x, model_position_y, 1);
	}

	public float CameraSize () {
		return camera_vertical;
	}

	public float CameraHorizontal () {
		return camera_horizontal;
	}

	public float CameraVertical () {
		return camera_vertical;
	}
}

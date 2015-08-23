﻿using UnityEngine;
using System.Collections;

public class BoundsController : MonoBehaviour {

	public const float MAP_SIZE_HORIZONTAL = 10f;
	public const float MAP_SIZE_VERTICAL = 5.5f;
	public const float CAMERA_SIZE = MAP_SIZE_VERTICAL - 0.5f;

	public void Update () {
		float model_position_x = transform.position.x * 0.5f + 0.5f * MAP_SIZE_HORIZONTAL;
		float model_position_y = transform.position.y * 0.5f + 0.5f * MAP_SIZE_VERTICAL;

		model_position_x = Mathf.Repeat(model_position_x, MAP_SIZE_HORIZONTAL) * 2f - MAP_SIZE_HORIZONTAL;
		model_position_y = Mathf.Repeat(model_position_y, MAP_SIZE_VERTICAL) * 2f - MAP_SIZE_VERTICAL;

		transform.position = new Vector3(model_position_x, model_position_y, 1);
	}
}

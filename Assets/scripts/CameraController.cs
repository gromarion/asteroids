using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.orthographicSize = BoundsController.CAMERA_SIZE;
	}
}

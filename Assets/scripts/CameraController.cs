using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Update () {
		Camera.main.orthographicSize = BoundsController.instance.CameraSize ();
	}
}

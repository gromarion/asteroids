using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	
	void Update () {
		transform.localScale = new Vector3 (BoundsController.instance.CameraHorizontal () * 1.5f/5.5f, BoundsController.instance.CameraVertical () * 1.5f/5.5f, 1f);
	}
}

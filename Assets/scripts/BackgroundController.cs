using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	
	void Update () {
		float scale = BoundsController.instance.CameraHorizontal () * 1.5f / 5.5f;
		transform.localScale = new Vector3 (scale, scale, 1f);
	}
}

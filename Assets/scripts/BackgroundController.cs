using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	
	void Start () {
		transform.localScale = new Vector3 (BoundsController.MAP_SIZE_VERTICAL * 1.5f/5.5f, BoundsController.MAP_SIZE_VERTICAL * 1.5f/5.5f, 1f);
	}
}

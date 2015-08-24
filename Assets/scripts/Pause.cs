using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public bool pause;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)){
			pause = !pause;
			if (pause) {
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class PauseDelegate : MonoBehaviour {

	public bool pause;
	public GameObject pnlPause;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
			pause = !pause;
			if (pause) {
				pnlPause.SetActive(true);
				Time.timeScale = 0;
			} else {
				pnlPause.SetActive(false);
				Time.timeScale = 1;
			}
		}
	}
}

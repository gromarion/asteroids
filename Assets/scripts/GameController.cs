using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag("asteroid");
		
		if (asteroids.Length == 0) {
			Application.LoadLevel("mainmenu");
		}
	}
}

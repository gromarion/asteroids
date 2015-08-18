using UnityEngine;
using System.Collections;

public class Router : MonoBehaviour {

	public void showGameScene () {
		Debug.Log ("click");
		Application.LoadLevel("gameplay");
	}
}

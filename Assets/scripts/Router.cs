using UnityEngine;
using System.Collections;

public class Router : MonoBehaviour {

	public void showGameScene () {
		Application.LoadLevel("gameplay");
	}

	public void showMainMenu () {
		Application.LoadLevel("mainmenu");
	}
}

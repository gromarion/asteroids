using UnityEngine;
using System.Collections;

public class MainSceneUIController : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject tutorial;

	public GameObject continueText;
	private float nextActionTime = 0.0f; 
	public bool continueTextOn;
	public static float PERIOD = .7f;

	void Awake () {
		continueTextOn = false;
	}

	public void showTutorial () {
		const bool showMainMenu = false;
		mainMenu.SetActive (showMainMenu);
		tutorial.SetActive (!showMainMenu);
	}

	public void showMainMenu () {
		const bool showMainMenu = true;
		mainMenu.SetActive (showMainMenu);
		tutorial.SetActive (!showMainMenu);
	}

	void Update () {
		if (Input.anyKey) {
			showMainMenu ();
		}
		if (Time.time > nextActionTime) { 
			nextActionTime += PERIOD;
			continueTextOn = !continueTextOn;
			continueText.SetActive (continueTextOn);
		}
	}
}

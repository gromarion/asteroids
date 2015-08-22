﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

	public GameObject hud;
	public Text txtScore;
	public Text txtLevel;
	public Text txtBonus;
	public Text txtFinalScore;
	public Text txtFinalLevel;
	public GameObject pnlGameOver;

	public void RefreshUi() {
		txtScore.text = GameManager.instance.GetScore().ToString();
		txtLevel.text = GameManager.instance.GetLevel().ToString();
		txtBonus.text = GameManager.instance.GetBonus().ToString();
	}

	public void ShowGameOver() {
		hud.SetActive(false);
		pnlGameOver.SetActive(true);
		txtFinalScore.text = GameManager.instance.GetScore().ToString();
		txtFinalLevel.text = GameManager.instance.GetLevel().ToString();
	}

	public void ShowHud() {
		hud.SetActive(true);
		pnlGameOver.SetActive(false);
	}

	public void HideAll() {
		hud.SetActive(false);
		pnlGameOver.SetActive(false);
	}
}

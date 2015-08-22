using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

	private Text txtScore;
	private Text txtLevel;
	private Text txtBonus;

	void Start () {
		txtScore = GameObject.Find("txtScore").GetComponent<Text>();
		txtLevel = GameObject.Find("txtLevel").GetComponent<Text>();
		txtBonus = GameObject.Find("txtBonus").GetComponent<Text>();
	}

	void Update() {
		this.RefreshUi();
	}

	public void RefreshUi() {
		txtScore.text = GameManager.instance.GetScore().ToString();
		txtLevel.text = GameManager.instance.GetLevel().ToString();
		txtBonus.text = GameManager.instance.GetBonus().ToString();
	}
}

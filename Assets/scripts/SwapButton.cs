using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwapButton : MonoBehaviour {

	public GameObject button;
	public bool on;
	public Sprite onSprite;
	public Sprite offSprite;

	public void swap () {
		on = !on;
		Sprite sprite;
		Image actual = button.GetComponent<Image> ();
		if (on) {
			sprite = onSprite;
		} else {
			sprite = offSprite;
		}
		actual.overrideSprite = sprite;
	}
}

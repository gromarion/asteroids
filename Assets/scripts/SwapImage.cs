using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwapImage : MonoBehaviour {

	public GameObject gameObject;
	public bool on;
	public Sprite onSprite;
	public Sprite offSprite;

	public void swap () {
		on = !on;
		Sprite sprite;
		Image actual = gameObject.GetComponent<Image> ();
		if (actual) {
			if (on) {
				sprite = onSprite;
			} else {
				sprite = offSprite;
			}
			actual.overrideSprite = sprite;
		}
	}
}
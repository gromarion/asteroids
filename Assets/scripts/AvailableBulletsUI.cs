using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvailableBulletsUI : MonoBehaviour {

	public GameObject availableBulletsPanel;
	private Image[] bulletImages;	
	
	void Start () {
		bulletImages = availableBulletsPanel.GetComponentsInChildren<Image> ();
	}

	void Update () {
		int amountOfAvailable = PoolManager.instance.bulletPool.getAvailableObjects ();
		for (int i = 0; i < bulletImages.Length; i++) {
			bulletImages[i].gameObject.SetActive(i <= amountOfAvailable);
		}
	}
}
	
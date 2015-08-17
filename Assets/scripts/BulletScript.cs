using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed = 10f;
	public float maxLifeTime = 2f;

	private float lifeTime = 0f;

	void OnDisable() {
		CancelInvoke();
	}

	void Update () {
		if (lifeTime < maxLifeTime) {
			lifeTime += Time.deltaTime;
			transform.Translate(Vector2.up * Time.deltaTime * speed);
		}
		else {
			Destroy();
		}
	}

	void Destroy() {
		gameObject.SetActive(false);
		lifeTime = 0;
	}
}

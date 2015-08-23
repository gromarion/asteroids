using UnityEngine;
using System.Collections;

public class EnemyBulletController : MonoBehaviour {
	
	public float speed = 3f;
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
		GameObject explosion = PoolManager.instance.explotionPool.GetObject ();
		if (explosion != null) {
			ExplotionController explosionController = explosion.GetComponent<ExplotionController> ();
			if (explosionController != null) {
				explosion.transform.position = transform.position;
				explosion.SetActive (true);
				explosionController.BulletExplosion();
			}
		}
		
		gameObject.SetActive(false);
		lifeTime = 0;
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			PlayerController controller = collider.gameObject.GetComponent<PlayerController> ();
			if (controller != null) {
				controller.hit();
				Destroy ();
			} else {
				Debug.LogWarning ("Hit something tagged as Player, but didn't have an PlayerController");
			}
		} else if (collider.gameObject.tag == "asteroid") {
			AsteroidController controller = collider.gameObject.GetComponent<AsteroidController> ();
			if (controller != null) {
				controller.Damage(1);
				Destroy ();
			} else {
				Debug.LogWarning ("Hit something tagged as Asteroid, but didn't have an AsteroidController");
			}
		}
	}
}

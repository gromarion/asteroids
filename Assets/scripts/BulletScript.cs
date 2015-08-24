using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed = 10f;
	public float maxLifeTime = 2f;
	public int maxHealth = 1;

	private float lifeTime = 0f;
	private int health = 1;

	void Start () {
		health = maxHealth;
	}

	void OnDisable() {
		CancelInvoke();
	}

	void Update () {
		if (lifeTime < maxLifeTime) {
			lifeTime += Time.deltaTime;
			transform.Translate(Vector2.up * Time.deltaTime * speed);
		}
		else {
			DestroyBullet();
		}
	}

	void DestroyBullet() {
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
		health = maxHealth;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "asteroid") {
			AsteroidController controller = collider.gameObject.GetComponent<AsteroidController> ();
			if (controller != null) {
				controller.Damage (1);
				//Destroy ();
				if (--health <= 0) {
					DestroyBullet();
				}
			} else {
				Debug.LogWarning ("Hit something tagged as asteroid, but didn't have an AsteroidController");
			}
		} else if (collider.gameObject.tag == "enemy") {
			EnemyController controller = collider.gameObject.GetComponent<EnemyController> ();
			if (controller != null) {
				controller.Destroy();
				GameManager.instance.OnDestroyEnemy();
				//Destroy();
				if (--health <= 0) {
					DestroyBullet();
				}
			} else {
				Debug.LogWarning ("Hit something tagged as asteroid, but didn't have an AsteroidController");
			}
		}
	}
}

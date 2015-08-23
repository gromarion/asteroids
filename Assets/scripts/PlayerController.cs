using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rigidBody;
	private FireController fireController;

	public float speed = 3.0f;
	public float rotationSpeed = 3.0f;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		fireController = GetComponent<FireController> ();
	}
	
	void Update () {
		float vertical = Input.GetAxis("Vertical");
		float rotate = Input.GetAxis("Horizontal") * rotationSpeed;
		transform.Rotate(0, 0, 0 - rotate);

		if (Input.GetKeyDown(KeyCode.Space)) {
			SoundManager.instance.shoot();
			fireController.Fire();
		}

		if (vertical > 0) {
			SoundManager.instance.thrustOn();
			rigidBody.AddForce(transform.up * Time.deltaTime * speed * vertical);
		} else {
			SoundManager.instance.thrustOff();
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "asteroid") {
			AsteroidController asteroidController = collider.gameObject.GetComponent<AsteroidController>();
			if (asteroidController != null) {
				asteroidController.Damage(1);
			}
			hit();
			GameManager.instance.OnPlayerDeath();
		}
	}

	void hit () {
		// JA JA JA I let's make some fire
		GameObject explosion = PoolManager.instance.explotionPool.GetObject ();
		explosion.transform.position = transform.position;
		explosion.SetActive (true);
		// Die Ship die
		gameObject.SetActive (false);
		ExplotionController explosionController = explosion.GetComponent<ExplotionController> ();
		explosionController.ShipExplosion ();
	}
}

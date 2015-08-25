using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rigidBody;
	private FireController fireController;

	public float speed = 3.0f;
	public float rotationSpeed = 3.0f;
	public PlayerDebrisController playerDebris;
	public Animator weapon;

	private bool charging = false;
	private float chargeTime = 0f;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		fireController = GetComponent<FireController> ();
	}
	
	void Update () {
		float vertical = Input.GetAxis("Vertical");
		float rotate = Input.GetAxis("Horizontal") * rotationSpeed;
		transform.Rotate(0, 0, 0 - rotate);

		if (Input.GetKeyDown(KeyCode.Space)) {
			bool fired = fireController.Fire(weapon.gameObject);
			if (fired) {
				weapon.gameObject.SetActive(true);
				weapon.Play ("muzzle");
			}
			charging = true;
		}

		if (Input.GetKeyUp(KeyCode.Space)) {
			charging = false;
			if (chargeTime >= 1) {
				fireController.StrongFire(weapon.gameObject);
				rigidBody.AddForce(-transform.up * 10, ForceMode2D.Impulse);
				weapon.gameObject.SetActive(false);
			}

			weapon.gameObject.SetActive(false);
			chargeTime = 0;
		}

		if (charging) {
			chargeTime += Time.deltaTime;
			if (chargeTime >= 1.0f) {
				weapon.gameObject.SetActive(true);
				weapon.Play ("bullet_charge");
			}
			else if (chargeTime >= 0.5f) {
				weapon.gameObject.SetActive(true);
				weapon.Play ("bullet_charge_small");
			}
		}

		if (vertical > 0) {
			SoundManager.instance.thrustOn();
			rigidBody.AddForce(transform.up * Time.deltaTime * speed * vertical);
		} else {
			SoundManager.instance.thrustOff();
		}
	}

	public void RestartPlayer() {
		gameObject.SetActive(true);
		transform.position = Vector3.zero;
		transform.rotation = new Quaternion(0, 0, 0, 1);
		playerDebris.gameObject.SetActive(false);
		chargeTime = 0;
		charging = false;
		weapon.gameObject.SetActive(false);
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "asteroid" || collider.gameObject.tag == "enemy" || collider.gameObject.tag == "enemy_bullet") {
			AsteroidController asteroidController = collider.gameObject.GetComponent<AsteroidController>();
			if (asteroidController != null) {
				asteroidController.Damage(1);
			}
			hit();
			GameManager.instance.OnPlayerDeath();
			SoundManager.instance.explode();
			SoundManager.instance.thrustOff();
		}
	}

	public void hit () {
		// JA JA JA I let's make some fire
		GameObject explosion = PoolManager.instance.explotionPool.GetObject ();
		explosion.transform.position = transform.position;
		explosion.SetActive (true);
		// Die Ship die
		gameObject.SetActive (false);
		ExplotionController explosionController = explosion.GetComponent<ExplotionController> ();
		explosionController.ShipExplosion ();
		SoundManager.instance.thrustOff ();

		if (playerDebris != null) {
			playerDebris.transform.position = transform.position;
			playerDebris.transform.rotation = transform.rotation;
			playerDebris.gameObject.SetActive(true);
			playerDebris.Explode();
		}
	}
}

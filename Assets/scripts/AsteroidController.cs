using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	public Sprite smallAsteroid;
	public Sprite mediumAsteroid;
	public Sprite largeAsteroid;

	public GameObject spawnsAsteroid;
	public int spawnAmount = 0;
	public float maxSpeed = 5f;

	private Vector2 velocity;
	private float speed;
	private SpriteRenderer spriteRenderer;
	private CircleCollider2D circleCollider;
	private int health = 3;

	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		circleCollider = gameObject.GetComponent<CircleCollider2D>();
	}
	
	void Update () {
		transform.Translate(velocity * Time.deltaTime);
	}

	public void MakeSmallAsteroid() {
		spriteRenderer.sprite = smallAsteroid;
		maxSpeed = 3f;
		velocity = Random.insideUnitCircle * maxSpeed;
		circleCollider.radius = 0.25f;
		health = 1;
	}

	public void MakeMediumAsteroid() {
		spriteRenderer.sprite = mediumAsteroid;
		maxSpeed = 2f;
		velocity = Random.insideUnitCircle * maxSpeed;
		circleCollider.radius = 0.5f;
		health = 2;
	}

	public void MakeLargeAsteroid() {
		spriteRenderer.sprite = largeAsteroid;
		maxSpeed = 1f;
		velocity = Random.insideUnitCircle * maxSpeed;
		circleCollider.radius = 0.65f;
		health = 3;
	}

	public int GetHealth() {
		return health;
	}

	public void Damage(int damage) {
		SoundManager.instance.explode();
		int finalHealth = health - damage;
		for (int i = 0; i < spawnAmount; i++) {
			GameObject explosion = PoolManager.instance.explotionPool.GetObject ();
			explosion.transform.position = transform.position;
			explosion.SetActive (true);
			ExplotionController explosionController = explosion.GetComponent<ExplotionController> ();
			explosionController.SmallExplosion();
			// If the asteroid wasn't fully destroyed, split it
			if (finalHealth > 0) {
				GameObject clone = PoolManager.instance.asteroidPool.GetObject();
				if (clone != null) {
					AsteroidController asteroidController = clone.GetComponent<AsteroidController>();
					if (asteroidController != null) {
						clone.transform.position = transform.position;
						clone.SetActive(true);

						if (finalHealth == 1) {
							asteroidController.MakeSmallAsteroid();
						}
						else if (finalHealth == 2) {
							asteroidController.MakeMediumAsteroid();
						}
						else if (finalHealth == 3) {
							asteroidController.MakeLargeAsteroid();
						}
					}
					else {
						Debug.LogWarning("Asteroid pool not returning asteroids");
					}
				}
				else {
					Debug.Log("Empty asteroid pool");
				}
			}
		}

		GameManager.instance.OnDestroyAsteroid(finalHealth);
		gameObject.SetActive(false);
	}
}

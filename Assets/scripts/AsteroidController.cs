using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	public Sprite smallAsteroid;
	public Sprite mediumAsteroid;
	public Sprite largeAsteroid;

	public GameObject spawnsAsteroid;
	public int spawnAmount = 0;
	public float maxSpeed = 5f;

	private Vector2 speed;
	private SpriteRenderer spriteRenderer;
	private int health = 3;

	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		transform.Translate(speed * Time.deltaTime);
	}

	void MakeSmallAsteroid() {
		spriteRenderer.sprite = smallAsteroid;
		speed = Random.insideUnitCircle * maxSpeed;
		health = 1;
	}

	void MakeMediumAsteroid() {
		spriteRenderer.sprite = mediumAsteroid;
		speed = Random.insideUnitCircle * maxSpeed;
		health = 2;
	}

	void MakeLargeAsteroid() {
		spriteRenderer.sprite = largeAsteroid;
		speed = Random.insideUnitCircle * maxSpeed;
		health = 3;
	}

	public int GetHealth() {
		return health;
	}

	public void Damage(int damage) {
		int finalHealth = health - damage;
		for (int i = 0; i < spawnAmount; i++) {
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
		gameObject.SetActive(false);
	}
}

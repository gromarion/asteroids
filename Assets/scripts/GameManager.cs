using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public int initialAsteroids = 100;
	public float asteroidsSpawnMinRadius = 3f;
	public float asteroidsSpawnMaxRadius = 6f;
	public PlayerController player;

	private int remainingAsteroids = 0; // Amount of SMALL asteroids remaining in the current level
	private int currentLevel = 1;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
		
		else if (instance != this) {
			Destroy(gameObject); 
		}

		DontDestroyOnLoad(gameObject);

	}

	void Start () {
		currentLevel = 1;
		CreateLevel(currentLevel);
	}

	public void CreateLevel(int level) {
		remainingAsteroids = 0;
		for (int i = 0; i < level + initialAsteroids; i++) {
			GameObject asteroid = PoolManager.instance.asteroidPool.GetObject();
			if (asteroid != null) {
				AsteroidController asteroidController = asteroid.GetComponent<AsteroidController>();
				if (asteroidController != null) {
					asteroid.SetActive(true);

					// Make sure the asteroid doesn't spawn too close to the player
					float rotate = Random.Range(0, 360f);
					float dist = Random.Range(asteroidsSpawnMinRadius, asteroidsSpawnMaxRadius);
					asteroid.transform.position = player.transform.position;
					asteroid.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360f)));
					asteroid.transform.Translate(new Vector3(dist, dist));

					asteroidController.MakeLargeAsteroid();
					remainingAsteroids += 3 * 3 * 3;
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

	public void OnDestroyAsteroid() {
		remainingAsteroids--;

		if (remainingAsteroids <= 0) {
			CreateLevel(++currentLevel);
		}
	}
}

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public int initialAsteroids = 3;
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
		for (int i = 0; i < level + 2; i++) {
			GameObject asteroid = PoolManager.instance.asteroidPool.GetObject();
			asteroid.SetActive(true);
			asteroid.transform.position = Random.insideUnitCircle * 3;
			AsteroidController asteroidController = asteroid.GetComponent<AsteroidController>();
			asteroidController.MakeLargeAsteroid();
			remainingAsteroids += 3 * 3 * 3;
		}
	}

	public void OnDestroyAsteroid() {
		remainingAsteroids--;

		if (remainingAsteroids <= 0) {
			Debug.Log("Holy shit");
			CreateLevel(++currentLevel);
		}
	}
}

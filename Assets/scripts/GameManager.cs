using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public int initialAsteroids = 100;
	public float asteroidsSpawnMinRadius = 3f;
	public float asteroidsSpawnMaxRadius = 6f;
	public int bonusTimePerLevel = 30; // In seconds
	public int maxBonus = 5000;
	public int bonusPerLargeAsteroid = 10;
	public PlayerController player;

	public GameUIController ui;

	private int remainingAsteroids = 0; // Amount of SMALL asteroids remaining in the current level
	private int currentLevel = 1;
	private int score = 0;
	private int bonus = 0;
	private float bonusTime = 0;

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
		ui.ShowHud();
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
					asteroid.transform.Rotate(new Vector3(0, 0, rotate));
					asteroid.transform.Translate(new Vector3(dist, dist));

					asteroidController.MakeLargeAsteroid();
					remainingAsteroids += 3 * 3;
				}
				else {
					Debug.LogWarning("Asteroid pool not returning asteroids");
				}
			}
			else {
				Debug.Log("Empty asteroid pool");
			}
		}

		bonusTime = bonusTimePerLevel * GetLevel();
	}

	void Update() {
		if (bonusTime > 0) {
			bonusTime -= Time.deltaTime;
		}
		else {
			bonusTime = 0;
		}

		bonus = (int) (maxBonus * bonusTime / (float) (bonusTimePerLevel));
		ui.RefreshUi();
	}

	public void OnDestroyAsteroid(int asteroidHealth) {
		if (asteroidHealth <= 0) {
			remainingAsteroids--;
			asteroidHealth = 0;
		}

		score += (3 - asteroidHealth) * bonusPerLargeAsteroid;

		if (remainingAsteroids <= 0) {
			OnLevelComplete();
		}
	}

	public void OnLevelComplete() {
		score += bonus;
		CreateLevel(++currentLevel);
	}

	public int GetLevel() {
		return currentLevel;
	}

	public int GetScore() {
		return score;
	}

	public int GetBonus() {
		return bonus;
	}
}

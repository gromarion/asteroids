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
	private int roundBonus;
	private float bonusTime = 0;
	private float delayTimer;
	private float delay = 2f;
	private GameStatus gameStatus = GameStatus.Normal;

	public enum GameStatus {
		Normal,
		FinishingLevel,
		Dying,
		GameOver
	};

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
		RestartGame();
	}

	public void RestartGame() {
		currentLevel = 1;
		score = 0;
		bonus = 0;
		gameStatus = GameStatus.Normal;

		PoolManager.instance.asteroidPool.RecycleAll();
		CreateLevel(currentLevel);
		ui.ShowHud();
		player.gameObject.SetActive(true);
		player.transform.position = Vector3.zero;
		player.transform.rotation = new Quaternion(0, 0, 0, 1);
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
		if (gameStatus == GameStatus.Normal) {
			if (bonusTime > 0) {
				bonusTime -= Time.deltaTime;
			}
			else {
				bonusTime = 0;
			}

			bonus = (int) (maxBonus * bonusTime / (float) (bonusTimePerLevel));
		}

		// If the player is dead, wait for a bit before showing the game over screen
		if (gameStatus == GameStatus.Dying) {
			delayTimer += Time.deltaTime;
			if (delayTimer >= delay) {
				ui.ShowGameOver();
				gameStatus = GameStatus.GameOver;
			}
		}

		// If there are no more asteroids, wait for a bit before going to the next level
		// Also, make that cool effect where the bonus is added to the score
		if (gameStatus == GameStatus.FinishingLevel) {
			delayTimer += Time.deltaTime;
			int deltaBonus = (int) Mathf.Ceil(((roundBonus / 1.5f) * Time.deltaTime));
			if (deltaBonus < bonus && bonus > 0) {
				bonus -= deltaBonus;
				score += deltaBonus;
			}
			else {
				bonus = 0;
				score += bonus;
			}

			if (delayTimer >= delay) {
				bonus = 0;
				CreateLevel(++currentLevel);
				gameStatus = GameStatus.Normal;
			}
		}

		// Finally, refresh the UI
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

	public void OnPlayerDeath() {
		gameStatus = GameStatus.Dying;
		delayTimer = 0;
		ui.HideAll();
	}

	public void OnLevelComplete() {
		if (gameStatus == GameStatus.Normal) {
			delayTimer = 0;
			roundBonus = bonus;
			gameStatus = GameStatus.FinishingLevel;
		}
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

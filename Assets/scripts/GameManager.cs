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
	private static float MIN_ENEMY_SPAWN_TIME = 10f;
	private static float MAX_ENEMY_SPAWN_TIME = 30f;
	private float enemy_spawn_time;
	private float spent_time_since_last_spawn;
	private bool enemy_spawned;

	public GameUIController ui;
	public PlayerController player;

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
	}

	void Destroy() {
		instance = null;
	}

	void Start () {
		RestartGame();
	}

	public void ResetEnemySpawnTime() {
		enemy_spawn_time = Random.Range(MIN_ENEMY_SPAWN_TIME, MAX_ENEMY_SPAWN_TIME);
		spent_time_since_last_spawn = 0;
		enemy_spawned = false;
	}

	public void RestartGame() {
		currentLevel = 1;
		score = 0;
		bonus = 0;
		gameStatus = GameStatus.Normal;

		PoolManager.instance.asteroidPool.RecycleAll();
		PoolManager.instance.enemyPool.RecycleAll();
		PoolManager.instance.bulletPool.RecycleAll();
		PoolManager.instance.enemyBulletPool.RecycleAll();
		PoolManager.instance.explotionPool.RecycleAll();
		player.RestartPlayer();
		CreateLevel(currentLevel);
		ResetEnemySpawnTime ();
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
		SpawnEnemyWithProbability ();
		ui.RefreshUi();
	}

	public void SpawnEnemyWithProbability() {
		spent_time_since_last_spawn += Time.deltaTime;
		if (spent_time_since_last_spawn >= enemy_spawn_time && !enemy_spawned) {
			enemy_spawned = true;
			GameObject enemy = PoolManager.instance.enemyPool.GetObject();
			if (enemy != null) {
				enemy.SetActive(true);
			}
		}
	}

	public void OnDestroyAsteroid(int asteroidHealth) {
		if (asteroidHealth <= 0) {
			remainingAsteroids--;
			asteroidHealth = 0;
		}

		score += (3 - asteroidHealth) * bonusPerLargeAsteroid;

		if (remainingAsteroids <= 0) {
		//if (remainingAsteroids <= 0 && !enemy_spawned) {
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

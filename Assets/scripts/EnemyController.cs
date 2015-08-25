using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private static float MAX_SPEED = 1f;
	private static float MIN_SPEED = 2f;
	private static float MIN_SHOOTING_TIME = 3f;
	private static float MAX_SHOOTING_TIME = 6f;
	private Vector2 speed;
	private EnemyFireController fireController;
	private float shooting_time;
	private float direction_time;
	private float elapsed_shooting_time;
	private float elapsed_direction_time;

	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		reset ();
		fireController = GetComponent<EnemyFireController> ();
	}
	
	// Update is called once per frame
	void Update () {
		elapsed_shooting_time += Time.deltaTime;
		elapsed_direction_time += Time.deltaTime;
		transform.Translate (speed * Time.deltaTime);
		if (elapsed_shooting_time >= shooting_time) {
			fireController.Fire ();
			resetShootingTime();
		}
		if (elapsed_direction_time >= direction_time) {
			resetSpeed();
		}
	}

	public void Destroy () {
		GameManager.instance.ResetEnemySpawnTime ();
		GameObject explosion = PoolManager.instance.explotionPool.GetObject ();
		explosion.transform.position = transform.position;
		explosion.SetActive (true);
		gameObject.SetActive (false);
		ExplotionController explosionController = explosion.GetComponent<ExplotionController> ();
		explosionController.ShipExplosion ();
		SoundManager.instance.explode ();
	}

	public void reset () {
		resetSpeed ();
		float rand = Random.Range (0f, 1f);
		BoundsController bounds_controller = GetComponent<BoundsController>();
		if (rand > 0.5) {
			transform.position = new Vector3(bounds_controller.CameraHorizontal (), Random.Range(-1 * bounds_controller.CameraVertical (), bounds_controller.CameraVertical ()), 1);
		} else {
			transform.position = new Vector3(-1 * bounds_controller.CameraHorizontal (), Random.Range(-1 * bounds_controller.CameraVertical (), bounds_controller.CameraVertical ()), 1);
		}
	}

	private void resetShootingTime() {
		shooting_time = Random.Range(MIN_SHOOTING_TIME, MAX_SHOOTING_TIME);
		elapsed_shooting_time = 0;
	}

	private void resetDirectionTime() {
		direction_time = Random.Range(MIN_SHOOTING_TIME, MAX_SHOOTING_TIME);
		elapsed_direction_time = 0;
	}

	private void resetSpeed() {
		speed = Random.insideUnitCircle * (Random.Range(MIN_SPEED, MAX_SPEED) + MIN_SPEED);
		resetDirectionTime ();
	}
}

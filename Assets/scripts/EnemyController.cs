using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private static float MAX_SPEED = 3f;
	private static float MIN_SPEED = 3f;
	private Vector2 speed;
	private EnemyFireController fireController;

	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		transform.position = OutOfScreenPosition ();
		speed = Random.insideUnitCircle * Random.Range(MIN_SPEED, MAX_SPEED);
		fireController = GetComponent<EnemyFireController> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (speed * Time.deltaTime);
		if (Random.Range (0f, 1f) > 0.99) {
			fireController.Fire ();
		}
	}

	public void Destroy () {
		gameObject.SetActive(false);
	}

	private Vector3 OutOfScreenPosition () {
		float rand = Random.Range (0, 1);
		if (rand > 0.5) {
			return new Vector3(BoundsController.MAP_SIZE_HORIZONTAL, Random.Range(0, BoundsController.MAP_SIZE_VERTICAL), 1);
		} else {
			return new Vector3(-1 * BoundsController.MAP_SIZE_HORIZONTAL, Random.Range(0, BoundsController.MAP_SIZE_VERTICAL), 1);
		}
	}
}

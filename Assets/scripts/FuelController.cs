using UnityEngine;
using System.Collections;

public class FuelController : MonoBehaviour {
	
	public SpriteRenderer spriteRenderer;
	private Vector2 speed;

	// maybe different fuels with different colors, I don't know, gotta discuss about it
	private int fuel;

	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		transform.Translate(speed * Time.deltaTime);
	}

	public int GetHealth() {
		return health;
	}

	public void heal() {
		// aca no se como conectar con la nave
	}
}

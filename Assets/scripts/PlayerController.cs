using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rigidBody;
	private FireController fireController;

	public float speed = 3.0f;
	public float rotationSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		fireController = GetComponent<FireController> ();
	}
	
	// Update is called once per frame
	void Update () {
		float vertical = Input.GetAxis("Vertical");
		float rotate = Input.GetAxis("Horizontal") * rotationSpeed;
		transform.Rotate(0, 0, 0 - rotate);

		if (Input.GetKeyDown(KeyCode.Space)) {
			fireController.Fire();
		}

		if (vertical > 0) {
			rigidBody.AddForce(transform.up * Time.deltaTime * speed * vertical);
		}
	}
}

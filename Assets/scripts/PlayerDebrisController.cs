using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDebrisController : MonoBehaviour {

	private List<Vector3> originalPositions;
	private List<Quaternion> originalRotations;
	private List<Rigidbody2D> rigidbodies;
	
	public void Explode() {
		if (originalPositions == null) {
			originalPositions = new List<Vector3>();
			originalRotations = new List<Quaternion>();
			rigidbodies = new List<Rigidbody2D>();
			foreach (Transform child in transform) {
				originalPositions.Add(child.transform.localPosition);
				originalRotations.Add(child.transform.localRotation);
				rigidbodies.Add(child.GetComponent<Rigidbody2D>());
			}
		}
		int i = 0;
		foreach (Transform child in transform) {
			child.transform.localPosition = originalPositions[i];
			child.transform.localRotation = originalRotations[i];
			rigidbodies[i].AddForce(child.transform.localPosition.normalized * Random.Range(100, 150));
			rigidbodies[i].AddTorque(Random.Range(-20, 20));
			i++;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireController : MonoBehaviour {

	private ObjectPooler bulletPool;

	void Start() {
		bulletPool = GetComponent<ObjectPooler>();
	}
	
	public void Fire() {
		GameObject obj = ObjectPooler.current.GetObject();

		if (obj != null) {
			obj.transform.position = transform.position;
			obj.transform.rotation = transform.rotation;
			obj.SetActive(true);
		}
	}

}

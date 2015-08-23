using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireController : MonoBehaviour {

	public void Fire() {
		GameObject obj = PoolManager.instance.bulletPool.GetObject();

		if (obj != null) {
			SoundManager.instance.shoot();
			obj.transform.position = transform.position;
			obj.transform.rotation = transform.rotation;
			obj.SetActive(true);
		}
	}

}

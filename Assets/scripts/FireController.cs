using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireController : MonoBehaviour {

	public bool Fire(GameObject shootFrom) {
		GameObject obj = PoolManager.instance.bulletPool.GetObject();
		bool fired = false;

		if (obj != null) {
			SoundManager.instance.shoot();
			if (shootFrom != null) {
				obj.transform.position = shootFrom.transform.position;
				obj.transform.rotation = shootFrom.transform.rotation;
			}
			else {
				obj.transform.position = transform.position;
				obj.transform.rotation = transform.rotation;
			}
			
			obj.SetActive(true);
			fired = true;
		}

		return fired;
	}

	public bool StrongFire(GameObject shootFrom) {
		GameObject obj = PoolManager.instance.bigBulletPool.GetObject();
		bool fired = false;

		if (obj != null) {
			SoundManager.instance.shoot();
			if (shootFrom != null) {
				obj.transform.position = shootFrom.transform.position;
				obj.transform.rotation = shootFrom.transform.rotation;
			}
			else {
				obj.transform.position = transform.position;
				obj.transform.rotation = transform.rotation;
			}
			obj.SetActive(true);
			fired = true;
		}

		return fired;
	}

}

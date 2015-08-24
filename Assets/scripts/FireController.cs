using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireController : MonoBehaviour {

	public void Fire(GameObject shootFrom) {
		GameObject obj = PoolManager.instance.bulletPool.GetObject();

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
		}
	}

	public void StrongFire(GameObject shootFrom) {
		GameObject obj = PoolManager.instance.bigBulletPool.GetObject();

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
		}
	}

}

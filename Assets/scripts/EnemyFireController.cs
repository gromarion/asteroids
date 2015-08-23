using UnityEngine;
using System.Collections;

public class EnemyFireController : MonoBehaviour {

	public void Fire() {
		GameObject obj = PoolManager.instance.enemyBulletPool.GetObject();
		
		if (obj != null) {
			GameObject player = GameObject.FindWithTag ("Player");
			if (player != null) {
				SoundManager.instance.shoot();
				Vector3 target = player.transform.position;
				Vector3 direction = transform.position - target;
				direction.Normalize();
				Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 90);
				obj.transform.rotation = rotation;
				obj.transform.position = transform.position;
				obj.SetActive(true);
			}
		}
	}
}

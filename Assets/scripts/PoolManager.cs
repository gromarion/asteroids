using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour {

	public static PoolManager instance;
	public ObjectPooler bulletPool;
	public ObjectPooler bigBulletPool;
	public ObjectPooler asteroidPool;
	public ObjectPooler explotionPool;
	public ObjectPooler enemyPool;
	public ObjectPooler enemyBulletPool;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
		
		else if (instance != this) {
			Destroy(gameObject); 
		}

		//DontDestroyOnLoad(gameObject);
	}

	void Destroy() {
		instance = null;
	}
}

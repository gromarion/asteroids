using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour {

	public static PoolManager instance;
	public ObjectPooler bulletPool;
	public ObjectPooler asteroidPool;
	public ObjectPooler explotionPool;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
		
		else if (instance != this) {
			Destroy(gameObject); 
		}

		DontDestroyOnLoad(gameObject);
	}
}

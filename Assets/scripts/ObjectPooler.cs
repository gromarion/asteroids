using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler current;
	public GameObject pooledObject;
	public int pooledAmount = 20;
	public bool willGrow = false;

	private List<GameObject> pooledObjects;

	void Awake() {
		current = this;
		pooledObjects = new List<GameObject>();

		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj = (GameObject) Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetObject() {
		GameObject obj = null;
		bool found = false;
		for (int i = 0; i < pooledObjects.Count && !found; i++) {
			if (!pooledObjects[i].activeInHierarchy) {
				obj = pooledObjects[i];
				found = true;
			}
		}

		if (!found && willGrow) {
			obj = (GameObject) Instantiate(pooledObject);
			pooledObjects.Add(obj);
		}

		return obj;
	}

	public void RecycleAll() {
		for (int i = 0; i < pooledObjects.Count; i++) {
			pooledObjects[i].SetActive(false);
		}
	}

	public int getAvailableObjects() {
		int count = 0;
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeInHierarchy) {
				count++;
			}
		}
		return count;
	}
}

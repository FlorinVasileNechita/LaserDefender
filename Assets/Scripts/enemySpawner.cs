using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySpawner : MonoBehaviour {

	private float xMin;
	private float xMax;

	public Transform childTo;
	public GameObject enemy1Prefab;

	private float spawnRate = GameConstants.ENEMY_SPAWN_RATE;
	private float spawner = 0;

	private List<GameObject> shipList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		//Capturing the camera perspective so we can restrict it later
		float distance = transform.position.z - Camera.main.transform.position.z;
		
		Vector3 lowerLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 upperRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));

		xMin = lowerLeft.x;
		xMax = lowerLeft.y;
	}
	
	// Update is called once per frame
	void Update () {
	
		//This is the simple case, where we have them only spawning and coming down.
		spawner += Time.deltaTime;
		if (spawner > spawnRate && shipList.Count < 10) {
			Vector3 position = new Vector3(Random.Range (xMin,xMax),7,this.transform.position.z);
			GameObject myEnemy = Instantiate (enemy1Prefab) as GameObject;
			myEnemy.transform.parent = childTo;
			myEnemy.transform.position = position;
			spawner = 0;
			shipList.Add (myEnemy);
		}

	}
}

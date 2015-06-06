using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySpawner : MonoBehaviour {

	private float xMin;
	private float xMax;
	private float timeSinceLastSpawn;

	public Transform childTo;

	//Existing Formations
	public GameObject pentagonFormation;
	public GameObject vFormation;
	public GameObject WFormation;

	private GameObject formation;
	public GameObject enemyShipPrefab;
	
	// Use this for initialization
	void Start () {
		timeSinceLastSpawn = 0f;
		spawnEnemies ();
	}

	void Update(){
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= GameConstants.ENEMY_SPAWN_RATE) {
			timeSinceLastSpawn = 0;
			spawnEnemies ();
		}
	}

	void spawnEnemies(){
		formation = getRandomFormation ();
		Transform transform = (Instantiate (formation) as GameObject).transform;
		foreach (Transform child in transform) {
			GameObject enemyShip = Instantiate (enemyShipPrefab) as GameObject;
			enemyShip.transform.parent = childTo;
			enemyShip.transform.position = child.transform.position;
			enemyShip.tag = formation.gameObject.name;
			Destroy(child.gameObject);
		}
	}

	GameObject getRandomFormation(){
		int lengthEnum = GameConstants.NUM_OF_FORMATIONS; //Going to have to do this manually?
		int value = Random.Range (0, lengthEnum);
		switch (value) {
		case 2:
			return WFormation;
		case 1:
			return vFormation;
		default:
			return pentagonFormation;
		}
	}
}

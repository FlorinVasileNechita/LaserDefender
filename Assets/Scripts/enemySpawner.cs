using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySpawner : MonoBehaviour {

	//xMin and xMax will be used when I start randomizing the position of solo ships
	private float xMin;
	private float xMax;

	//time since last spawn to control the waves of enemies.
	private float timeSinceLastSpawn;

	//Declares what parent to place the enemies in
	public Transform childTo;

	//Existing Formations
	public GameObject pentagonFormation;
	public GameObject vFormation;
	public GameObject WFormation;
	private GameObject formation; //We need this to randomize which formation comes later

	private bool chosenDirection;

	public GameObject enemyShipPrefab; //The Enemy ship prefab.
	//TODO: Add more enemies.

	void Start () {
		timeSinceLastSpawn = 0f;
		spawnEnemies ();
	}


	void Update(){
		//Controlling the spawn of enemies.
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= GameConstants.ENEMY_SPAWN_RATE) {
			timeSinceLastSpawn = 0;
			spawnEnemies ();
		}
	}

	/// <summary>
	/// Spawns the enemies.
	/// </summary>
	void spawnEnemies(){
		formation = getRandomFormation ();
		chosenDirection = getDirection ();
		Transform transform = (Instantiate (formation) as GameObject).transform;
		foreach (Transform child in transform) {
			GameObject enemyShip = Instantiate (enemyShipPrefab) as GameObject;
			enemyShip.transform.parent = childTo;
			enemyShip.transform.position = child.transform.position;
			enemyShip.tag = formation.gameObject.name;

			enemyShip.GetComponent<enemyControlScript>().moveRight = chosenDirection;
			Destroy(child.gameObject);

		}
	}

	/// <summary>
	/// Gets the random formation.
	/// </summary>
	/// <returns>A GameObject representing a formation.</returns>
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

	bool getDirection(){
		float r = Random.value;
		if (r >= 0.5) {
			return true;
		} else {
			return false;
		}
	}
}

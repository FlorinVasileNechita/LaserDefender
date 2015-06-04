using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySpawner : MonoBehaviour {

	private float xMin;
	private float xMax;

	public Transform childTo;

	public GameObject pentagonFormation;
	public GameObject vFormation;
	private GameObject formation;
	public GameObject enemyShipPrefab;
	
	// Use this for initialization
	void Start () {

		formation = getRandomFormation ();
		Transform transform = (Instantiate (formation) as GameObject).transform;
		foreach (Transform child in transform) {
			GameObject enemyShip = Instantiate (enemyShipPrefab) as GameObject;
			enemyShip.transform.parent = childTo;
			enemyShip.transform.position = child.transform.position;
			enemyShip.tag = formation.name;
			}
	}

	GameObject getRandomFormation(){
		int lengthEnum = GameConstants.NUM_OF_FORMATIONS; //Going to have to do this manually?
		int value = Random.Range (0, lengthEnum);
		switch (value) {
		case 1:
			return vFormation;
		default:
			return pentagonFormation;
		}
	}
}

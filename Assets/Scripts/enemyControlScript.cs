using UnityEngine;
using System.Collections;

public class enemyControlScript : MonoBehaviour {

	private float xmin;
	private float xmax;
	private float ymin;
	private float ymax;

	private float spriteWidth;
	private float spriteHeight;

	Vector3 shipPosition;
	string direction = "left";

	// Use this for initialization
	void Start () {
		//Capturing the camera perspective so we can restrict it later
		float distance = transform.position.z - Camera.main.transform.position.z;

		Vector3 lowerLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 upperRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));

		//Getting the width and height of the sprite so we can make the ship display properly.
		spriteWidth = GetComponent<Renderer> ().bounds.size.x / 2;
		spriteHeight = GetComponent<Renderer> ().bounds.size.y / 2;

		xmin = lowerLeft.x + spriteWidth;
		xmax = upperRight.x - spriteWidth;
		ymin = lowerLeft.y + spriteHeight;
		ymax = upperRight.y - spriteHeight;



		shipPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Storing this for now because... well we might want this motion later.
		zigzag ();


	}

	void zigzag(){
		if (direction == "left" && shipPosition.x < xmax) {
			shipPosition.x += GameConstants.ENEMY_HORZ_SPEED * Time.deltaTime;
		} else if (shipPosition.x >= xmax) {
			direction = "right";
		}
		if (direction == "right" && shipPosition.x > xmin) {
			shipPosition.x -= GameConstants.ENEMY_HORZ_SPEED * Time.deltaTime;
		} else if (shipPosition.x <= xmin) {
			direction = "left";
		}
		shipPosition.y -= GameConstants.ENEMY_VERT_SPEED * Time.deltaTime;
		this.transform.position = shipPosition;
	}

}

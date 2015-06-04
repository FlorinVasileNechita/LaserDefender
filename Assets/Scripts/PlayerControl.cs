using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	Vector3 position;
	public GameObject laserPrefab;
	public Transform childTo;

	private Vector3 leftBottom;
	private Vector3 rightTop;
	private float spriteWidth;
	private float spriteHeight;

	private float fireDelay = 0;

	//Movement Speed. We want this to be "constant". We'll lock it down permanently once we determine it.
	//This is a lot higher because we made it frame independent.
	public float MOVEMENT_SPEED = GameConstants.PLAYER_MOVEMENT_SPEED;

	// Use this for initialization
	void Start () {
	
		//Capturing the camera perspective so we can restrict it later
		float distance = transform.position.z - Camera.main.transform.position.z;
		leftBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		rightTop = Camera.main.ViewportToWorldPoint (new Vector3 (1,1,distance));

		//Getting the width and height of the sprite so we can make the ship display properly.
		spriteWidth = GetComponent<Renderer> ().bounds.size.x / 2;
		spriteHeight = GetComponent<Renderer> ().bounds.size.y / 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Movement support for WASD
		position = this.transform.position;
		if (Input.GetKey(KeyCode.W)) {
			position.y+= MOVEMENT_SPEED * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A)) {
			position.x-= MOVEMENT_SPEED * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S)) {
			position.y-= MOVEMENT_SPEED * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D)) {
			position.x+= MOVEMENT_SPEED * Time.deltaTime;
		}

		//TODO: Fix the Diagonal double speed problem

		//Clamping the position so the plane doesn't go off the window
		position.x = Mathf.Clamp(position.x, leftBottom.x + spriteWidth, rightTop.x - spriteWidth);
		position.y = Mathf.Clamp (position.y,leftBottom.y + spriteHeight,rightTop.y - spriteHeight);
		this.transform.position = position;

		//On Mouse Click, fire laser.
		if (Input.GetMouseButtonDown (0)) {
			fireLaser();
		}

		//On Holding the Mouse button down, fire the laser with a delay between each round.
		if (Input.GetMouseButton (0)) {
			fireDelay += Time.deltaTime;
			if (fireDelay > GameConstants.FIRE_DELAY) {
				fireLaser ();
				fireDelay = 0;
			}
		} else {
			fireDelay = 0;
		}

	}

	void fireLaser(){
		GameObject laser = Instantiate (laserPrefab) as GameObject;
		laser.transform.parent = childTo;
		Vector3 laserPosition = position;
		laserPosition.y += spriteHeight;
		laser.transform.position = laserPosition;
	}
}

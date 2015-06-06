using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	Vector3 position; //The vector3 used to control the ships position.
	public GameObject laserPrefab;
	public GameObject explosionPrefab;
	public Transform childTo;

	public LevelManager levelManager;

	private Vector3 leftBottom;
	private Vector3 rightTop;
	private float spriteWidth;
	private float spriteHeight;

	private float fireDelay = 0;

	//Player Health
	public float health = 100f;


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

	/// <summary>
	/// Fires the laser.
	/// </summary>
	void fireLaser(){
		GameObject laser = Instantiate (laserPrefab) as GameObject;
		laser.transform.parent = childTo;
		Vector3 laserPosition = position;
		laserPosition.y += spriteHeight;
		laser.transform.position = laserPosition;
	}


	/// <summary>
	/// Checks for collisions and decrements health when appropriate
	/// </summary>
	/// <param name="col">Collider/param>
	void OnTriggerEnter2D(Collider2D col){
		enemyLaserControl enemyLaser = col.gameObject.GetComponent<enemyLaserControl> ();
		enemyControlScript enemyShip = col.gameObject.GetComponent<enemyControlScript> ();
		if (enemyLaser) {
			Destroy(col.gameObject);
			health -= GameConstants.ENEMY_LASER_DAMAGE;
			checkAlive();
		}
		if (enemyShip) {
			Destroy(col.gameObject);
			health -= GameConstants.ENEMY_SHIP_DAMAGE;
			checkAlive();
		}
	}

	/// <summary>
	/// Checks if Alive.
	/// </summary>
	void checkAlive(){
		if (health <= 0) {
			GameObject explosion = Instantiate(explosionPrefab) as GameObject;
			explosion.transform.position = this.transform.position;
			Destroy (this.GetComponent<SpriteRenderer>());
			Destroy (this.GetComponent<PolygonCollider2D>());
			Invoke("loadNextLevel",1);
		}
	}

	/// <summary>
	/// Loads the next level.
	/// </summary>
	void loadNextLevel(){
		levelManager.LoadLevel("Win Screen");
	}

}

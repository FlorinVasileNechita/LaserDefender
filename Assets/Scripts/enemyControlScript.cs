using UnityEngine;
using System.Collections;

public class enemyControlScript : MonoBehaviour {

	public Transform childTo;
	public GameObject enemyLaserPrefab;
	public GameObject explosionPrefab;
	public GameObject enemyDestroyer;

	private float xmin;
	private float xmax;
	private float ymin;
	private float ymax;
	private float width;

	public bool canFire = false;

	private Random rand = GameConstants.rand;

	private float spriteWidth;
	private float spriteHeight;

	//Tracking the starting X
	private float startX;

	Vector3 shipPosition;
	bool moveRight = true;

	// Use this for initialization
	void Start () {
		//Capturing the camera perspective so we can restrict it later
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightTop = Camera.main.ViewportToWorldPoint (new Vector3 (1,1,distance));

		enemyDestroyer = GameObject.Find ("EnemyDestroyer");

		if (this.tag == "PentagonFormation") {
			width = GameConstants.PENTAGON_WIDTH / 2;
		} else if (this.tag == "VFormation") {
			width = GameConstants.V_WIDTH/2;
		} else if (this.tag == "WFormation") {
			width = GameConstants.W_WIDTH/2;
		}


		xmin = leftBottom.x + width; xmax = rightTop.x - width;
		ymax = rightTop.y; ymin = leftBottom.y;



		//Getting the width and height of the sprite so we can make the ship display properly.
		spriteWidth = GetComponent<Renderer> ().bounds.size.x / 2;
		spriteHeight = GetComponent<Renderer> ().bounds.size.y / 2;

		//Creating a Vector3 position
		shipPosition = this.transform.position;
		startX = shipPosition.x;
	}
	
	// Update is called once per frame
	void Update () {
		//Moving the ships vertically
		shipPosition.y -= GameConstants.ENEMY_VERT_SPEED * Time.deltaTime;

		//Moving the ships horizontally.
		if (moveRight) {
			shipPosition.x += GameConstants.ENEMY_HORZ_SPEED * Time.deltaTime;
		} else {
			shipPosition.x -= GameConstants.ENEMY_HORZ_SPEED * Time.deltaTime;
		}
		this.transform.position = shipPosition;

		//Changing Directions
		if (shipPosition.x > (xmax + startX)) {
			moveRight = false;
		} else if (shipPosition.x < (xmin + startX)) {
			moveRight = true;
		}

		//Random Firing.
		float probability = Time.deltaTime * GameConstants.ENEMY_FIRE_RATE;
		if (Random.value < probability && this.transform.position.y < ymax) {
			canFire = true;
			fireLaser();
			probability = 0;
		}

	}

	void fireLaser(){
		childTo = GameObject.Find ("LaserHolder").transform;
		GameObject laser = Instantiate (enemyLaserPrefab) as GameObject;
		laser.transform.parent = childTo;
		Vector3 laserPosition = this.transform.position;
		laserPosition.y -= spriteHeight;
		laser.transform.position = laserPosition;

	}

	void OnTriggerEnter2D(Collider2D col){
		playerLaserControl playerLaser = col.gameObject.GetComponent<playerLaserControl> ();
		Destroyer enemyDestroyer = col.gameObject.GetComponent<Destroyer>();
		if (playerLaser) {
			GameObject explosion = Instantiate(explosionPrefab) as GameObject;
			explosion.transform.position = this.transform.position;
			Destroy (col.gameObject);
			Destroy (this.gameObject);
		}
		if (enemyDestroyer) {
			Destroy(this.gameObject);
		}
	}
}

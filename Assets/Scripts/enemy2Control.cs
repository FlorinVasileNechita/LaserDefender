using UnityEngine;
using System.Collections;

public class enemy2Control : MonoBehaviour {

	public Transform childTo;
	public GameObject enemyLaserPrefab;
	public GameObject explosionPrefab;
	public ScoreKeeper scoreboard;
	private float health;
	private AudioSource hit;
	private Animator animator;
	private Vector2 dir;
	private Random rand;
	private Vector3 position;
	private float timeEllapsed;
	private float spriteWidth;
	private float spriteHeight;
	private float maxX;
	private float maxY;
	private float minX;
	private float minY;
	private bool startMoving;


	// Use this for initialization
	void Start () {
		//Locking the ships movement until the animation is finished.
		startMoving = false;

		//Capturing the camera perspective so we can restrict it later
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightTop = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));

		//Gets the animator to see when the intro animation is completed.
		animator = this.GetComponent<Animator> ();

		//Time Ellapsed since we got the new random direction
		timeEllapsed = 0f;

		//Getting the width and height of the sprite to create the valid bounds of the ship's movement
		spriteWidth = this.GetComponent<SpriteRenderer> ().bounds.size.x;
		spriteHeight = this.GetComponent<SpriteRenderer> ().bounds.size.y;

		//Establishing the bounds of the ships movement.
		maxX = rightTop.x - spriteWidth/2;
		minX = leftBottom.x + spriteWidth/2;
		maxY = rightTop.y - spriteHeight/2;
		minY = leftBottom.y + spriteHeight/2 + 2;


		//Establishing the first direction
		dir = getNewDirection();
		position = this.transform.position;

		//Setting up the Health
		health = 50f;
		hit = this.GetComponent<AudioSource> ();

		//Getting the ScoreKeeper
		scoreboard = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	
	}
	
	// Update is called once per frame
	void Update () {
		/*	The way this code segment is going to work is as follows:
		 * 		Every update cycle, it will look at the startMoving flag
		 * 		This flag is instantiated as false because I don't want the ship to move until it's done animating
		 * 		Once that intro animation has been played, the animation will move into "Idle" state
		 * 		It's at this point that startMoving will be changed to "true" and the animator will be deleted from the object.
		 */
		if (startMoving) {
			if(!this.gameObject.GetComponent<PolygonCollider2D>().enabled){
				this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
			}
			timeEllapsed += Time.deltaTime;
			if (timeEllapsed > GameConstants.ENEMY2_MOVEMENT_DELAY) {
				dir = getNewDirection ();
				timeEllapsed = 0f;
			}
			if (position.x <= minX || position.x >= maxX) {
				dir.x *= -1;
			}
			if (position.y <= minY || position.y >= maxY) {
				dir.y *= -1;
			}
			position.x += dir.x * Time.deltaTime;
			position.y += dir.y * Time.deltaTime;
			this.transform.position = position;

			//Firing
			float probability = Time.deltaTime * GameConstants.ENEMY_FIRE_RATE;
			if (Random.value < probability) {
				fireLaser();
				probability = 0;
			}
		} else {
			this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				startMoving = true;
				Destroy (animator);
			}
		}
	}

	/// <summary>
	/// Gets the new direction.
	/// </summary>
	/// <returns>The new direction.</returns>
	Vector2 getNewDirection(){
		Vector2 newDir;
		newDir.x = Random.Range (GameConstants.ENEMY2_MIN,GameConstants.ENEMY2_MAX);
		newDir.y = Random.Range (GameConstants.ENEMY2_MIN,GameConstants.ENEMY2_MAX);
		return newDir;
	}

	/// <summary>
	/// Fires the laser.
	/// </summary>
	void fireLaser(){
		childTo = GameObject.Find ("LaserHolder").transform;
		GameObject laser = Instantiate (enemyLaserPrefab) as GameObject;
		laser.transform.parent = childTo;
		Vector3 laserPosition = this.transform.position;
		laserPosition.y -= spriteHeight;
		laser.transform.position = laserPosition;
	}

	/// <summary>
	/// Raises the trigger enter2d event.
	/// Both when the player laser hits the ship and when they hit the garbage collector
	/// </summary>
	/// <param name="col">Collider</param>
	void OnTriggerEnter2D(Collider2D col){
		playerLaserControl playerLaser = col.gameObject.GetComponent<playerLaserControl> ();
		Destroyer enemyDestroyer = col.gameObject.GetComponent<Destroyer>();
		if (playerLaser) {
			Destroy (col.gameObject); //This destroys the player laser.
			hit.Play ();
			health -= GameConstants.PLAYER_DAMAGE;
			if (health <=0){
				GameObject explosion = Instantiate(explosionPrefab) as GameObject;
				explosion.transform.position = this.transform.position;
				scoreboard.addScore(GameConstants.ENEMY_TWO_VALUE);
				Destroy (this.gameObject);
			}
		}
	}


}

using UnityEngine;
using System.Collections;

public class enemy2Control : MonoBehaviour {

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
		minY = leftBottom.y + spriteHeight/2;
		minX = leftBottom.x + spriteWidth/2;
		maxY = rightTop.y - spriteHeight/2;

		//Establishing the first direction
		dir = getNewDirection();
		position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		/*	The way this code segment is going to work is as follows:
		 * 		Every update cycle, it will look at the startMoving flag
		 * 		This flag is instantiated as false because I want that intro movement
		 * 		Once that intro animation has been played, the animation will move into "Idle"
		 * 		
		 */
		if (startMoving) {
			timeEllapsed += Time.deltaTime;
			if (timeEllapsed > 1.5f) {
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
			Debug.Log (this.transform.position);
		} else {
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				startMoving = true;
				Destroy (animator);
			}
		}
	}

	Vector2 getNewDirection(){
		Vector2 newDir;
		newDir.x = Random.Range (-5f, 5f);
		newDir.y = Random.Range (-5f, 5f);
		Debug.Log (newDir);
		return newDir;
	}
}

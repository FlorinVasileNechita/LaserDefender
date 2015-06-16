using UnityEngine;
using System.Collections;

public class enemy2Control : MonoBehaviour {

	private Animator animator;
	private Vector2 dir;
	private Random rand;
	private Vector2 position;
	private float timeEllapsed;
	private float spriteWidth;
	private float spriteHeight;
	private float maxX;
	private float maxY;
	private float minX;
	private float minY;

	// Use this for initialization
	void Start () {

		//Capturing the camera perspective so we can restrict it later
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightTop = Camera.main.ViewportToWorldPoint (new Vector3 (1,1,distance));



		Debug.Log (minY);
		Debug.Log (maxY);


		animator = this.GetComponent<Animator> ();

		timeEllapsed = 0f;

		spriteWidth = this.GetComponent<SpriteRenderer> ().bounds.size.x;
		spriteHeight = this.GetComponent<SpriteRenderer> ().bounds.size.y;

		maxX = rightTop.x - spriteWidth/2;
		minY = leftBottom.y + spriteHeight/2;
		minX = leftBottom.x + spriteWidth/2;
		maxY = rightTop.y - spriteHeight/2;

		dir = getNewDirection();
		position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetCurrentAnimatorStateInfo (1).IsName ("Idle")) {
			animator.StopPlayback();
			timeEllapsed += Time.deltaTime;
			if (timeEllapsed > 1.5f) {
				dir = getNewDirection ();
				timeEllapsed = 0f;
			} else {
				if (position.x <= minX || position.x >= maxX) {
					dir.x *= -1;
				}
				if (position.y <= minY || position.y >= maxY) {
					dir.y *= -1;
				}
				position.x += dir.x * Time.deltaTime;
				position.y += dir.y * Time.deltaTime;
			}
			this.transform.position = position;
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

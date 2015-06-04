using UnityEngine;
using System.Collections;

public class playerLaserControl : MonoBehaviour {

	private float laserVelocity = GameConstants.LASER_MOVEMENT_SPEED;
	private Vector3 position;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().Play ();
		position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		position.y += laserVelocity * Time.deltaTime;
		this.transform.position = position;

	}

	//Destroying any laser that hits the Destroyer
	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.name != "PlayerShip") {
			Destroy (gameObject);
		}
	}

}

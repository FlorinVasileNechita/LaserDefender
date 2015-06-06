using UnityEngine;
using System.Collections;

public class enemyLaserControl : MonoBehaviour {

	private float laserVelocity = GameConstants.LASER_MOVEMENT_SPEED;
	private Vector3 position;

	void Start () {
		GetComponent<AudioSource> ().Play ();
		position = this.transform.position;
	}

	void Update () {
		position.y -= laserVelocity * Time.deltaTime;
		this.transform.position = position;
		
	}
	
	//Destroying any laser that hits the Destroyer
	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.name == "PlayerShip" || collision.gameObject.name == "EnemyDestroyer") {
			Destroy (gameObject);
		}
	}
}

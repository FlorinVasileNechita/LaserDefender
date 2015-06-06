using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	float ymin;

	//This just needs to exist for the ships to be destroyed when they hit the bottom of the screen really.
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		ymin = leftBottom.y - this.GetComponent<BoxCollider2D>().size.y / 2 - GameConstants.DESTROYER_BUFFER;
		this.transform.position = new Vector3 (0, ymin, 0);
	}



}

using UnityEngine;
using System.Collections;

public class BackgroundSize : MonoBehaviour {

	private float maxX;
	private float maxY;
	private float minX;
	private float minY;

	// Use this for initialization
	void Start () {
		//Capturing the camera perspective so we can restrict it later
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightTop = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));

		minX = leftBottom.x;
		maxX = rightTop.x;
		minY = leftBottom.y;
		maxY = rightTop.y;

		setTransform (minX,minY,maxX,maxY);

	}
	
	void setTransform(float minX,float minY,float maxX, float maxY){
		this.gameObject.transform.position = new Vector3 (0, maxY + 1, 5); //It has to be 5 because we want it to be behind the items
		this.gameObject.transform.localScale = new Vector3 (Mathf.Abs (minX) + maxX, 1,1);
	}
}

using UnityEngine;
using System.Collections;

public class explosionControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Makes it so the explosion deletes itself when the particle display is complete.
	void Update () {
		if (!GetComponent<ParticleSystem> ().IsAlive()) {
			Destroy(this.gameObject);
		}
	}
}

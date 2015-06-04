using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	// Use this for initialization
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (this.transform.position, 0.5f);
	}
}

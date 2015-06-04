using UnityEngine;
using System.Collections;

public class FormationWidth : MonoBehaviour {

	private string parent;
	private int sides;
	
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(this.transform.position, new Vector3(5f,5f));
	}
}

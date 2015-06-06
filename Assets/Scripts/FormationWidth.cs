using UnityEngine;
using System.Collections;

public class FormationWidth : MonoBehaviour {

	private string parent;
	private int sides;

	//This is purely for use of the developer. It should not exist on any prefabs once the width is determined.
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(this.transform.position, new Vector3(7f,5f));
	}
}

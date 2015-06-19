using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoseScreenScore : MonoBehaviour {

	private Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		int score = ScoreKeeper.getScore ();
		scoreText.text = "Score: " + score.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

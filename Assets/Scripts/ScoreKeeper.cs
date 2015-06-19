using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	Text scoreText;
	private static int playerScore;

	// Use this for initialization
	void Start () {
		playerScore = 0;
		scoreText = gameObject.GetComponent<Text> ();
		scoreText.text = "Score: 0";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addScore(int score){
		playerScore += score;
		scoreText.text = "Score: " + playerScore.ToString ();
	}

	public static int getScore(){
		return playerScore;
	}

}

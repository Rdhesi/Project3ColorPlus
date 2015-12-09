using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameOverScene : MonoBehaviour {
	public Text scoreUI;
	public Text results;
	// Use this for initialization
	void Start () {
		scoreUI.text = "Final Score: " + Field.score;
		if (GameController.isWin == true) {
			results.color = Color.green;
			results.text = "You Win";
		} else {
			results.color = Color.red;
			results.text = "You Lose";
		}

		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

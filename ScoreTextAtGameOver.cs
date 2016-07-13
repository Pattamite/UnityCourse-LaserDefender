using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTextAtGameOver : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Text ScoreText = GetComponent<Text>();
		ScoreText.text = ScoreKeeper.score.ToString();
		ScoreKeeper.Reset();
	}
	
}

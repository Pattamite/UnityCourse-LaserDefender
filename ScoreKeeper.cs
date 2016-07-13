using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour 
{
	public static int score = 0;
	private Text ScoreText;
	
	void Start()
	{
		ScoreText = GetComponent<Text>();
		Reset();
		ScoreText.text = score.ToString();
	}
	
	public void Score(int value)
	{
		score += value;
		ScoreText.text = score.ToString();
	}
	
	public static void Reset()
	{
		score = 0;
	}
}

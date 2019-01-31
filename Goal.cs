using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

	public Text G_ScoreText;
	public Text G_ResultText;
	public Text G_BackToMainMenuText;
	public Text G_PlayAgain;
	public static int G_Score1 = 0;
	public static int G_Score2 = 0;
	public static bool G_HaveWinner;

	private void Start()
	{
		G_HaveWinner = false;
		if (gameObject.tag.Equals("GoalPlayer1"))
		{
			G_ScoreText.text = G_Score2.ToString();
		}
		else if (gameObject.tag.Equals("GoalPlayer2"))
		{
			G_ScoreText.text = G_Score1.ToString();
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("FlagPlayer1") && gameObject.tag.Equals("GoalPlayer2"))
		{
			if (!G_HaveWinner)
			{
				G_Score1++;
				G_ScoreText.text = G_Score1.ToString();
			}
			if (G_Score1 < 3)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			else
			{
				G_HaveWinner = true;
				Cursor.visible = true;
				G_ResultText.enabled = true;
				G_BackToMainMenuText.enabled = true;
				G_PlayAgain.enabled = true;
				G_ResultText.text = "Player 1 has won!";
				G_BackToMainMenuText.text = "Back To Main Menu";
				G_PlayAgain.text = "Rematch!";
			}

		}
		else if (col.gameObject.tag.Equals("FlagPlayer2") && gameObject.tag.Equals("GoalPlayer1"))
		{
			if (!G_HaveWinner)
			{
				G_Score2++;
				G_ScoreText.text = G_Score2.ToString();
			}
			if (G_Score2 < 3)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			else
			{
				G_HaveWinner = true;
				Cursor.visible = true;
				G_ResultText.enabled = true;
				G_BackToMainMenuText.enabled = true;
				G_PlayAgain.enabled = true;
				G_ResultText.text = "Player 2 has won!";
				G_BackToMainMenuText.text = "BackToMainMenu";
				G_PlayAgain.text = "Rematch!";
				
			}
		}
	}

	public void G_Rematch()
	{
		G_Score1 = 0;
		G_Score2 = 0;
		G_HaveWinner = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void G_BackToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}

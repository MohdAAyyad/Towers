using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LM_LoadScene(string SceneName)
	{
		SceneManager.LoadScene(SceneName);
	}

	public void LM_Quit()
	{
		Application.Quit();
	}

}

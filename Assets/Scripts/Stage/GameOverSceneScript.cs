using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverSceneScript : MonoBehaviour {

	public void restart(){
		GameManagerScript.playerNum = 3;
		GameManagerScript.Save ();

		SceneManager.LoadScene ("StageSelect");
	}
}

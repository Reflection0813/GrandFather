using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StageClearScript : MonoBehaviour {
	public GameObject gameClearBack;

	void Start(){
		Sound.LoadBgm ("clear", "congrats");
	}

	public void stageClear(){
		gameClearBack.SetActive (true);
		gameClearBack.GetComponent<FadeInSpriteScript> ().Play ();
		Sound.PlayBgm ("clear");
		Invoke ("nextStage",gameClearBack.GetComponent<FadeInSpriteScript> ().duration );

	}

	private void nextStage(){
		GameManagerScript.stageClear [SceneManager.GetActiveScene().buildIndex] = true;
		GameManagerScript.Save ();
		SceneManager.LoadScene("StageSelect");
	}
}

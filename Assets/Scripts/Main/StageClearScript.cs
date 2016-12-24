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
		//ステージクリアのセーブについてはこのスクリプトのみが受け持つ
		PlayerPrefs.SetInt ("clearedStage", SceneManager.GetActiveScene().buildIndex);
		GameManagerScript.Save ();
		SceneManager.LoadScene("StageSelect");
	}
}

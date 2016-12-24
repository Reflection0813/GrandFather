using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using System.Collections;

public class GameOverScript : MonoBehaviour {
	private float fadeAlpha = 0;
	private float interval = 1;
	bool isFading = false;
	public Text gameOverText;
	public Image gameOverImage;

	private bool gameOverFlag = false;

	public Button continueButton;
	public Button quitButton;

	// Use this for initialization
	void Start () {
		gameOverText.color = new Color (255, 255, 255, 0);
		gameOverImage.color = new Color (0, 0, 0, 0);


		Sound.LoadBgm ("end", "end");
	}

	void Update(){
		/*
		if (gameOverFlag) {
			if(Input.GetMouseButtonUp(0))
				SceneManager.LoadScene("StageSelect");
		}*/
	}

	void OnGUI(){
		if (!this.isFading)
			return;
		
			Color Wcolor = new Color (255, 255, 255, this.fadeAlpha);
			Color Bcolor = new Color (0, 0, 0, this.fadeAlpha);
			gameOverText.color = Wcolor;
			if (this.fadeAlpha < 0.7f)
				gameOverImage.color = Bcolor;
		

	}

	public void GameOver(){
		if (!gameOverFlag) {
			//Time.timeScale = 0;

			if (Advertisement.IsReady()) {
			//	Advertisement.Show ();
			}

			gameOverFlag = true;
			StartCoroutine (TransScene ());
			Sound.PlayBgm ("end");

			if (GameManagerScript.playerNum <= 0) {
				SceneManager.LoadScene ("GameOver");
			} else {
				continueButton.gameObject.SetActive (true);
				quitButton.gameObject.SetActive (true);
			}

		}

	}

	public void continueStage(){
		//ゲームオーバーでコンティニューボタンを選んだ際、チェックポイントを通過していれば
		//checkPointというキーに１を代入しておく
		//これは、ステージセレクト画面からステージの内容へと飛ぶ際に0を代入する。
		if (GameManagerScript.passedCheckPoint) {
			PlayerPrefs.SetInt ("checkPoint", 1);
		}

		GameManagerScript.playerNum -= 1;
		GameManagerScript.Save();
		SceneManager.LoadScene ("stage" + GameManagerScript.playingStage.ToString());
	}

	public void quitStage(){

		SceneManager.LoadScene ("World1");
	}

	public bool getGameOverFlag(){
		return gameOverFlag;
	}



	private IEnumerator TransScene(){
		isFading = true;
		float time = 0;
		while (time <= interval) {
			this.fadeAlpha = Mathf.Lerp (0f, 1f, time / interval);
			time += Time.deltaTime;
			yield return 0;
		}
	}



}

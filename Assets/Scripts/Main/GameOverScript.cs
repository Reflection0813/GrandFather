using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScript : MonoBehaviour {
	private float fadeAlpha = 0;
	private float interval = 1;
	bool isFading = false;
	public Text gameOverText;
	public Image gameOverImage;

	private bool gameOverFlag = false;

	// Use this for initialization
	void Start () {
		gameOverText.color = new Color (255, 255, 255, 0);
		gameOverImage.color = new Color (0, 0, 0, 0);


		Sound.LoadBgm ("end", "end");
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
			gameOverFlag = true;
			StartCoroutine (TransScene ());
			Sound.PlayBgm ("end");
		}
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

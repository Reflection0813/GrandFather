using UnityEngine;
using System.Collections;

public class PlayerBodyScript : MonoBehaviour {
	public GameObject player;
	private PlayerScript playerScript;
	private GameObject gameManager;
	private GameOverScript gameOverScript;
	private MessageScript messageScript;

	void Start(){
		playerScript = player.GetComponent<PlayerScript> ();
		gameManager = GameObject.Find ("GameManager");
		gameOverScript = gameManager.GetComponent<GameOverScript> ();
		messageScript = gameManager.GetComponent<MessageScript> ();
	}
	
	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag == "Bottom")
			gameOverScript.GameOver ();

		if (col.gameObject.tag == "Flag"){
			gameManager.GetComponent<StageClearScript>().stageClear();

		}

		if (col.gameObject.tag == "CheckPoint") {
			Sound.LoadSe ("checkPoint", "passedCheckPoint");
			Sound.PlaySe ("checkPoint");

			col.gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
			GameManagerScript.passedCheckPoint = true;
		}

		if (col.gameObject.tag == "Message") {
			messageScript.getMessage (col.gameObject.name);
		}

	}
}

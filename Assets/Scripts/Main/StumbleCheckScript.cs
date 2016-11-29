using UnityEngine;
using System.Collections;

public class StumbleCheckScript : MonoBehaviour {
	private GameOverScript gameOverScript;

	// Use this for initialization
	void Start () {
		gameOverScript = GameObject.Find ("GameManager").GetComponent<GameOverScript> ();
	}
	
	void OnTriggerEnter2D(Collider2D col){
		gameOverScript.GameOver ();
	}
}

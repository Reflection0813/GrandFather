using UnityEngine;
using System.Collections;

public class PlayerHeadScript : MonoBehaviour {
	public GameObject player;
	private PlayerScript playerScript;

	void Start(){
		playerScript = player.GetComponent<PlayerScript> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Line" || col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Ground")
			directionChange ();	

	}

	void directionChange(){
		playerScript.direction = -1 * playerScript.direction;
		playerScript.playerSprite.flipX = !playerScript.playerSprite.flipX;
	}
	

}

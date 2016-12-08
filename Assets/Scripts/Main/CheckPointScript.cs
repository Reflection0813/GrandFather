using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D col){
		print ("nunu");
		if (col.gameObject.tag == "Player") {
			//gameObject.GetComponent<SpriteRenderer> ().material.color = Color.green;
		}
	}
}

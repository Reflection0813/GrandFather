using UnityEngine;
using System.Collections;

public class JumpScript : MonoBehaviour {



	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			Sound.PlaySe ("jump");
			col.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 700));
		}
	}
}

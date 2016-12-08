using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {
	public Sprite usedBlock;
	public GameObject something;
	private int direction;

	void OnMouseOver(){
		if (Input.GetMouseButton (0) && gameObject.GetComponent<SpriteRenderer>().sprite != usedBlock) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = usedBlock;
			GameObject instantiatedObject = Instantiate (something, gameObject.transform.position + new Vector3 (0.5f, 0.5f, 0), Quaternion.identity) as GameObject;
			if(GameObject.Find("player").transform.position.x < gameObject.transform.position.x ){ direction = 1;}
			else direction = -1;
			instantiatedObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (direction * 100, 200));
		}
	}
}

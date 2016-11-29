using UnityEngine;
using System.Collections;

public class CameraScript : BaseScript {

	private GameObject player;
	private Vector3 offset = Vector3.zero;
	private float positionOutOfRange = 1.2f;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		offset = transform.position - player.transform.position;
	}

	void Update () {
		offset = transform.position - player.transform.position;
		if (offset.x > positionOutOfRange || offset.x < -positionOutOfRange) {
			if ( transform.position.y > -2 )
				transform.position = Vector3.Lerp (transform.position, new Vector3(player.transform.position.x,player.transform.position.y + 2,-10), 2.0f * Time.deltaTime);
		}
	}
}

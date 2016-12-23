using UnityEngine;
using System.Collections;

public class SimpleLiftScript : MonoBehaviour {
	//上下のときはtrue
	private bool vhFlag;
	private bool direction;

	private Vector3 defaultPosition;
	private Vector3 x_speed;
	private Vector3 y_speed;

	public float movingSize;
	public float speed;


	// Use this for initialization
	void Start () {



		if (gameObject.tag == "udLift") {
			vhFlag = true;
			y_speed = new Vector3 (0, speed, 0);
		} else {
			vhFlag = false;
			x_speed = new Vector3 (speed, 0, 0);
		}

		defaultPosition = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (vhFlag) {
			if (direction)
				transform.position += y_speed;
			else
				transform.position -= y_speed;

			if (transform.position.y >= defaultPosition.y + movingSize || transform.position.y <= defaultPosition.y - movingSize)
				direction = !direction;


		} else {
			if (direction)
				transform.position += x_speed;
			else
				transform.position -= x_speed;

			if (transform.position.x >= defaultPosition.x + movingSize || transform.position.x <= defaultPosition.x - movingSize)
				direction = !direction;
		}


	}
}

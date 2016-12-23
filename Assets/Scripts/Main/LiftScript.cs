using UnityEngine;
using System.Collections;

public class LiftScript : MonoBehaviour {
	//上下のときはtrue
	private bool vhFlag;
	private int direction;

	private Vector3 defaultPosition;
	private Vector3[] targetPosition = new Vector3[2];
	private Vector3 x_speed;
	private Vector3 y_speed;
	public float movingSize;


	// Use this for initialization
	void Start () {
		x_speed = new Vector3(0.05f,0,0);
		y_speed = new Vector3 (0, 0.05f, 0);

		targetPosition[0] = targetPosition[1] = defaultPosition = gameObject.transform.position;


		if (gameObject.tag == "udLift") {
			vhFlag = true;
			targetPosition [0].x = defaultPosition.x - movingSize;
			targetPosition [1].x = defaultPosition.x + movingSize;
		} else {
			vhFlag = false;
			targetPosition [0].y = defaultPosition.y - movingSize;
			targetPosition [1].y = defaultPosition.y + movingSize;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (vhFlag) {
			transform.position = Vector3.Lerp (transform.position,targetPosition[direction],Time.deltaTime * 0.2f);

			if (gameObject.transform.position.y >= defaultPosition.y + movingSize)
				direction = 0;
			if (gameObject.transform.position.y <= defaultPosition.y - movingSize + 10)
				direction = 1;
				
			
		} else {
			transform.position = Vector3.Lerp (transform.position,targetPosition[direction],Time.deltaTime * 0.2f);

			if (gameObject.transform.position.x >= defaultPosition.x + movingSize - 1)
				direction = 0;
			if (gameObject.transform.position.x <= defaultPosition.x - movingSize + 1)
				direction = 1;
		}


	}
}

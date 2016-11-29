using UnityEngine;
using System.Collections;

public class TouchScript : BaseScript {

	public GameObject linePrefab;
	public GameObject parentPrefab;
	public GameObject[] Lines;
	public float lineLength = 0.2f;
	public float lineWidth = 0.1f;

	private GameObject Enemy;

	//LINE関連
	private int unusedLine = 0;
	private int existedLine = 0;
	private GameObject[] parent = new GameObject[2];

	//left, top, right, bottom
	private Vector3[] checkRoundPoint = new Vector3[4];
	const int INF = 100000;


	private Vector3 touchPos;

	//バブルを動かす
	private GameObject touchedEnemy;
	private bool draggingEnemy = false;
	private bool consecutiveDrag = false;

	void Start(){
		clearCheckNum ();
		Enemy = GameObject.Find ("Enemy");
		Sound.LoadSe ("bubbled", "bubbled");

	}

	void clearCheckNum(){
		checkRoundPoint [0] = new Vector3 (1000,0,0); checkRoundPoint [1] = new Vector3(-1000,0,0);
		checkRoundPoint [2] = new Vector3(-1000,0,0); checkRoundPoint [3] = new Vector3(1000,0,0);
	}

	void Update (){
		drawLine ();
	}



	void drawLine(){

		if(Input.GetMouseButtonDown(0))
		{
			touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			touchPos.z=0;


			//バブルに当たっているかどうか
			Collider2D col = Physics2D.OverlapPoint (touchPos);

			if (col) {
				if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.gameObject.tag == "Enemy" && col.gameObject.GetComponent<BatScript> ().getBubbledFlag ()) {
					touchedEnemy = col.gameObject;
					draggingEnemy = true;
					col.gameObject.GetComponent<BatScript> ().setTouchedFlag (true);
				} 
			} 

			consecutiveDrag = true;


			existedLine += 1;
			if (existedLine > 2) {
				Destroy (parent [unusedLine]);
				existedLine -= 1;
			}
			parent[unusedLine] = Instantiate(parentPrefab, transform.position, transform.rotation) as GameObject;

		}

		if(Input.GetMouseButton(0) && !draggingEnemy && consecutiveDrag)
		{
			
			Vector3 startPos = touchPos;
			Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			endPos.z=0;

			//round
			if ( endPos.x < checkRoundPoint[0].x )checkRoundPoint[0] = endPos;
			if ( endPos.y > checkRoundPoint[1].y )checkRoundPoint[1] = endPos;
			if ( endPos.x > checkRoundPoint[2].x )checkRoundPoint[2] = endPos;
			if ( endPos.y < checkRoundPoint[3].y )checkRoundPoint[3] = endPos;


			if((endPos-startPos).magnitude > lineLength){
				GameObject obj = Instantiate(linePrefab, transform.position, transform.rotation) as GameObject;
				obj.transform.position = (startPos+endPos)/2;
				obj.transform.right = (endPos-startPos).normalized;

				obj.transform.localScale = new Vector3( (endPos-startPos).magnitude, lineWidth , lineWidth );

				obj.transform.parent = parent[unusedLine].transform;

				touchPos = endPos;
			}
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			if (draggingEnemy) {
				touchedEnemy.GetComponent<BatScript> ().setTouchedFlag (false);
				draggingEnemy = false;
			}
			if (checkRound ()) {
				print ("ROUND");
				if (checkIncludeEnemy ()) {
					print ("INCLUDE");
					Sound.PlaySe ("bubbled");
					Destroy (parent [unusedLine]);

				}
			}
			clearCheckNum ();
			if ( parent[unusedLine] != null) parent[unusedLine].AddComponent<Rigidbody2D> ();
			switchUnusedLine ();
			consecutiveDrag = false;
		}



	
	}


	private bool checkRound(){
		Vector3 horizontalCenter = (checkRoundPoint [0] + checkRoundPoint [2]) / 2;
		Vector3 verticalCenter = (checkRoundPoint [1] + checkRoundPoint [3]) / 2;
		for (int i = 0; i < 4; i++) {
			//print (checkRoundPoint [i]);
		}
		//print ("horizon = " + horizontalCenter); print ("vertical = " + verticalCenter);
		//print (pointNear (horizontalCenter, verticalCenter, 0.8f)); print (near (Mathf.Abs (checkRoundPoint [0].x - checkRoundPoint [2].x), Mathf.Abs (checkRoundPoint [1].y - checkRoundPoint [3].y), 2));
		return pointNear (horizontalCenter, verticalCenter, 0.8f) && near(Mathf.Abs(checkRoundPoint[0].x - checkRoundPoint[2].x), Mathf.Abs(checkRoundPoint[1].y - checkRoundPoint[3].y),2);
	}

	private bool checkIncludeEnemy(){
		foreach (Transform enemy in Enemy.transform){
			print (enemy);

		//GameObject[] enemy = GameObject.FindGameObjectsWithTag ("Enemy");
			if (enemy != null && checkRoundPoint [0].x < enemy.transform.position.x && enemy.transform.position.x < checkRoundPoint [2].x) {
				if (enemy != null && checkRoundPoint [3].y < enemy.transform.position.y && enemy.transform.position.y < checkRoundPoint [1].y){
					enemy.GetComponent<BatScript> ().bubbled ();
					return true;
				}
			}
		}

		return false;
	}


	private int switchUnusedLine(){

		if (unusedLine == 0)
			unusedLine = 1;
		else{
			unusedLine = 0;
		}
		
		return unusedLine;

	}

	public void setDraggingEnemy(bool bl){
		draggingEnemy = bl;
	}


}

using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {


	public GameObject linePrefab;
	public GameObject parentPrefab;
	public GameObject[] Lines;
	public float lineLength = 0.2f;
	public float lineWidth = 0.1f;

	private GameObject Enemy;
	public GameObject Step;

	//LINE関連
	private int unusedLine = 0;
	private int existedLine = 0;
	private GameObject[] parent = new GameObject[2];

	//left, top, right, bottom
	private Vector3[] checkRoundPoint = new Vector3[4];
	//ジャンプ台の上の角２点と下の角２点での長さを比べる
	private Vector3[] checkStepPoint = new Vector3[2];
	private bool tmpFlag = false;
	private bool stepFlag;

	const int INF = 100000;
	private Vector3 touchPos;
	private Color defaultColor; //= linePrefab.gameObject.GetComponent<SpriteRenderer>().color;
	private Color clr;

	//バブルを動かす
	private GameObject touchedEnemy;
	private bool draggingEnemy = false;
	//バブルを動かしているときは線を出さない
	private bool consecutiveDrag = false;

	private GameOverScript gameOverScript;

	void Start(){
		clearCheckNum ();
		Enemy = GameObject.Find ("Enemy");
		Sound.LoadSe ("bubbled", "bubbled");
		defaultColor = linePrefab.gameObject.GetComponent<SpriteRenderer>().color;
		clr = defaultColor;

		gameOverScript = GameObject.Find ("GameManager").GetComponent<GameOverScript> ();
	}

	void clearCheckNum(){
		linePrefab.gameObject.GetComponent<SpriteRenderer> ().color = defaultColor;
		clr = defaultColor;
		checkStepPoint[0] = new Vector3 (0,0,0); checkStepPoint [1] = new Vector3(0,0,0);tmpFlag = false;
		checkRoundPoint [0] = new Vector3 (1000,0,0); checkRoundPoint [1] = new Vector3(0,-1000,0);
		checkRoundPoint [2] = new Vector3(-1000,0,0); checkRoundPoint [3] = new Vector3(0,1000,0);
	}

	void Update (){
		if (!gameOverScript.getGameOverFlag ()) {
			drawLine ();
		}
	}




	void drawLine(){

		if(Input.GetMouseButtonDown(0))
		{
			touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			touchPos.z=0;

			checkStepPoint [0] = touchPos;
			print ("checkStepPoint[0] = " + checkStepPoint [0]);

			consecutiveDrag = true;

			//バブルに当たっているかどうか
			Collider2D col = Physics2D.OverlapPoint (touchPos);

			if (col) {
				if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.gameObject.tag == "Enemy" && col.gameObject.GetComponent<BatScript> ().getBubbledFlag ()) {
					touchedEnemy = col.gameObject;
					draggingEnemy = true;
					col.gameObject.GetComponent<BatScript> ().setTouchedFlag (true);
					consecutiveDrag = false;
				}

				if (col.gameObject.tag == "Player") {
					col.gameObject.GetComponent<PlayerScript> ().directionChange ();
					consecutiveDrag = false;
				}
			} 




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


			if (startPos.x > endPos.x && !tmpFlag) {
				checkStepPoint [1] = startPos;
				print ("checkStepPoint [1]" + checkStepPoint [1]);
				tmpFlag = true;
			}

			if((endPos-startPos).magnitude > lineLength){
				clr += new Color (0.1f, 0.05f, 0.01f);
				//print (clr);
				linePrefab.GetComponent<SpriteRenderer> ().color = clr;


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
			}// else 
			if(checkIfStep(Camera.main.ScreenToWorldPoint(Input.mousePosition))){
				Destroy (parent [unusedLine]);
				existedLine -= 1;
				//TODO 無理やり２回switchしているだけなので
				switchUnusedLine ();
				Instantiate (Step, touchPos, Quaternion.identity);
				
			}

			clearCheckNum ();
			if ( parent[unusedLine] != null) parent[unusedLine].AddComponent<Rigidbody2D> ();
			switchUnusedLine ();
			consecutiveDrag = false;
		}



	
	}


	private bool checkRound(){
		/*
		for (int i = 0; i < 4; i++) {
			print("checkROundpoint[" + i + " ] = " + checkRoundPoint[i]);
		}*/
		Vector3 horizontalCenter = (checkRoundPoint [0] + checkRoundPoint [2]) / 2;
		Vector3 verticalCenter = (checkRoundPoint [1] + checkRoundPoint [3]) / 2;

		//print ("horizon = " + horizontalCenter); print ("vertical = " + verticalCenter);
		//print (pointNear (horizontalCenter, verticalCenter, 0.8f)); print (near (Mathf.Abs (checkRoundPoint [0].x - checkRoundPoint [2].x), Mathf.Abs (checkRoundPoint [1].y - checkRoundPoint [3].y), 2));
		return Functions.pointNear (horizontalCenter, verticalCenter, 0.8f) && Functions.near(Mathf.Abs(checkRoundPoint[0].x - checkRoundPoint[2].x), Mathf.Abs(checkRoundPoint[1].y - checkRoundPoint[3].y),2);

	}

	private bool checkIncludeEnemy(){
		foreach (Transform enemy in Enemy.transform){
			//print (enemy);

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

	private bool checkIfStep(Vector3 endPos){
		float line_one = checkStepPoint [1].x - checkStepPoint [0].x;
		float line_two = checkRoundPoint [2].x - checkRoundPoint [0].x;


		return ((line_one < line_two) && Functions.pointNear(checkStepPoint[0],endPos,0.5f) && line_one > 1.4f);
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

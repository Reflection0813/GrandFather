using UnityEngine;
using System.Collections;

public class BatScript :EnemyScript {
	public TouchScript touchScript;

	private int HP = 1;
	private Vector3 offset;
	private bool updownFlag = true;

	private bool bubbledFlag = false;
	private bool touchedFlag = false;

	private Animator animator;
	private SpriteRenderer spriteRenderer;
	public Sprite bubbledSprite;
	public GameObject brokenBubble;

	//敵の種別ごと
	public int enemyKind;
	private float homingSpeed = 0.005f;

	private Vector3 playerPosition;
	private GameObject player;

	private bool afterBeMoved = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
		offset = gameObject.transform.position;

		touchScript = GameObject.Find ("GameManager").GetComponent<TouchScript> ();
		animator = gameObject.GetComponent<Animator> ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();

		//タグによって敵の種類を判別
		switch (gameObject.tag) {
		case "Bat1":
			enemyKind = 0;
			break;
		case "Bat2":
			enemyKind = 1;
			break;
		}
		//print (animator);
	}
	
	// Update is called once per frame
	void Update () {
		if (bubbledFlag && touchedFlag) {
			beMoved ();
		
		}else {
			Move ();
		}

		if (HP <= 0)
			Destroy (gameObject);
	}

	void Move(){
		if (enemyKind == 0 || bubbledFlag) {

			if (updownFlag) {
				transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, offset.y + 1, 0), 1.0f * Time.deltaTime);
			} else {
				transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, offset.y - 1, 0), 1.0f * Time.deltaTime);
			}

			if (transform.position.y >= offset.y + 0.75f || transform.position.y <= offset.y - 0.75f)
				updownFlag = !updownFlag;

		} else if (enemyKind == 1) {
			playerPosition = player.transform.position;
			//print (playerPosition);print (gameObject.transform.position);
			if(pointNear(playerPosition,gameObject.transform.position,10) && Time.timeScale != 0){
				transform.position = Vector3.Lerp (transform.position, playerPosition, homingSpeed);
			}
		}

	}

	//タッチで動かされている状態
	void beMoved(){
		Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		touchPoint.z = 0;
		gameObject.transform.position = touchPoint;
	}



	public void bubbled(){
		animator.enabled = false;
		spriteRenderer.sprite = bubbledSprite;
		bubbledFlag = true;
	}

	public bool getBubbledFlag(){
		return bubbledFlag;
	}

	public void setTouchedFlag(bool bl){
		touchedFlag = bl;
	}

	void OnCollisionEnter2D( Collision2D col ) {
		
		//泡の状態で敵に当たると破裂
		if (col.gameObject.transform.parent != null && bubbledFlag && col.gameObject.transform.parent.tag == "Enemy" ) {
			spriteRenderer.sprite = null;
			Instantiate (brokenBubble,gameObject.transform.position,Quaternion.identity);
			Destroy (this.gameObject);
			col.gameObject.GetComponent<BatScript> ().Damage(2);
			touchScript.setDraggingEnemy (false);
			print (col.gameObject.GetComponent<BatScript> ().HP);
		}

		if (col.gameObject.tag == "Boss1" && bubbledFlag ) {
			spriteRenderer.sprite = null;
			Instantiate (brokenBubble,gameObject.transform.position,Quaternion.identity);
			Destroy (this.gameObject);
			col.gameObject.GetComponent<Boss1Script> ().damage ();
			touchScript.setDraggingEnemy (false);
		}
	}

	void Damage(int damageAmount){
		HP = HP - damageAmount;
	}


}

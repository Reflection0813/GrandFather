using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : BaseScript {
	public GameObject Player;
	public SpriteRenderer playerSprite;
	public Sprite normalSprite;
	public Sprite almostStumble;
	private bool stumbleFlag = false;
	private Animator animator;
	private float rotateExtent;
	public float walkSpeed = 0.5f;
	Rigidbody2D rigidbody;

	//特に何もない場合はtrue messageなどがある場合はfalse
	public bool regular;
	//1 is right, -1 is left
	public int direction = 1;

	//GameOver
	private GameOverScript gameOverScript;
	private GameManagerScript gameManagerScript;
	private bool muteki = false;
	//
	private MessageScript messageScript;
	private GameObject gameManager;

	// Use this for initialization
	void Start () {
		playerSprite = gameObject.GetComponent<SpriteRenderer> ();
		animator = gameObject.GetComponent<Animator> ();
		gameManager = GameObject.Find ("GameManager");
		gameOverScript = gameManager.GetComponent<GameOverScript> ();
		gameManagerScript = gameManager.GetComponent<GameManagerScript> ();
		messageScript = gameManager.GetComponent<MessageScript> ();

		regular = true;
		Sound.LoadSe ("down", "down");
		Sound.LoadBgm ("muteki", "muteki");

	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOverScript.getGameOverFlag () && regular) {
			Move ();
		}
	}

	void Move(){
		Player.transform.position += new Vector3(walkSpeed * direction,0,0);
		rotateExtent = transform.localEulerAngles.z;
		if ((rotateExtent > 25 && rotateExtent < 90) || (rotateExtent < 335 && rotateExtent > 270 )) {
			stumbleFlag = true;
			animator.enabled = false;
			playerSprite.sprite = almostStumble;
		} else {
			if (stumbleFlag) {
				playerSprite.sprite = normalSprite;
				animator.enabled = true;
				stumbleFlag = false;
			}
		}
			
	}

	void directionChange(){
		direction = -1 * direction;
		playerSprite.flipX = !playerSprite.flipX;
	}

	private void invincibleOver(){
		walkSpeed /= 2;
		muteki = false;
		animator.SetBool ("Invisible", false);
		Sound.PlayBgm ("default");
	}

	void OnCollisionEnter2D(Collision2D col){
		if (!muteki) {
			if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.gameObject.tag == "Enemy" && !col.gameObject.GetComponent<BatScript> ().getBubbledFlag ()) {
				animator.enabled = false;
				playerSprite.sprite = almostStumble;
				gameOverScript.GameOver ();
			}
		}else {
			if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.gameObject.tag == "Enemy") {
				Destroy (col.gameObject);
			}
		}
			
		if (col.gameObject.tag == "Star") {
			Destroy (col.gameObject);
			Sound.PlayBgm ("muteki");
			walkSpeed *= 2;
			muteki = true;
			animator.SetBool ("Invisible", true);
			Invoke ("invincibleOver", 8);
		}

		if (col.gameObject.tag == "Line") {
			//print ("col = " + col.transform.position.y);print ("pla = " + gameObject.transform.position.y );
			if (col.transform.position.y > gameObject.transform.position.y - 1) {
				directionChange ();
			}
		}



	}

	void OnTriggerEnter2D(Collider2D col){
		//print ("trigger");
		if(col.gameObject.tag == "Line" || col.gameObject.tag == "Obstacle")
			directionChange();

		if (col.gameObject.tag == "Bottom")
			gameOverScript.GameOver ();

		if (col.gameObject.tag == "Flag"){
			gameManager.GetComponent<StageClearScript>().stageClear();

		}

		if (col.gameObject.tag == "Senbei"){
			Destroy (col.gameObject);
			GameManagerScript.senbeiNum += 1;
			gameManagerScript.DisplayReLoad ();

		}


		if (col.gameObject.tag == "CheckPoint") {
			Sound.LoadSe ("checkPoint", "passedCheckPoint");
			Sound.PlaySe ("checkPoint");

			col.gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
			GameManagerScript.passedCheckPoint = true;
		}

		if (col.gameObject.tag == "Message") {
			messageScript.getMessage (col.gameObject.name);
		}

	}



	void OnTriggerStay2D(Collider2D col){
		if ((rotateExtent > 70 && rotateExtent < 100) || (rotateExtent < 300 && rotateExtent > 260)) {
			//Sound.PlaySe ("down");
			gameOverScript.GameOver ();
		}
	}


}



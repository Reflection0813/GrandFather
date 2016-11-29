using UnityEngine;
using System.Collections;

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

	//1 is right, -1 is left
	private int direction = 1;

	//GameOver
	private GameOverScript gameOverScript;

	// Use this for initialization
	void Start () {
		playerSprite = gameObject.GetComponent<SpriteRenderer> ();
		animator = gameObject.GetComponent<Animator> ();
		gameOverScript = GameObject.Find ("GameManager").GetComponent<GameOverScript> ();

		Sound.LoadSe ("down", "down");
	}
	
	// Update is called once per frame
	void Update () {
		Move ();



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

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.gameObject.tag == "Enemy" && !col.gameObject.GetComponent<BatScript> ().getBubbledFlag ())
			gameOverScript.GameOver ();
			//Destroy (gameObject);

	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Line" )
			directionChange ();	
		if (col.gameObject.tag == "Bottom")
			gameOverScript.GameOver ();
	}

	void OnTriggerStay2D(Collider2D col){
		if ((rotateExtent > 70 && rotateExtent < 100) || (rotateExtent < 300 && rotateExtent > 260)) {
			//Sound.PlaySe ("down");
			gameOverScript.GameOver ();
		}
	}


}



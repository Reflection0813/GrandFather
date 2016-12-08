using UnityEngine;
using System.Collections;

public class Boss1Script : MonoBehaviour {
	private int HP;
	private bool invincible = false;

	public GameObject smallBat;
	public GameObject EnemyEmpty;
	private int insBatNum;

	private SpriteRenderer spriteRenderer;
	private Vector3 offset;
	private Vector3 moveVector;
	//falseは左
	private bool direction;

	// Use this for initialization
	void Start () {
		insBatNum = 3; HP = 5;
		Sound.LoadSe ("beatBoss", "beatBoss");

		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		offset = gameObject.transform.position;
		moveVector = new Vector3 (0.1f, 0, 0);
		direction = false;
	}
	
	// Update is called once per frame
	void Update () {
		normalMove ();

		if (gameObject.transform.position.x <= offset.x - 10 || gameObject.transform.position.x >= offset.x + 10) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
			instantiateSmallBats ();
			direction = !direction;
		}
			

		if (HP <= 0) {
			Destroy (gameObject);
			Sound.PlaySe ("beatBoss");
			GameObject.Find ("GameManager").GetComponent<StageClearScript> ().stageClear ();

		}


	}

	private void normalMove(){
		if (!direction) {
			gameObject.transform.position -= moveVector;
		} else {
			gameObject.transform.position += moveVector;
		}

	}

	private void instantiateSmallBats(){
		for (int i = 0; i < insBatNum; i++) {
			GameObject tmp = Instantiate (smallBat, gameObject.transform.position + new Vector3(0,-3,0), Quaternion.identity) as GameObject;
			tmp.transform.parent = EnemyEmpty.gameObject.transform;
		}
	}

	public void damage(){
		if (!invincible) {
			HP -= 1;
			if (HP == 3)
				moveVector += new Vector3 (0.05f, 0, 0);
			if (HP == 1)
				moveVector += new Vector3 (0.1f, 0, 0);
			invincible = true;
			StartCoroutine (blink (0));
		}
	}


	private IEnumerator blink(int blkCnt){
		while (true) {
			spriteRenderer.enabled = !spriteRenderer.enabled;
			if (blkCnt >= 11) {
				invincible = false;
				break;
			}
			blkCnt += 1;
			yield return new WaitForSeconds (0.2f);
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StageSelectScript : MonoBehaviour {
	//public static bool onMouseOver = false;
	private bool onMouseOver;

	private int stageNum = 1;
	private float moveSpeed = 0.2f;
	public Text stageInfo;

	public GameObject player;
	private Vector3 offset;
	private Vector3 targetPosition;
	private int playerPosition;
	private bool moving = false;
	//falseは左
	private bool direction;
	private bool entering;

	public Text playerLeft;
	public Text senbeiLeft;

	//ステージ見た目系
	public GameObject[] mass = new GameObject[5];

	// Use this for initialization
	void Start () {
		Sound.LoadBgm ("GeneralBGM", "stage1to5");
		Sound.PlayBgm ("GeneralBGM");
		playerPosition = 0;
		entering = false; onMouseOver = false;
		targetPosition = player.gameObject.transform.position;
		MapLoad ();
	}

	// Update is called once per frame
	void Update () {
	//	print (onMouseOver);

		if (!onMouseOver && Input.GetMouseButtonDown (0) && !moving && !entering) {
			print (playerPosition);
			Vector3 touchPos = Input.mousePosition;
			touchPos.z=0;
			moving = true;

			offset = player.transform.position;
			if (touchPos.x < Screen.width/2) {
				if (playerPosition > 0) {
					stageNum -= 1;
					playerPosition -= 1;
					targetPosition = offset - new Vector3 (10, 0, 0);
					direction = false;
					player.GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else {
				if (playerPosition < 4) {
					stageNum += 1;
					playerPosition += 1;
					targetPosition = offset + new Vector3 (10, 0, 0);
					direction = true;
					player.GetComponent<SpriteRenderer> ().flipX = true;

				}
			}
			StartCoroutine (movePlayer ());
			changeStageInfo ();
		}
			
	}
	/*
	private bool RayHit(){
		Ray ray = new Ray (transform.position, transform.forward);
		Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 10.0f);

		if (hit.collider) {
			print ("aaaa");
			if (hit.collider.gameObject.tag == "Button" || hit.collider.gameObject.tag == "Panel") {
				return true;
			}
		}
		

		return false;
	}*/

	public void enterTheStage(){
		if (!moving) {
			PlayerPrefs.SetInt ("checkPoint", 0);
			entering = true;
			GameManagerScript.playingStage = stageNum;
			SceneManager.LoadScene ("stage" + stageNum.ToString ());
		}
	}

	public void trueMouseOver(){
		onMouseOver = true;
	}

	public void falseMouseOver(){
		onMouseOver = false;
	}

	IEnumerator movePlayer(){
		while (true) {
			if (direction) {//右向きであれば
				if (player.transform.position.x >= targetPosition.x) {
					moving = false;
					break;
				} else {
					player.transform.position += new Vector3 (moveSpeed, 0, 0);
				}

			} else {
				if (player.transform.position.x <= targetPosition.x) {
					moving = false;
					break;
				} else {
					player.transform.position -= new Vector3 (moveSpeed, 0, 0);
				}
			}
			yield return null;
		}
	}



	void MapLoad(){
		int clearedStage = PlayerPrefs.GetInt ("clearedStage");
		for (int i = 0; i < clearedStage - 1; i++) {
			print ("i = " + i);
			mass [i].GetComponent<MassScript> ().changeColor ();
		}

		playerLeft.text = "×" + PlayerPrefs.GetInt ("playerNum").ToString();
		senbeiLeft.text = "×" + PlayerPrefs.GetInt ("senbeiNum").ToString();
			
	}

	void changeStageInfo(){
		stageInfo.text = "stage" + stageNum.ToString ();
	}
		
}

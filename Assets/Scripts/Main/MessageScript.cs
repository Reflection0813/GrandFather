using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageScript : MonoBehaviour {
	public GameObject player;
	public GameObject messageBoard;
	public Text messageText;

	private int tmp = 0;
	public GameObject[] conductor = new GameObject[10];

	private FadeInSpriteScript fadeInSpriteScript;
	private bool isMessaged = false;

	private string[] messages = new string[10];

	// Use this for initialization
	void Start () {
		fadeInSpriteScript = messageBoard.GetComponent<FadeInSpriteScript> ();

		messages [0] = "おじいさん";
	}

	void Update(){
		if (isMessaged && Input.GetMouseButtonUp (0))
			foldMessage ();
	}


	public void getMessage(string messageTriggerNum){
		messageBoard.gameObject.SetActive(true);
		//fadeInSpriteScript.Play ();
		print(int.Parse(messageTriggerNum));
		messageText.text = messages [int.Parse(messageTriggerNum)];

		Instantiate(conductor [int.Parse (messageTriggerNum)]);
		tmp = int.Parse (messageTriggerNum);

		player.GetComponent<PlayerScript> ().regular = false;
		isMessaged = true;
	}

	public void foldMessage(){
		//fadeout
		Time.timeScale = 1;
		player.GetComponent<PlayerScript> ().regular = true;
		messageBoard.gameObject.SetActive(false);
		//Destroy (conductor [tmp]);
		messageText.text = "";
		isMessaged = false;
	}
}

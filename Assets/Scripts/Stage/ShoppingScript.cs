using UnityEngine;
using System.Collections;

public class ShoppingScript : MonoBehaviour {
	public GameObject shoppingPanel;

	private AdScript adScript;

	void Start(){
		adScript = GameObject.Find ("GameManager").GetComponent<AdScript> ();

	}

	//TODO ポジションが恣意的すぎる

	public void shoppingButton(){
		shoppingPanel.gameObject.SetActive (true);
	}

	public void foldShoppingList(){
		shoppingPanel.gameObject.SetActive (false);
	}

	public void purchasePlayer(){
		if (PlayerPrefs.GetInt ("senbeiNum") >= 100) {
			GameManagerScript.playerNum += 1;
			GameManagerScript.senbeiNum -= 100;
		} else {
			print ("かえないよ");
		}
	}

	public void purchaseLine(){
	
	}
}

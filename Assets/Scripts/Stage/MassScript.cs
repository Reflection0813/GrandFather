using UnityEngine;
using System.Collections;

public class MassScript : MonoBehaviour {
	public Material clearedMaterial;

	public void changeColor(){
		gameObject.GetComponent<Renderer> ().material = clearedMaterial;
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StageSelectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		touchPoint.z = 0;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);




		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction);

			if (hit.collider) {
				print (hit.collider.gameObject);
				string loadingStageName = hit.collider.gameObject.name;
				SceneManager.LoadScene (loadingStageName);
			}
		}
	
	}
}

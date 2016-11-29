using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour {



	public bool pointNear(Vector3 vec_a, Vector3 vec_b, float diff){
		return (Mathf.Abs (vec_a.x - vec_b.x) < diff && (Mathf.Abs (vec_a.y - vec_b.y) < diff));
	}

	public bool near(float a, float b, float diff){
		return (Mathf.Abs (a - b) < diff);
	}

}

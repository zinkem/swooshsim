using UnityEngine;
using System.Collections;

public class AngularVelocity : MonoBehaviour {

  public float angularVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

    transform.Rotate(0, 0, angularVelocity);
	
	}
}

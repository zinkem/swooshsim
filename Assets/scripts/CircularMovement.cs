using UnityEngine;
using System.Collections;

public class CircularMovement : MonoBehaviour {

  float counter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
    
    counter += .0314f;

    float dx = Mathf.Cos(counter);
    float dy = Mathf.Sin(counter);

    GetComponent<Rigidbody2D>().velocity = new Vector3 ( dx, dy, 0f );
    
	
	}
}

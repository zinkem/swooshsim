using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

  public Transform carrot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
    float destx = carrot.position.x;
    float desty = carrot.position.y;

    float currx = transform.position.x;
    float curry = transform.position.y;

    float diffx = destx - currx;
    float diffy = desty - curry;

    destx -= .9f*diffx;
    desty -= .9f*diffy;

    transform.position = new Vector3 (destx, desty, transform.position.z);          	
	}
}

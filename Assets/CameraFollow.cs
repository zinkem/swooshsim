using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

  public Transform carrot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    float destx = carrot.position.x;
    float desty = carrot.position.y;

    transform.position = new Vector3 (destx, desty, transform.position.z);          	
	}
}

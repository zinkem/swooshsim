using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {


  public float rate;

  float total;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
    total += rate;

    Vector3 theScale = transform.localScale;

    theScale.x += Mathf.Cos(total);
    theScale.y += Mathf.Sin(total);

    transform.localScale = theScale;

	}
}

using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {


  public float rate;
  public float amplitude;

  float total;
  Vector3 refscale;

	// Use this for initialization
	void Start () {
    refscale = transform.localScale;
    total = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
    total += rate;
    Vector3 theScale = refscale;

    theScale.x += amplitude*Mathf.Sin(total);
    theScale.y += amplitude*Mathf.Sin(total);

    transform.localScale = theScale;

	}
}

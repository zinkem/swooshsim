using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

  public Transform carrot;
  public float velocity;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

    if( carrot == null )
      return;

    float currx = transform.position.x;
    float curry = transform.position.y;

    float destx = carrot.position.x;
    float desty = carrot.position.y;

    float velx = destx - currx;
    float vely = desty - curry;

    float magsq = Mathf.Sqrt(velx*velx + vely*vely);

    velx = (velx/magsq)*velocity;
    vely = (vely/magsq)*velocity;

    if( magsq >5 )
      {
        rigidbody2D.velocity = new Vector2 ( velx, vely );        
      }


    float a;
    float b;

    if( velx > vely ) {
        a = curry;
        b = desty;
    } else {
        a = currx;
        b = destx;
    }

    if( a < b ) {
      transform.Rotate(0, 0, 1);
    } else {
      transform.Rotate(0, 0, -1);
    }

	}
}

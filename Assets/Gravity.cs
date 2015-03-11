using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {

  public Transform player;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
    float wellx = transform.position.x;
    float welly = transform.position.y;

    float distx = wellx - player.transform.position.x; 
    float disty = welly - player.transform.position.y; 
    
    float mag = distx*distx + disty*disty;
    float magsqrt = Mathf.Sqrt(mag);

    mag = mag * magsqrt;

    float fx = distx/mag;
    float fy = disty/mag;

    if( mag >= 0.01 )
      {
        player.rigidbody2D.velocity = player.rigidbody2D.velocity + 
          new Vector2( fx, fy );        
      }


	}
}

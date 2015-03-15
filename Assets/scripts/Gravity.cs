using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {

  public Transform player;

  public float intensity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
    
    if( player == null )
      return;


    float wellx = transform.position.x;
    float welly = transform.position.y;

    float distx = wellx - player.transform.position.x; 
    float disty = welly - player.transform.position.y; 
    
    float mag = distx*distx + disty*disty;
    float magsqrt = Mathf.Sqrt(mag);

    mag = mag * magsqrt;

    float fx = distx/mag;
    float fy = disty/mag;


    player.rigidbody2D.velocity = player.rigidbody2D.velocity + 
      new Vector2( fx*intensity, fy*intensity );        

    float pspeed =  Mathf.Sqrt( player.rigidbody2D.velocity.x
                                *player.rigidbody2D.velocity.x +
                                player.rigidbody2D.velocity.y
                                * player.rigidbody2D.velocity.y );

    if( pspeed > 50 )
      {
        player.rigidbody2D.velocity = player.rigidbody2D.velocity * 50/pspeed;
      }
                    
                    

	}
}

using UnityEngine;
using System.Collections;

public class BoundaryBoxScript : MonoBehaviour {

  public float bound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    float x = transform.position.x;
    float y = transform.position.y;
    
    float dx = rigidbody2D.velocity.x;
    float dy = rigidbody2D.velocity.y;

    if( x > bound ) 
      {
        rigidbody2D.velocity = new Vector2(-dx, dy);
        x = bound;
      } 
    if ( x <= -bound )
      {
        rigidbody2D.velocity = new Vector2(-dx, dy);
        x = -bound;

      }
    if( y > bound ) 
      {
        rigidbody2D.velocity = new Vector2(dx, -dy);
        y = bound;
      } 
    if ( y <= -bound )
      {
        rigidbody2D.velocity = new Vector2(dx, -dy);
        y = -bound;
      }

    transform.position = new Vector3(x, y, transform.position.z);

	}
}

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

    if( x > bound ) 
      {
        x -= bound*2;
      } 
    if ( x <= -bound )
      {
        x += bound*2;
      }
    if( y > bound ) 
      {
        y -= bound*2;
      } 
    if ( y <= -bound )
      {
        y += bound*2;
      }

    transform.position = new Vector3(x, y, transform.position.z);

	}
}

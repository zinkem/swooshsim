using UnityEngine;
using System.Collections;

public class ParallaxScroller : MonoBehaviour {

  public GameObject reference_object;

  float offset_ratio;
  float origin_x;
  float origin_y;
  float origin_z;

	// Use this for initialization
	void Start () {
    origin_x = transform.position.x;
    origin_y = transform.position.y;
    origin_z = transform.position.z;

    offset_ratio = 1/(origin_z * Mathf.Abs(origin_z));
	}
	
	// Update is called once per frame
	void Update () {

    float dx = origin_x+reference_object.rigidbody2D.position.x*offset_ratio;
    float dy = origin_y+reference_object.rigidbody2D.position.y*offset_ratio;

    /*
    transform.localScale = new Vector2 (offset_ratio * origin_z,
                                        offset_ratio * origin_z);
                                        */
    transform.position = new Vector3( dx, dy, origin_z );
	
	}
}

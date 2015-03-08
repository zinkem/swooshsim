using UnityEngine;
using System.Collections;
using System;


public class NewBehaviourScript : MonoBehaviour {

  public Sprite tilt_0;
  public Sprite tilt_1;
  public Sprite tilt_2;
  public Sprite tilt_3;

  public Transform exhaust;

  public float friction;

  private Sprite[] sprite_array;


  private SpriteRenderer sr;

  private float orientation;
  private float last_rads;

	// Use this for initialization
	void Start () {
    sr = GetComponent<SpriteRenderer>();
    orientation = 0f;
	  sr.sprite = tilt_0;
    last_rads = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	  float move = Input.GetAxis("Horizontal");
    float thrust = Input.GetAxis("Vertical");

    float angle;
    int angle_index;

    last_rads = (float)(orientation * Math.PI / 180);

    orientation -= (move*3);

    while ( orientation < 0 )
      orientation+= 360;
    while ( orientation > 360 )
      orientation-= 360;

    sr.sprite = tilt_3;

    float rads = (float)(orientation * Math.PI / 180);
    transform.Rotate(0, 0, -move*3);

    int index = (((int)orientation + 15) / 30)%12;

    if( index % 6 == 0 ) {
      sr.sprite = tilt_0;
    } else if( index % 3 == 0) {
      sr.sprite = tilt_3;
    } else if( index %2 == 0) {
      sr.sprite = tilt_2;
    } else {
      sr.sprite = tilt_1;      
    }

    if( index > 3 && index < 9 ) {
      if( transform.localScale.y > 0 ) {
         flipy();
      }
    } else {
      if( transform.localScale.y < 0 ) {
         flipy();
      }   
    }

    float slow = 1-(friction*friction);
    float dx = slow*rigidbody2D.velocity.x;
    float dy = slow*rigidbody2D.velocity.y;// - 1.5f;

    if( thrust > 0 ){
      dx += (float)Math.Cos(rads)/2;
      dy += (float)Math.Sin(rads)/2;// - 1.5f;
      Transform t = (Transform)Instantiate( exhaust, transform.position, transform.rotation );
      t.rigidbody2D.velocity = new Vector2 ((float)-Math.Cos(rads)/2,
                                            (float)-Math.Sin(rads)/2 );

      Vector3 theScale = t.transform.localScale;
      theScale *= thrust;
      t.transform.localScale = theScale;

    }

    rigidbody2D.velocity = new Vector2 (dx , dy );



    /* bounding box logic
    Vector3 pos = rigidbody2D.position;

    if( pos.x > 10 ) {
      pos.x -= 20;
    }else if( pos.x < -10 ) {
      pos.x += 20;
    }

    if( pos.y > 10 ) {
      pos.y -= 20;
    }else if( pos.y < -10 ) {
      pos.y += 20;
    }

    rigidbody2D.position = pos;
    */



    /*
    float friction = .9f;

    float dx = friction*rigidbody2D.velocity.x + move;
    float dy = (friction*rigidbody2D.velocity.y + thrust*3f) - 1.5f;


    rigidbody2D.velocity = new Vector2 (dx , dy );
    */
	}

  void flipx(){
    Vector3 theScale = transform.localScale;
    theScale.x *= -1;
    transform.localScale = theScale;
  }

  void flipy(){
    Vector3 theScale = transform.localScale;
    theScale.y *= -1;
    transform.localScale = theScale;
  }

}

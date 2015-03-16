using UnityEngine;
using System.Collections;
using System;


public class PlayerScript : MonoBehaviour {

  public Sprite tilt_0;
  public Sprite tilt_1;
  public Sprite tilt_2;
  public Sprite tilt_3;

  public Transform exhaust;
  public Transform projectile;
  public Transform star;
  public Transform path;
  public Transform ammo;
  
  public float friction;

  private Sprite[] sprite_array;

  private SpriteRenderer sr;

  private float orientation;

  public int energy;
  public int energyMax;
  public int powcount;

  public float firedelay;
  private int fire_count;



	// Use this for initialization
	void Start () {
    sr = GetComponent<SpriteRenderer>();
    orientation = 0f;
	  sr.sprite = tilt_0;
    
    energyMax = 1000;
    energy = energyMax;
    powcount = 0;

    for( int i = 0; i < 100; i++)
      {
        float a = UnityEngine.Random.value*200-100;
        float b = UnityEngine.Random.value*200-100;
        float c = Mathf.Floor(UnityEngine.Random.value*3)*-10;

        Transform o = (Transform)Instantiate( star, new Vector3(a, b, c), transform.rotation);

        Gravity g = o.GetComponent<Gravity>();
        g.player = this.transform;

        o.rigidbody2D.velocity = new Vector2( (float)UnityEngine.Random.value-.5f,
                                              (float)UnityEngine.Random.value-.5f );
      }

    for( int i = 0; i < 100; i++)
      {
        float a = UnityEngine.Random.value*400-200;
        float b = UnityEngine.Random.value*400-200;
        float c = -1;

        Transform o = (Transform)Instantiate( ammo, new Vector3(a, b, c), transform.rotation);

      }

	}
  
  void Update()
  {

  }

	
	// Update is called once per frame
	void FixedUpdate () {

	  float move = Input.GetAxis("Horizontal");
    float thrust = Input.GetAxis("Vertical");
    bool fire = Input.GetButton("Fire1");

    float rotspeed = 3;
    if( !fire ) rotspeed += 7*(1-thrust);

    orientation -= (move*rotspeed);

    while ( orientation < 0 )
      orientation+= 360;
    while ( orientation > 360 )
      orientation-= 360;

    sr.sprite = tilt_3;

    float rads = (float)(orientation * Math.PI / 180);
    transform.Rotate(0, 0, -move*rotspeed);

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

    Instantiate( path, transform.position, transform.rotation );

    if( thrust > 0 && energy > 100){
      energy -= 10;
      dx += thrust*(float)Math.Cos(rads)/2;
      dy += thrust*(float)Math.Sin(rads)/2;// - 1.5f;
      Transform t = (Transform)Instantiate( exhaust, transform.position, transform.rotation );
      t.rigidbody2D.velocity = new Vector2 ((float)-Math.Cos(rads)/2,
                                            (float)-Math.Sin(rads)/2 );

      Vector3 theScale = t.transform.localScale;
      theScale *= thrust;
      t.transform.localScale = theScale;

    } else {
      if( energy < energyMax)
        energy += 5;
    }

    rigidbody2D.velocity = new Vector2 (dx , dy );



    
    fire_count++;
    if( fire && fire_count >= firedelay*60  )
      {

        fire_count = 0;
        for( int i = -1; i <= 1; i++ )
          {
            float mod = (i*Mathf.PI/36);
            float a = (float)Math.Cos(rads+mod)*16;
            float b = (float)Math.Sin(rads+mod)*16;

            Vector2 pvel = new Vector2(a,b);

            Vector3 ppos = new Vector3(Mathf.Cos(rads+(4*mod))/4,
                                       Mathf.Sin(rads+(4*mod))/4,
                                       0) + transform.position;

            Transform t= (Transform)Instantiate( projectile, 
                                                 ppos,
                                                 transform.rotation );
            t.rigidbody2D.velocity = pvel + rigidbody2D.velocity;
            
          }

        
      }

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

  void OnDestroy()
  {

  }

}

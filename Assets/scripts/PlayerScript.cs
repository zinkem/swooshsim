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
  public Transform gravitron;

  public float friction;

  private Sprite[] sprite_array;

  private SpriteRenderer sr;

  private float orientation;

  public int energy;
  public int energyMax;
  public int powcount;

  public float firedelay;
  private int fire_count;

  Transform[] pickup;

  Transform[] gravfield;
  private int set;
  
	// Use this for initialization
	void Start () {
    sr = GetComponent<SpriteRenderer>();
    orientation = 0f;
	  sr.sprite = tilt_0;
    
    energyMax = 1000;
    energy = energyMax;
    powcount = 0;
    set = 0;

    gravfield = new Transform[600];
    
    pickup = new Transform[10];
    for( int i = 0; i < 10; i++)
      {
        float a = UnityEngine.Random.value*40-20+transform.position.x;
        float b = UnityEngine.Random.value*40-20+transform.position.y;
        float c = -1;

        Transform o = (Transform)Instantiate( ammo, new Vector3(a, b, c), transform.rotation);
        AmmoScript am = o.GetComponent<AmmoScript>();
        am.player = this.transform;

        Gravity gv = o.GetComponent<Gravity>();
        gv.player = this.transform;

        pickup[i] = o;

      }

	}
  
  void Update()
  {

  }

  public void incMaxEnergy(int x)
  {
    energyMax += x;
  }

  public void incLife(int x)
  {
    gameObject.GetComponent<KillMutateOnCollide>().life++;
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
    float dx = slow*GetComponent<Rigidbody2D>().velocity.x;
    float dy = slow*GetComponent<Rigidbody2D>().velocity.y;// - 1.5f;

    Instantiate( path, transform.position, transform.rotation );

    if( thrust > 0 ){
      dx += thrust*(float)Math.Cos(rads)/4;
      dy += thrust*(float)Math.Sin(rads)/4;// - 1.5f;
      Transform t = (Transform)Instantiate( exhaust, transform.position, transform.rotation );
      t.GetComponent<Rigidbody2D>().velocity = new Vector2 ((float)-Math.Cos(rads)/2,
                                            (float)-Math.Sin(rads)/2 );

      Vector3 theScale = t.transform.localScale;
      theScale *= thrust;
      t.transform.localScale = theScale;

    } 

    GetComponent<Rigidbody2D>().velocity = new Vector2 (dx , dy );



    
    fire_count++;
    if( fire_count >= firedelay*60) {
     
      if( fire && energy >= 10 ) {
        energy -= 10;
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
            t.GetComponent<Rigidbody2D>().velocity = pvel + GetComponent<Rigidbody2D>().velocity;
            
          }
      } else {
        if( energy < energyMax)
          energy += 5;
      }
        
    }

    set = (set + 1)%gravfield.Length;
    for( int i = 0; i < gravfield.Length; i++){
        GameObject grav;

        gravfield[set] = null;

        
        if( gravfield[i] ){
            grav = gravfield[i].gameObject;
        } else {
            Transform o = (Transform)Instantiate( gravitron, transform.position, transform.rotation);

            o.position = transform.position
                + new Vector3 ( Mathf.Sin(1+i*1.61f), Mathf.Cos(1+i*1.61f), 0);
            
            o.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + new Vector2 ( Mathf.Sin(i*.628f), Mathf.Cos(i*.628f));
            gravfield[i] = o;
            grav = o.gameObject;
        }

        for( int j = 0; j < 10; j++){
            GameObject a = pickup[j].gameObject; 

            
            float wellx = a.transform.position.x;
            float welly = a.transform.position.y;

            float distx = wellx - grav.transform.position.x; 
            float disty = welly - grav.transform.position.y; 
    
            float mag = distx*distx + disty*disty;
            float magsqrt = Mathf.Sqrt(mag);

            mag = mag * magsqrt;

            float fx = distx/mag;
            float fy = disty/mag;
            float intensity = a.GetComponent<Gravity>().intensity;

            grav.GetComponent<Rigidbody2D>().velocity = grav.GetComponent<Rigidbody2D>().velocity + 
                new Vector2( fx*intensity, fy*intensity );        
        }
        /*
        float pspeed =  Mathf.Sqrt( grav.GetComponent<Rigidbody2D>().velocity.x
                                    *grav.GetComponent<Rigidbody2D>().velocity.x +
                                    grav.GetComponent<Rigidbody2D>().velocity.y
                                    * grav.GetComponent<Rigidbody2D>().velocity.y );

        if( pspeed > 50 )
            {
                grav.GetComponent<Rigidbody2D>().velocity = grav.GetComponent<Rigidbody2D>().velocity * 50/pspeed;
            }
        */

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

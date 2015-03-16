using UnityEngine;
using System.Collections;

public class AmmoScript : MonoBehaviour {

  public Transform pickupdust;
  public Transform player;

  void newPosition()
  {
    Instantiate(pickupdust, transform.position, transform.rotation);

    float a = UnityEngine.Random.value*40-20+player.position.x;
    float b = UnityEngine.Random.value*40-20+player.position.y;
    float c = transform.position.z;

    transform.position = new Vector3( a, b, c );

    transform.localScale = new Vector3( 1, 1, 1);
  }

	// Use this for initialization
	void Start () {
    //newPosition();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
    rigidbody2D.velocity = new Vector3 (0,0,0);

    if( player ) {
      float currx = transform.position.x;
      float curry = transform.position.y;

      float destx = player.position.x;
      float desty = player.position.y;

      float velx = destx - currx;
      float vely = desty - curry;

      float magsq = Mathf.Sqrt(velx*velx + vely*vely);

      if( magsq > 30 ) {
        transform.position = player.position + 
          new Vector3( velx, vely, 0f )*.9f;
        return;
      }

    }

	}

  void OnCollisionEnter2D(Collision2D other)
  {
    transform.localScale = transform.localScale * 2.1f;
    if(transform.localScale.x > 2)
      {
        newPosition();

        PlayerScript ps = other.gameObject.GetComponent<PlayerScript>();

        if(ps){
          ps.powcount += 1;
          if( UnityEngine.Random.value < .5 ) {
            ps.incMaxEnergy(10);
          } else {
            ps.incLife(1);
          }
        }

      }
  }

}

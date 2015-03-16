using UnityEngine;
using System.Collections;

public class AmmoScript : MonoBehaviour {


  void newPosition()
  {
    float a = UnityEngine.Random.value*200-100;
    float b = UnityEngine.Random.value*200-100;
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
	}

  void OnCollisionStay2D(Collision2D other)
  {
    transform.localScale = transform.localScale * 2.1f;
    if(transform.localScale.x > 2)
      {
        newPosition();

        PlayerScript ps = other.gameObject.GetComponent<PlayerScript>();

        if(ps)
          {
            ps.energyMax += 100;

          }

      }
  }

  void OnCollisionExit2D(Collision2D other)
  {
    newPosition();
  }

}

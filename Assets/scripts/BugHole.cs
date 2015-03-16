using UnityEngine;
using System.Collections;

public class BugHole : MonoBehaviour {

  
  public float rate;
  public float start;
  public float wavepop;
  public Transform bug;
  public Transform player;
  public Transform star;

  public HUDscript hud;

  float accumulator;
  float census;


	// Use this for initialization
	void Start () {
    accumulator = 0;
    census = 0;
	}
	
  void bugWave()
  {
    float score = (float)hud.score;

    while( census <= wavepop * Mathf.Ceil((score+1)/5000) ) {

      float rads = census*Mathf.PI/180;
      rads *= 15;
      Vector3 pos = new Vector3 ( Mathf.Cos(rads), Mathf.Sin(rads), 0)
        *(Mathf.Floor(census/16)+1);
      pos += transform.position;

      Transform t = (Transform)Instantiate( bug, pos, transform.rotation );

      FollowObject fo = t.GetComponent<FollowObject>();
      fo.carrot = player;

      KillMutateOnCollide km = t.GetComponent<KillMutateOnCollide>();
      km.hud = hud;            

      census += 1;

    }
    
  }


  void starWave()
  {
    
    for( int i = 0; i < wavepop/4; i++)
      {
        float a = Mathf.Cos(UnityEngine.Random.value*Mathf.PI*2)*10
          +player.transform.position.x;
        float b = Mathf.Sin(UnityEngine.Random.value*Mathf.PI*2)*10
          +player.transform.position.y;

        Transform o = (Transform)Instantiate( star, new Vector3(a, b, -1f), transform.rotation);

        FollowObject fo = o.GetComponent<FollowObject>();
        fo.carrot = player;

        Gravity g = o.GetComponent<Gravity>();
        g.player = player;

        o.GetComponent<Rigidbody2D>().velocity = new Vector2( (float)UnityEngine.Random.value-.5f,
                                              (float)UnityEngine.Random.value-.5f );
      }

  }

	// Update is called once per frame
	void FixedUpdate () {
	
    if( player )
      accumulator += 1;

    if( accumulator >= 60*rate )
      {

        if( UnityEngine.Random.value > .1 ) {
          bugWave();            
        } else  {
          starWave();                
        }

        accumulator = 0;              
        census = 0;

      }
	}
}

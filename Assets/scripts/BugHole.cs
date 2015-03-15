using UnityEngine;
using System.Collections;

public class BugHole : MonoBehaviour {

  
  public float rate;
  public float start;
  public float wavepop;
  public Transform bug;
  public Transform player;

  public HUDscript hud;

  float accumulator;
  float census;


	// Use this for initialization
	void Start () {
    accumulator = 0;
    census = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
    float score = (float)hud.score;
    
    accumulator += 1;

    if( accumulator >= 60*rate )
      {
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


        accumulator = 0;              
        census = 0;

      }
	}
}

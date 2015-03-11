using UnityEngine;
using System.Collections;

public class BugHole : MonoBehaviour {

  
  public float rate;
  public Transform bug;
  public Transform player;

  float accumulator;

	// Use this for initialization
	void Start () {
    accumulator = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
    accumulator += rate;

    if( accumulator >= 1 )
      {
        accumulator = 0;
        Transform t = (Transform)Instantiate( bug, transform.position, transform.rotation );

        FollowObject fo = t.GetComponent<FollowObject>();

        fo.carrot = player;
      }
    
	}
}

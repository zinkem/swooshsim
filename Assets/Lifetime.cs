using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour {
  
  public float lifetime;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
    lifetime--;

    if( lifetime <= 0 )
      {
        
        Destroy(gameObject);
      }

	}
}

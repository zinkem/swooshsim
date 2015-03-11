using UnityEngine;
using System.Collections;

public class KillMutateOnCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      
	}

  void OnCollisionEnter2D(Collision2D other)
  {
      print ( "wooot !");
      Destroy(gameObject);
      Destroy(other.gameObject);

      if (other.gameObject.tag == "mutable"){


      } else {
          //Destroy(gameObject);
      }
  }

}

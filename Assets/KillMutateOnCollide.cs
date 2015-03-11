using UnityEngine;
using System.Collections;

public class KillMutateOnCollide : MonoBehaviour {

  public int life;
  public Transform replace;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      
	}

  void OnCollisionEnter2D(Collision2D other)
  {
      life--;
      print("boomy " + life);
      if( life <= 0 ){
          Destroy(gameObject);
          Instantiate(replace, transform.position, transform.rotation);
      }
  }

}

using UnityEngine;
using System.Collections;

public class KillMutateOnCollide : MonoBehaviour {

  public int life;
  public Transform replace;
  public HUDscript hud;
  public int pointval;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
    if( life <= 0 ){
      Destroy(gameObject);
      Instantiate(replace, transform.position, transform.rotation);
    }
      
	}

  void decrementLife(){
    life--;
    print(life+" life");
  }

  void OnCollisionEnter2D(Collision2D other)
  {
      decrementLife();
  }


  void OnDestroy(){
      if( hud ){
          hud.registerKill(pointval);
      }

  }

}

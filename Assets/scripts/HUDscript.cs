using UnityEngine;
using System.Collections;

public class HUDscript : MonoBehaviour {

  public GUIText board;

  public GUIText life;
  public Transform player;

  int score;

  void populateLifeText()
  {
    if( player ) {
      int l = player.GetComponent<KillMutateOnCollide>().life;
      life.text = l + " life ";        
    } else {
      life.text ="press space to play again";            
    }
  }

  public void registerKill(int val)
  {
    score += val;
  }

	// Use this for initialization
	void Start () {
    populateLifeText();
    score = 0;
    board.text = " "+score+" ";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
    populateLifeText();
    board.text = " "+score+" ";
	}
}

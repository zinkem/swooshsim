using UnityEngine;
using System.Collections;

public class HUDscript : MonoBehaviour {

  public GUIText board;

  public GUIText life;
  public Transform player;

  public Transform energy_bar;

  public int score;
  
  int interest_counter;
  
  void populateLifeText()
  {
    if( player ) {
      int l = player.GetComponent<KillMutateOnCollide>().life;
      life.text = l + " life ";        
    } else {
      life.text ="press space to play again";            
    }
  }

  void scaleEnergyBar()
  {
    if( player)
      {
        int e = player.GetComponent<PlayerScript>().energy;
        energy_bar.transform.localScale = new Vector2(4*(e/100), 4);
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
    scaleEnergyBar();
    board.text = " "+score+" ";

    interest_counter++;

    if( interest_counter >= 60 && player != null )
      {
        score *= (player.GetComponent<PlayerScript>().powcount+100)/100;
        interest_counter = 0;
      }
	}
}

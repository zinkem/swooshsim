using UnityEngine;
using System.Collections;

public class RestartLevelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
			bool restart = Input.GetButtonDown("Jump");

			if( restart ){
					Application.LoadLevel(0);
			}
	}
}

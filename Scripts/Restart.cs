using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RestartGame() {
		Debug.Log ("Restart");
		Application.LoadLevel(0);
	}

	public void Exit() {
		Application.Quit();
	}
}

using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider col)
	{
		if (col.name == "Player") {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}

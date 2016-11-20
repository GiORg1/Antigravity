using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float timer = 5.0f;
	public GameObject toDestroy;

	void FixedUpdate () {
		timer -= Time.deltaTime;

		if (timer <= 0.0f) {
			Destroy (toDestroy);
		}
	}
}

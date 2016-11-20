using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using UnityEngine.VR;

public class AntiGravity : MonoBehaviour 
{
	public Material CurrMaterial;
	public Material OtherMaterial;
	///
	public string CurrentStage = "-Y";

	public bool useMouse = true;
	/// <summary>
	/// 
	/// </summary>
	public float speed = 5.0f;
	public GameObject cam;

	public float speedH = 2.0f;
	public float speedV = 2.0f;

	private float yaw = 0.0f;
	private float pitch = 0.0f;
	//### VECTORS CAMERA
	public Vector3 y_minus = new Vector3(0, 0, 0); 
	//###
	public float movementSpeed = 5.0f;
	//

	public int level;
	void Start() {
		//ChangeWorld ("+Y");
		ChangeWorld("-Y");
	}
	void Update() {
		////DETECT WHERE TO CHANGE

	}
	public bool normal = false;

	public GameObject hit;
	void FixedUpdate()
	{
		//RaycastHit hit;

		//if (Physics.Raycast(transform.position, -Vector3.up, out hit))
		//	print("Found an object - distance: " + hit.distance);
		//{
			
		//}
		if (normal) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				RaycastHit hit;
				Ray ray = cam.GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (ray, out hit)) {
					if (hit.distance < 5) {
						CurrentStage = hit.transform.name;
						ChangeWorld (hit.transform.name);
					}
				}
			}
		} else {
			Debug.DrawLine (cam.transform.position, cam.transform.forward);
		}
		////
		//cam.transform.position = gameObject.transform.position + new Vector3(0, 0.5f, 0);
		// ### FOR TESTING PURPOSE ###
		if (Input.GetKeyDown (KeyCode.Tab)) {
			Debug.Log ("Debug Paused");
			Debug.Break ();
		}
		///
		if (useMouse) {
			yaw += speedH * Input.GetAxis ("Mouse X");
			pitch -= speedV * Input.GetAxis ("Mouse Y");
		} else {
			yaw += speedH * Input.GetAxis("Horizontal");
			pitch -= speedV * Input.GetAxis("Vertical");
		}

		Vector3 forward = Vector3.zero;
		Vector3 right = Vector3.zero;

		if (CurrentStage == "-Y") {
			cam.transform.eulerAngles = new Vector3 (pitch, yaw, 0.0f);

			forward = cam.transform.forward;
			forward = new Vector3(forward.x, 0.0f, forward.z);
			right = cam.transform.right;
			right = new Vector3(right.x, 0.0f, right.z);
		}
		if (CurrentStage == "+Y") {
			cam.transform.eulerAngles = new Vector3 (-pitch, -yaw, -180.0f);

			forward = cam.transform.forward;
			forward = new Vector3(forward.x, 0.0f, -forward.z);
			right = cam.transform.right;
			right = new Vector3(right.x, 0.0f, right.z);
		}

		if (CurrentStage == "+Z") {
			cam.transform.eulerAngles = new Vector3 (-yaw,pitch,90.0f);

			forward = cam.transform.forward;
			forward = new Vector3(forward.y, 0, forward.z);
			right = cam.transform.right;
			right = new Vector3(right.y, 0.0f, right.z);

			//cam.transform.localRotation = Quaternion.Euler(cam.transform.localRotation.x, cam.transform.localRotation.y, 0);
		}
		if (CurrentStage == "-Z") {
			cam.transform.eulerAngles = new Vector3 (yaw,pitch,-90.0f);

			forward = cam.transform.forward;
			forward = new Vector3(-forward.y, 0, forward.z);
			right = cam.transform.right;
			right = new Vector3(-right.y, 0.0f, -right.x);
		}
		/*
		if (CurrentStage == "+X") {
			cam.transform.eulerAngles = new Vector3 (yaw,0.0f,-pitch);
		}
		if (CurrentStage == "-X") {
			cam.transform.eulerAngles = new Vector3 (-pitch,yaw,0);
		}
		*/
		//Movement

		if (Input.GetKey (KeyCode.W)) {
			transform.Translate(forward * Time.deltaTime * movementSpeed);
		}
		if (Input.GetKey (KeyCode.S)) {

			transform.Translate(- forward * Time.deltaTime * movementSpeed);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate(-right * Time.deltaTime * movementSpeed);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate(right * Time.deltaTime * movementSpeed);
		}
	}

	void ChangeWorld(string i) {
		Debug.Log ("World was changed by: " + i);
		string[] names = {
			"-Y",
			"+Y",
			"+Z",
			"-Z",
			"+X",
			"-X"
		};
		int b;
		for (b = 0; b < names.Length; b++) {
			if (names [b] == i) {
				break;
			}
		}

		GameObject Level = GameObject.Find ("Level");

		for(int a = 0; a < Level.transform.childCount; a++)
		{
			GameObject child = Level.transform.GetChild (a).gameObject;
			//Level.transform.GetChild (a).gameObject.GetComponent<Renderer> ().material = CurrMaterial;
			if (child.name == names [b]) {
				child.GetComponent<Renderer> ().material = CurrMaterial;
			} else {
				child.GetComponent<Renderer> ().material = OtherMaterial;
			}
		}

		Vector3[] dir = { 
			new Vector3(0, -5, 0), // -Y
			new Vector3(0, 5, 0), // +Y
			new Vector3(5, 0, 0), // +Z
			new Vector3(-5, 0, 0), // -Z
			new Vector3(0, 0, 5), // +X
			new Vector3(0, 0, -5) // -X
		};
		Vector3[] playerRotate = {
			new Vector3 (0, 0, 0), // -Y
			new Vector3 (-180,0,0), // +Y
			new Vector3 (0, 0,90), // +Z
			new Vector3 (0, 0,270), // -Z
			new Vector3 (-90, 0, 0), // +X
			new Vector3 (90, 0, 0) // -X
		};
		Physics.gravity = dir[b];
		transform.rotation = Quaternion.Euler (playerRotate[b]);
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Finish")
		{
			Debug.Log ("Loading Next Level");
			//Debug.Break ();
			Application.LoadLevel(level);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Pressed");

			if (col.gameObject.name != CurrentStage) {
				Debug.Log ("Changing");
				CurrentStage = col.gameObject.name;
				ChangeWorld (col.gameObject.name);
			}
		}
		Debug.Log (col.gameObject.name);
	}

	void OnCollisionStay(Collision col) {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Pressed");

			if (col.gameObject.name != CurrentStage) {
				Debug.Log ("Changing");
				CurrentStage = col.gameObject.name;
				ChangeWorld (col.gameObject.name);
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Anomaly") {
			cam.gameObject.GetComponent<Vortex> ().enabled = true;
		}
		if (col.gameObject.tag == "Portal") {
			gameObject.transform.position = GameObject.Find ("Destination").transform.position;

			Physics.gravity = new Vector3 (0, 5, 0);
			transform.rotation = Quaternion.Euler (-180,0,0);
			CurrentStage = "+Y";
			//CurrentStage = "+Y";
			ChangeWorld ("+Y");
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Anomaly") {
			cam.gameObject.GetComponent<Vortex> ().enabled = false;
		}
	}
}

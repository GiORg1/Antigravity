using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	public float Speed;
	public float OriginZ;
	public float Distance;
	public bool direction;

	void Start()
	{
		direction = true;   
	}

	void OnCollisionEnter(Collision collision)
	{
		print("collision");
		if  (collision.gameObject.tag == "Platform")
		{
			if (direction = true)
			{
				direction = false;
			}
			else if (direction = false)
			{
				direction = true;   
			}
		} 
	}

	void Update(){
		if (direction = true)
		{
			transform.Translate (0, 0, -Speed * Time.deltaTime);
			print ("true");
		}
		else if (direction = false)
		{
			transform.Translate(0, 0, Speed * Time.deltaTime);  
			print ("false");
		}

	}

}

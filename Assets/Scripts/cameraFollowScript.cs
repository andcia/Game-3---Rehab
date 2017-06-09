// John Scerri 12/5/17
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowScript : MonoBehaviour
{
	// Object to Follow
	public GameObject obj;
	// Object to Follow Pos
	Vector3 objPos = new Vector3();
	// Camerta pos
	Vector3 camPos = new Vector3();
	// The Distance between tha cmaera and the object to follow
	float dist;



	// Use this for initialization
	void Start()
	{
		camPos = gameObject.transform.position;
		objPos = obj.transform.position;
		dist = objPos[2] - gameObject.transform.position.z;
	}

	// Update is called once per frame
	void Update()
	{
		objPos = obj.transform.position;

		gameObject.transform.position = new Vector3(objPos[0], camPos[1], objPos[2] - dist);

	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	float speed = 10f;
	float spacing = 2.3f;
	Vector3 pos;

	// Use this for initialization
	void Start()
	{

		pos = transform.position;
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.W))
			pos.y += spacing;
		if (Input.GetKeyDown(KeyCode.S))
			pos.y -= spacing;
		if (Input.GetKeyDown(KeyCode.A))
			pos.x -= spacing;
		if (Input.GetKeyDown(KeyCode.D))
			pos.x += spacing;

		transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {

	// INventory
	bool isInvOpen = false;

	// this controls the rigidbody movement
	Vector3 _movement;

	// this is how fast the capsule will move
	public float speed = 10f;

	// refer to the Rigidbody component
	// this does not mean that the Rigidbody will be available
	// by default.
	protected Rigidbody _rigidbody;


	// when the object wakes up (is created), get its components
	void Awake () {

		// Get the Rigidbody component on this object.
		// GetComponent <Type> ();
		_rigidbody = GetComponent <Rigidbody> ();

	}

	
	// Fixed Update is used for Physics and Regular Intervals
	void FixedUpdate () {
		if (!isInvOpen) {
			// the player will press on the left and right keys
			float horizontal = Input.GetAxisRaw ("Horizontal");

			// the player will press on the up and down keys
			float vertical = Input.GetAxisRaw ("Vertical");

			Move (horizontal, vertical);
		}
	}


	// Controls the player movement
	void Move (float h, float v) {

		// Sets the movement I'm inputting on the keyboard
		_movement.Set (h, 0, v);

		// make sure the player is moving on a circle, not a square
		_movement = _movement.normalized * speed * Time.deltaTime;

		// moves the player to a specified position
		_rigidbody.MovePosition (transform.position + _movement);

		// rotate the player towards the direction we want
		if (_movement != Vector3.zero) {
			_rigidbody.MoveRotation (Quaternion.LookRotation (_movement));
		}
	}

	// Set id INventoryOpen
	public bool SetInventoryOpen{
		set{ isInvOpen = value;}
	}
}

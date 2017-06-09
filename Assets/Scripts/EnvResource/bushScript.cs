using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bushScript : MonoBehaviour {

	// Game Controller
	GameObject cont;
	// INventory
	GameObject inv;

	// Fruit Group
	GameObject fruitGrp;

	// Obejct number
	int itemNum = 1;
	int itemfill = 10;

	// Grow Time
	float growtime = 10f; // planed 4800sec
	float curTime = 0f;
	bool isGrowing = false;

	// Is player in proximity
	bool inProxy = false;

	// Use this for initialization
	void Start () {
		// Ste Game  Controller
		cont = gameObject.transform.parent.gameObject;
		// Set Inventory
		inv = cont.GetComponent<envirnmentControlScript> ().GetInventory;
		// Set Fruit Group
		fruitGrp = gameObject.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		// If player is near Bush
		if (inProxy) {
			// If player press key
			if (Input.GetKeyDown (KeyCode.F) && !inv.activeSelf && !isGrowing) {
				inv.GetComponent<inventoryScripts> ().AddItem (itemNum, itemfill);
				// Set is growing to trough
				isGrowing = true;
				fruitGrp.SetActive (false);
			}
		}

		// Grow Fruit
		GrowFruit();
	}

	// Grow fruit
	void GrowFruit(){
		if (isGrowing) {
			// Time Update
			curTime += Time.deltaTime;
			// If its time toi Grow, set fruit to active
			if (curTime >= growtime) {
				curTime = 0;
				isGrowing = false;
				fruitGrp.SetActive (true);
			}

		}
	}

	// If player enters Bush
	void OnTriggerEnter(Collider  other){
		if (other.tag == "Player") {
			inProxy = true;
		}
	}

	// If player Exits bush
	void OnTriggerExit (Collider other){
		if (other.tag == "Player") {
			inProxy = false;
		}
	}

}

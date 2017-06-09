// John Scerri 12/5/17
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logScript : MonoBehaviour {

    GameObject cont;
    GameObject inventory;
    bool isTriggerd = false;

    public float destroyTime;
    float curTime = 0;
    public int itemNum = 0;
    public int itemFill = 1;

    private void Start() {
		inventory = cont.GetComponent<envirnmentControlScript> ().GetInventory;
    }

    // Update is called once per frame
    void Update () {
		// Update Time
        curTime += Time.deltaTime;
		// if destroy Time == 0, do not destroy object
		// else if current time exceeds destroy time, destroy object
        if(destroyTime > 0 && curTime >= destroyTime) {
            DestroyObject(gameObject);
        }

		// If player enters log bounds, check for pickup Key
        if (isTriggerd) {
            CheckKey();
        }
	}

    // When player enters object collider and presees key
    // PIckUp Item
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            isTriggerd = true;
        }
    }

	// Check if player is in bounds
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            isTriggerd = false;
        }
    }

	//Check if player is pressing pickup key
    void CheckKey() {
        if (Input.GetKeyDown(KeyCode.F)) {
            // Update Inventory
            inventory.GetComponent<inventoryScripts>().AddItem(itemNum, itemFill);
            // Destroy Object
            DestroyObject(gameObject);
        }
    }

	// Set Game Controller
	public GameObject SetGameController {
		set{ cont = value; }
	}
}

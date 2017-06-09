// John Scerri 18/5/17
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterScript : MonoBehaviour {

    GameObject cont;
    // INventory
    GameObject inv;

    // Is player in Water?
    bool inProxy = false;

	// Use this for initialization
	void Start () {
        // Find Game Controller
        cont = gameObject.transform.parent.gameObject;
        // Find INventory
        inv = cont.GetComponent<envirnmentControlScript>().GetInventory;
	}
	
	// Update is called once per frame
	void Update () {
        if (inProxy) {
            if (Input.GetKeyDown(KeyCode.F)) {
                // If inventory is open, fill bottle
                if (inv.activeSelf) {
                    inv.GetComponent<inventoryScripts>().BottleFill();
                }
                // Player Drink
                else {
                    inv.GetComponent<inventoryScripts>().PlayerDrink();
                }
            }
        }
	}

    // If Player eneters water
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            // Set is in proxy to true
            inProxy = true;
        }
    }

    // If Player exits water
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            inProxy = false;
        }
    }
}

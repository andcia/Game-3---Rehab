using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireCampScript : MonoBehaviour {

    // Game Controller
    GameObject cont;
    // INventory
    GameObject inv;

    // Log Count
    int logCount = 5;
    int curLog = 4;

    // Log Models
    GameObject[] logModel = new GameObject[5];
    // Fire
    GameObject fire;
    bool isBurning;
    float timePerLog = 5.0f; // 2 min =  120 sec
    float curTime = 0f;

    // PLayer in Proximity
    bool inProxy = false;

	// Use this for initialization
	void Start () {
        // Populate Log Models
        logModel[0] = gameObject.transform.GetChild(0).gameObject;
        logModel[1] = gameObject.transform.GetChild(1).gameObject;
        logModel[2] = gameObject.transform.GetChild(2).gameObject;
        logModel[3] = gameObject.transform.GetChild(3).gameObject;
        logModel[4] = gameObject.transform.GetChild(4).gameObject;
        // Fire
        fire = gameObject.transform.GetChild(5).gameObject;
        // TurnOff fire
        fire.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (inProxy) {
            if (!inv.activeSelf && Input.GetKeyDown(KeyCode.F)) {
                // If there are logs to burn
                if (logCount > 0) {
                    if (isBurning) {
                        isBurning = false;
                    }
                    else {
                        isBurning = true;
                    }
                }
            }
            // Else if Inventory is Open and F is pressed, Add fire wood
            else if (inv.activeSelf && Input.GetKeyDown(KeyCode.F)) {
                inv.GetComponent<inventoryScripts>().AddFireWood(gameObject);
            }
            
        }
        // Burn Fire
        Burn();
	}

    // Burn
    void Burn() {
        
        // Turn on Fire
        if (isBurning && !fire.activeSelf) {
            fire.SetActive(true);
        }
        // else Turn Off
        else if (!isBurning && fire.activeSelf) {
            fire.SetActive(false);
        }
        // If On, Burn Logs
        if (isBurning) {
            curTime += Time.deltaTime;
            // If time of log ended, remove log and step to next
            if(curTime >= timePerLog) {
                logCount -= 1;
                curTime = 0;
                logModel[curLog].SetActive(false);
                curLog -= 1;
                // Turn off if all logs are consumed
                if(logCount == 0) {
                    isBurning = false;
                }
            }
        }
    }

    // If player is in Proximity
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            inProxy = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            inProxy = false;
        }
    }

    // Set Inventory
    public GameObject SetInv {
        set { inv = value; }
    }

    // Set Game Controller
    public GameObject SetGameController {
        set { cont = value; }
    }

    // Get Set Log Count
    public int GetSetLogCount {
        get { return logCount; }
        set { logCount += value;
            curLog += 1;
            logModel[curLog].SetActive(true);
        }
    }
}

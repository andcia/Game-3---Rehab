using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Player Belongings and buildings
 * Note that Inventory is opened from envirnemntControlScript by checking for spacebar
*/

public class inventoryScripts : MonoBehaviour {

	// PLayer
	GameObject player;
    // Game Controller
    GameObject cont;

    // Inventorys
    GameObject[] invList = new GameObject[2];

	// Amount of items in inventory
	int[] resItemCount = new int[3]{0,0,0};
    const int maxBottleFill = 100;
    // Price of Items
    int[] buildItemCost = new int[4];
    // Bulding Space from object to player
    const float spaceing = 1.5f;

    // Sprites
    public GameObject selectionFrame;
    // Resource
    // Item List: 0 = logs, 1 = fruit, 2 =  water
    GameObject[] resItemList = new GameObject[3];
	public Sprite[] activeSpr = new Sprite[3];
	public Sprite[] inactiveSpr = new Sprite[3];
    // Inventory Item Pos
    const int log = 0;
    const int fruit = 1;
    const int waterBottle = 2;
	
    // Building
    // 0 = campFire, 1 = Camp, 2 = House, 3 = Mansion
    GameObject[] buildItemList = new GameObject[4];
    // Item Prefab
    public GameObject[] buildingPrefabList = new GameObject[4];

    // Current selected Item
    int curSelected = 0;
    int invSelection = 0; // 0 = resInv, 1 = bulidInv
    const int resInv = 0;
    const int builInv = 1;

	// Use this for initialization
	void Awake () {
        // Set Inventories
        invList[0] = gameObject.transform.GetChild(0).gameObject;
        invList[1] = gameObject.transform.GetChild(1).gameObject;

        // populate Resource Item List
        resItemList[0] = invList[0].transform.GetChild(0).gameObject; // Log
        resItemList[1] = invList[0].transform.GetChild(1).gameObject; // Fruit
        resItemList[2] = invList[0].transform.GetChild(2).gameObject; // Water

        // Populate Building Item List
        buildItemList[0] = invList[1].transform.GetChild(0).gameObject; // Fire
        buildItemList[1] = invList[1].transform.GetChild(1).gameObject; // Camp
        buildItemList[2] = invList[1].transform.GetChild(2).gameObject; // House
        buildItemList[3] = invList[1].transform.GetChild(3).gameObject; // Mansion

        // Update Item Cost
        buildItemCost[0] = int.Parse(buildItemList[0].transform.GetChild(0).gameObject.GetComponent<Text>().text);
        buildItemCost[1] = int.Parse(buildItemList[1].transform.GetChild(0).gameObject.GetComponent<Text>().text);
        buildItemCost[2] = int.Parse(buildItemList[2].transform.GetChild(0).gameObject.GetComponent<Text>().text);
        buildItemCost[3] = int.Parse(buildItemList[3].transform.GetChild(0).gameObject.GetComponent<Text>().text);

        // Move Slection on first item
        MoveSelection();
		// Set sprites to inactive
		resItemList[0].GetComponent<Image>().sprite = inactiveSpr[0];
		resItemList[1].GetComponent<Image>().sprite = inactiveSpr[1];
		resItemList[2].GetComponent<Image>().sprite = inactiveSpr[2];

        // Turn Off buildIOnv
        invList[1].SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		ScrollItems ();
        SwitchInventory();
        // Check if action button is pressed
        if (Input.GetKeyDown(KeyCode.F)) {
            // If in ResInv
            if(invSelection == resInv) {

            }
            // Or in Building Inv
            else {
                Build(curSelected);
            }
        }
	}


    // INventory Actions ___________________________________________________________________________________
	// Add Item on pickup
	public void AddItem(int num, int fill){
		// num = The position in list
		// fill = The amount to add

		// Chekc if list is empty
		if(resItemCount[num] == 0){
			// change Sprite sprite
			resItemList[num].GetComponent<Image>().sprite = activeSpr[num];
			// Add count
			AddRemoveCount(num, fill);
		}
		// else if already filled
		else{
			// Add count
			AddRemoveCount(num, fill);
		}
	}

	// Remove item on use
	public void RemoveItem(int num, int fill){
		int newAmount = resItemCount [num] - fill;

		// If amount removed is smaller then 0
		// Do not allow action
		if (newAmount < 0) {
            return;
		}
		// If newAmount = 0, then  deactivate icon
		else if (newAmount == 0) {
			resItemList [num].GetComponent<Image> ().sprite = inactiveSpr [num];
		}
		// Else update amount
		else{
			AddRemoveCount (num, newAmount, false);
		}
	}


	// Add remove Count
	void AddRemoveCount(int num, int fill, bool isAdd = true){
		int action = 1;
		// Change to negative if remove item
		if (!isAdd) {
			action = -1;
		}
		// Update
		resItemCount[num] += (action * fill);
        // Update on Screen Text
        resItemList[num].transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + resItemCount[num];

	}

    // Lets you manualy input how much of that item is available
    public void ManualItemCountSet(int num, int fill) {
        // Update
        resItemCount[num] = fill;
        // Update on Screen Text
        resItemList[num].transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + resItemCount[num];
    }

    // Scroll trough inventory items
	void ScrollItems(){
		if (Input.GetKeyDown (KeyCode.A)) {
			curSelected -= 1;
			CleanSelection (false);
		} 
		else if (Input.GetKeyDown(KeyCode.D)){
			curSelected += 1;
			CleanSelection (true);
		}
		// Update Selection
		MoveSelection();
	}

	// If Selection ecxeeds lenght of array or is smaller then 0 is looped 
	void CleanSelection(bool isPos){
        int totalIcons;
        if (invSelection == resInv) {
            totalIcons = resItemList.Length;
        }
        else {
            totalIcons = buildItemList.Length;
        }
		// I selection is incersing
		if (isPos) {
			if (curSelected > totalIcons - 1) {
				curSelected = 0;
			}
		}

		// else if selection is decreasing
		else {
			if (curSelected < 0) {
				curSelected = totalIcons - 1;
			}
		}
	}

	// Move Selection Frame
	void MoveSelection(){
        // Set selection frame
        if (invSelection == resInv) {
            selectionFrame.transform.position = resItemList[curSelected].transform.position;
        }
        else {
            selectionFrame.transform.position = buildItemList[curSelected].transform.position;
        }
	}

    // Switch from Inventory to other inventory
    void SwitchInventory() {
        if (Input.GetKeyDown(KeyCode.E)) {
            // Turn On resINv
            if (invSelection == builInv) {
                invList[0].SetActive(true);
                invList[1].SetActive(false);
                invSelection = resInv;
            }
            // Turn On BuildInv
            else {
                invList[1].SetActive(true);
                invList[0].SetActive(false);
                invSelection = builInv;
            }
        }
    }

    // INventory Setup  ___________________________________________________________________________________
    // Set Player
    public GameObject SetPlayer{
		set{ player = value;}
	}

    // Set Game Controller
    public GameObject SetGameController {
        set { cont = value; }
    }

    // INventory World Interaction ___________________________________________________________________________________
    // Build Objects
    void Build(int num) {
        // Build if enopugh Resources
        if(resItemCount[0] >= buildItemCost[num]) {
            // Create Object
            GameObject obj = Instantiate(buildingPrefabList[num]);
            // Set Inventory to refer
            obj.GetComponent<fireCampScript>().SetInv = gameObject;
            // Set object infront of player and offset
            Vector3 pos = player.transform.position;
            pos[2] += spaceing;
            pos[1] = 0; // Ray Cast?? ______________________________________________________________________***
            obj.transform.position = pos;
            // Remove cost fromInventory
            AddRemoveCount(0, buildItemCost[num], false);
        }
    }

    // Add Fire Wood to Camp Fire
    public void AddFireWood(GameObject fire) {
        // if selection == wood, and wood is available
        if (invSelection == resInv && curSelected == log && resItemCount[0] > 0) {
            // if space in campFire
            if(fire.GetComponent<fireCampScript>().GetSetLogCount < 5) {
                // if F is pressed, add wood
                if (Input.GetKeyDown(KeyCode.F)) {
                    // Subtract wood from inventory
                    AddRemoveCount(0, 1, false);
                    // Add wood to campFire
                    fire.GetComponent<fireCampScript>().GetSetLogCount = 1;
                }
            }

        }
    }

    // Bottle Fill
    public void BottleFill() {
        // If player is in inventory and has the bottle selected,
        // if there is space in bottle, full fill bottle
        if(invSelection == resInv && curSelected == waterBottle && resItemCount[waterBottle] != maxBottleFill) {
            ManualItemCountSet(waterBottle, maxBottleFill);
        }
    }

    // Player Drink
    public void PlayerDrink() {

    }
}

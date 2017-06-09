// John Scerri 12/5/17
using UnityEngine;
using System.Collections;

public class treeScripts : MonoBehaviour {

	// Controller
	GameObject cont;

    // Is player near Tree
    bool inProxy = false;

    // Time
    public float growTime;
    float curTime = 0f;

    // Growing Stage
    public GameObject[] treeStages = new GameObject[3]; // 0= stump, 1= small Tree, 3 = large tree;
    int curStage = 2;
    bool isGrowing = false;

    // Hits
    public int killHit;
    int hitCount = 0;
    public float hitDelay;
    float curHitTime = 0f;
    bool isHit = false;

    // Log Reward
    public int logReward;
    public GameObject logPrefab;

    // Player -- will be found once it hits tree, on trigger eneter
    GameObject player;

	void Awake(){
        // Set Game Controller
		cont = gameObject.transform.parent.gameObject;
    }

	
	// Update is called once per frame
	void Update () {
        // Grow Tree
        GrowTree();
        // If player in proximity Check key down
        CheckKey();
	}

    // Grow Tree
    void GrowTree() {
        // If tree is Growing... Grow
        if (isGrowing) {
            // Update Time
            curTime += Time.deltaTime;
            // Check time with GrowTime
            // If curTime > growTime
            if (curTime >= growTime) {
                // Trun off small Tree
                treeStages[1].SetActive(false);
                // Turn On Large Tree
                treeStages[2].SetActive(true);
                // Update Stage
                curStage = 2;
                // Ste growing to off
                isGrowing = false;
                // Reset Timer
                curTime = 0f;
            }
            // If curTime >= Half the grow Time, change model to small tree
            else if (curTime >= growTime / 2f) {
                // Turn off stump
                treeStages[0].SetActive(false);
                // Turn On Small tree
                treeStages[1].SetActive(true);
                // Update Stage
                curStage = 1;
            }
        }

    }

    // If player is inside Trigger Check for key
    // If tree is tump, do not check for key
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            if(player == null) {
                // Set PLayer
                player = other.gameObject;
            }
            // Set proxy
            inProxy = true;
        }
    }
    // ON exit - In proximity is false
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            inProxy = false;
        }
    }

    // Check for Key
    void CheckKey() {
        if (curStage != 0) {
            // Update Hit Time
            if (hitCount > 0 && isHit) {
                curHitTime += Time.deltaTime;
                // If delay time passed, enable Hit
                if (curHitTime >= hitDelay) {
                    curHitTime = 0;
                    isHit = false;
                }
            }
            // Check key if player is near
            if (inProxy && !isHit) {
                if (Input.GetKeyDown(KeyCode.F)) {
                    isHit = true;
                    HitTree();
                }
            }
        }
    }

    // Hit Tree
    public void HitTree(int hit = 1) {
        // If tree is not a stump
        // Hit it
        hitCount += 1;
        // Check if tree ghets choped down
        KillTree();
    }

    // Kill Tree
    void KillTree() {
        if (hitCount == killHit) {
            // Give logs
            // if tree is small, log reward is devided by 2
            LogDispenser();
            // Reset is Growing
            isGrowing = true;
            // Reset Time
            curTime = 0;
            // reset Hit count
            hitCount = 0;
            // reset is Hit
            isHit = false;
            // reset Hit Time
            curHitTime = 0f;
            // Turn Off tree
            treeStages[curStage].SetActive(false);
            // Turn on stump
            treeStages[0].SetActive(true);
            // reset Current Stage
            curStage = 0;
        }
    }

    // Log dispanser
    void LogDispenser() {

        int reward = logReward;
        if(curStage == 1) {
            reward = reward / 2;
        }
        // if tree is killed, give logs to player
        for(int i=0; i<reward; i++) {
            GameObject log = Instantiate(logPrefab);
            Vector3 logPos = player.transform.position;
            log.transform.position = LogScatter(logPos);
			log.GetComponent<logScript> ().SetGameController = cont;
        }
    }

    // Log Scatter
    Vector3 LogScatter(Vector3 pos) {
        int[] randAxis = new int[2] { 0, 2 };
        pos[randAxis[Random.Range(0, 2)]] += Random.Range(-1.5f, 1.5f);
        return pos;
    }
		
}

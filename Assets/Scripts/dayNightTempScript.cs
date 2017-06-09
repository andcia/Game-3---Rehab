using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightTempScript : MonoBehaviour {

	// Directional Light
	public GameObject dirLight;
	float maxLight = 1f;
	float minLight = 0f;
	float curLightint = 1f;
	float diff;

	// Temp Value
	public int temperatureValue = 13;

	// COlours
	public Color dayCol;
	public Color nightCol;

	// Time
	float dayTime = 60; // planed 600sec
	float curTime = 0f;
	float vectorTime = 0f;
	bool toDay = false;

	// Use this for initialization
	void Awake () {
		diff = Calcdiff (minLight, maxLight);
	}
	
	// Update is called once per frame
	void Update () {
		//Change  background colour Day/night (10 mins)
		// time update
		curTime += Time.deltaTime;
		// Calculate Time Vecotor
		vectorTime = calcVector (curTime, dayTime, 1);
		// update Time Direction
		TimeDirection();
		// shift Colour
		Camera.main.backgroundColor = Color.Lerp(dayCol, nightCol, vectorTime);
		// Update Light
		ChangeLightInt();
		//temperatureText.temperature += 1;
	}

	//calculate Vector
	float calcVector(float curNum, float maxNum, float vect){
		return ((curNum / maxNum) * vect);
	}

	// Time direction
	void TimeDirection(){
		// Change Vector Direction
		if (toDay) {
			vectorTime = 1 - vectorTime;
		}

		//  If time is == to dayTime
		if (curTime >= dayTime) {
			curTime = 0f;
			if (toDay) {
				toDay = false;
			}
			else {
				toDay = true;
			}
		}
	}

	// ChangeLight Intesity
	void ChangeLightInt(){
		curLightint = maxLight - calcVector (vectorTime, 1, diff);
		dirLight.GetComponent<Light> ().intensity = curLightint;
	}

	// Calculate Difference between
	float Calcdiff(float fromNum, float toNum){
		return toNum - fromNum;
	}
}

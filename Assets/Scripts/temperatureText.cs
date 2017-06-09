using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class temperatureText : MonoBehaviour
{
	public static int temperature;        // The player's score.


	Text text;                      // Reference to the Text component.


	void Awake()
	{
		// Set up the reference.
		text = GetComponent<Text>();

		// Reset the score.
		temperature = 13;
	}


	void Update()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		text.text = "Temperature: " + temperature;
	}
}
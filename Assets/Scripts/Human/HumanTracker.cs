using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTracker : MonoBehaviour {
	public float HappinessMax;
	public float HungerMax;
	public float CleanlinessMax;

	private float Happiness;
	private float Hunger;
	private float Cleanliness;

	public HumanTrackerUI TrackerUI;

	public void SetHappiness(float val) {
		Happiness = val;
		TrackerUI.SetHappiness(val, HappinessMax);
	}

	public void SetHunger(float val) {
		Hunger = val;
		TrackerUI.SetHappiness(val, HungerMax);
	}

	public void SetCleanliness(float val) {
		Cleanliness = val;
		TrackerUI.SetCleanliness(val, CleanlinessMax);
	}
}

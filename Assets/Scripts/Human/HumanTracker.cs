using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTracker : MonoBehaviour {
	[Header("Happiness")]
	public float Happiness;
	public float HappinessMax;

	[Header("Hunger")]
	public float Hunger;
	public float HungerMax;
	public float MinHungerToEat;

	[Header("Cleanliness")]
	public float Cleanliness;
	public float CleanlinessMax;

	public HumanTrackerUI TrackerUI;

	public int attention;
	public int annoyance;

	void Start() {
		TrackerUI.SetHappiness(Happiness, HappinessMax);
		TrackerUI.SetHunger(Hunger, HungerMax);
		TrackerUI.SetCleanliness(Cleanliness, CleanlinessMax);
	}

	public void SetHappiness(float val) {
		Happiness = val;
		TrackerUI.SetHappiness(val, HappinessMax);
	}

	public void SetHunger(float val) {
		Hunger = val;
		TrackerUI.SetHunger(val, HungerMax);
	}

	public void SetCleanliness(float val) {
		Cleanliness = val;
		TrackerUI.SetCleanliness(val, CleanlinessMax);
	}

	public void AddHappiness(float val) {
		Happiness += val;
	}

	public void ResetHunger() {
		SetHunger(HungerMax);
	}

	public void ResetCleanliness() {
		SetCleanliness(CleanlinessMax);
	}
}
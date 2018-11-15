using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanTrackerUI : MonoBehaviour {
	public Slider HappinessSlider;
	public Slider HungerSlider;
	public Slider CleanlinessSlider;

	public void SetHappiness(float val, float max) {
		HappinessSlider.maxValue = max;
		HappinessSlider.value = val;
	}

	public void SetHunger(float val, float max) {
		HungerSlider.maxValue = max;
		HungerSlider.value = val;
	}

	public void SetCleanliness(float val, float max) {
		CleanlinessSlider.maxValue = max;
		CleanlinessSlider.value = val;
	}
}

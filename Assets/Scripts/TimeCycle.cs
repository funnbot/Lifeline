using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour {
	public int Day;
	public int Hour;

	public Light Sun;
	public Color DayColor;
	public Color NightColor;

	public float DayInMinutes;
	public int MorningHour;
	public int NightHour;

	public bool paused;

	void Start() {
		StartCoroutine(DayRoutine());
	}

	void Update() {
		UpdateSunlight();
	}

	public bool IsNight() {
		return Hour >= NightHour && Hour < MorningHour;
	}

	public void SkipToMorning() {
		
	}

	public void SetHour() {

	}

	void TriggerMorning() {

	}

	void TriggerNight() {

	}

	void NewDay() {
		Human.Tracker.SetHunger(3);
		Human.Tracker.SetCleanliness(4);
	}

	void UpdateSunlight() {
		if (paused) return;
		
	}

	IEnumerator DayRoutine() {
		var hour = DayInMinutes / 24;
		var wait = new WaitForSeconds(hour * 60);
		while (true) {
			if (!paused) {
				Hour++;
				if (Hour >= 24) {
					Hour = 0;
					Day++;
					NewDay();
				} else if (Hour == MorningHour) TriggerMorning();
				else if (Hour == NightHour) TriggerNight();
			}
			yield return wait;
		}
	}

	public static TimeCycle Access;
	void Awake() {
		if (Access != null) Destroy(gameObject);
		Access = this;
	}
}
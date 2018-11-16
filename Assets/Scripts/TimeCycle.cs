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

	public bool IsNight() {
		return Hour >= NightHour || Hour < MorningHour;
	}

	public void SkipToMorning() {
		SetHour(MorningHour);
		NewDay();
	}

	public void SkipHour(int amount) {
		Hour += amount;
	}

	public void SetHour(int hour) {
		Hour = hour;
	}

	void TriggerMorning() {
		Human.Action.Queue(HumanActionType.Work);
	}

	void TriggerNight() {

	}

	void NewDay() {
		Human.Tracker.SetHunger(3);
		Human.Tracker.SetCleanliness(4);
	}

	void UpdateSunlight() {
		if (paused) return;
		float hour = Mathf.Abs(Hour - 12);
		float perc = hour / 12;
		Color col = Color.Lerp(DayColor, NightColor, perc);
		StartCoroutine(FadeColor(Sun.color, col));
	}

	IEnumerator FadeColor(Color col1, Color col2) {
		float lerp = 0f;
		while (lerp < 1f) {
			lerp += Time.deltaTime * 3;
			Sun.color = Color.Lerp(col1, col2, lerp);
			yield return null;
		}
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

				UpdateSunlight();
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
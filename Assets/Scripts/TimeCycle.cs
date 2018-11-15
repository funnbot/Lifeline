using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour {
	public int Day;
	public int Hour;

	public Light Sun;

	public float DayInMinutes;
	public int MorningHour;
	public int NightHour;

	public bool paused;

	private float SunRotSpeed;

	void Start() {
		StartCoroutine(DayRoutine());
		SunRotSpeed = (360 / DayInMinutes) / 60;
	}

	void Update() {
		SetSunRotation();
	}

	public void SkipHour(int amount) {
		Hour += amount;
		Sun.transform.Rotate(Vector3.left * SunRotSpeed * amount * 60);
	}

	void TriggerMorning() {

	}

	void TriggerNight() {

	}

	void NewDay() {

	}

	void SetSunRotation() {
		if (paused) return;
		if (Hour == 0) Sun.transform.localEulerAngles = new Vector3(-90, 0, 0);
		else {
			var sunRotAngle = new Vector3(-1, 0, 0);
			Sun.transform.Rotate(sunRotAngle * SunRotSpeed * Time.deltaTime);
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
			}
			yield return wait;
		}
	}

	public static TimeCycle Get;
	void Awake() {
		if (Get != null) Destroy(gameObject);
		Get = this;
	}
}
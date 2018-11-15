using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAction : MonoBehaviour {
	private Queue<HumanActionType> actions;
	private bool processing;

	void Start() {
		actions = new Queue<HumanActionType>();
		Queue(HumanActionType.Sleep);
	}

	void Update() {
		if (!processing) ProcessNext();
	}

	public void Queue(HumanActionType act) {
		actions.Enqueue(act);
	}

	void ProcessNext() {
		if (actions.Count == 0) return;
		processing = true;
		var action = actions.Dequeue();

		if (action == HumanActionType.Sleep) 
			StartCoroutine(DoSleep());
		else if (action == HumanActionType.Wake)
			StartCoroutine(DoWake());
		else if (action == HumanActionType.Eat)
			StartCoroutine(DoEat());
		else if (action == HumanActionType.Clean)
			StartCoroutine(DoClean());
		else if (action == HumanActionType.Bath)
			StartCoroutine(DoBath());
		else if (action == HumanActionType.Walk)
			StartCoroutine(DoWalk());
	}

	IEnumerator DoSleep() {
		yield return Human.Movement.GoTo("Bed");
		Debug.Log("At Bed");
	}

	IEnumerator DoWake() {
		yield return null;
	}

	IEnumerator DoEat() {
		yield return null;
	}

	IEnumerator DoClean() {
		yield return null;
	}

	IEnumerator DoBath() {
		yield return null;
	}

	IEnumerator DoWalk() {
		yield return null;
	}
}

public enum HumanActionType {
	Sleep,
	Wake,
	Eat,
	Clean,
	Bath,
	Walk
}
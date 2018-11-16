using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAction : MonoBehaviour {
	private Queue<HumanActionType> actions;
	private bool processing;

	public HumanActionState State;

	void Start() {
		actions = new Queue<HumanActionType>();
		// Queue(HumanActionType.Sleep);
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
		else if (action == HumanActionType.Bath)
			StartCoroutine(DoBath());
		else if (action == HumanActionType.Walk)
			StartCoroutine(DoWalk());
		else if (action == HumanActionType.Pet)
			StartCoroutine(DoPet());
		else if (action == HumanActionType.Work)
			StartCoroutine(DoWork());
	}

	IEnumerator DoSleep() {
		yield return Human.Movement.GoTo("Bed");
		yield return TriggerAnim("Lie Down");
		State = HumanActionState.Sleeping;
		processing = false;
	}

	IEnumerator DoWake() {
		yield return null;
		State = HumanActionState.Idle;
		processing = false;
	}

	IEnumerator DoEat() {
		if (Human.Tracker.Hunger > Human.Tracker.MinHungerToEat)
			Human.Tracker.annoyance++;
		else Human.Tracker.attention++;

		if (Human.Tracker.attention <= 2) {
			processing = false;
			yield break;
		}
		if (Human.Tracker.annoyance > 2) {
			Human.Dialog.TriggerDialog(HumanEmotion.Angry);
			if (Human.Tracker.annoyance > 4)
				Human.Tracker.AddHappiness(-1);
			processing = false;
			yield break;
		}

		yield return Human.Movement.GoTo("Fridge");
		Animators.TriggerAnim("Fridge", "Open");
		yield return TriggerAnim("Access Fridge");
		yield return Human.Movement.GoTo("Counter");
		yield return TriggerAnim("Use Counter");

		Human.Tracker.ResetHunger();
		State = HumanActionState.Eating;
		processing = false;
	}

	IEnumerator DoBath() {
		yield return null;
		State = HumanActionState.Bathing;
		processing = false;
	}

	IEnumerator DoWalk() {
		yield return null;
		State = HumanActionState.Walking;
		processing = false;
	}

	IEnumerator DoPet() {
		yield return null;
		State = HumanActionState.Petting;
		processing = false;
	}

	IEnumerator DoWork() {
		yield return null;
		State = HumanActionState.Working;
		processing = false;
	}

	IEnumerator TriggerAnim(string trigger) {
		Human.Animator.SetTrigger(trigger);
		var state = Human.Animator.GetCurrentAnimatorStateInfo(0);
		var length = state.length + state.normalizedTime;
		yield return new WaitForSeconds(length);
	}
}

public enum HumanActionType {
	Sleep,
	Wake,
	Work,
	Eat,
	Bath,
	Walk,
	Pet
}

public enum HumanActionState {
	Sleeping,
	Eating,
	Working,
	Bathing,
	Walking,
	Petting,
	Idle
}
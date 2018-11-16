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

	public void SetActive(bool active) {
		Human.Animator.enabled = active;
		processing = !active;
		Human.Movement.SetActive(active);
	}

	public void Queue(HumanActionType act) {
		// Don't repeat actions
		if (actions.Count != 0 && actions.Peek() == act) return;
		actions.Enqueue(act);
	}

	void ProcessNext() {
		if (actions.Count == 0) return;
		processing = true;
		var action = actions.Dequeue();

		Debug.Log("Action: " + action.ToString());

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
		else if (action == HumanActionType.OpenMenuDoor)
			StartCoroutine(OpenMenuDoor());
	}

	IEnumerator DoSleep() {
		State = HumanActionState.Sleeping;
		yield return Human.Movement.GoTo("Bed");
		yield return Human.Movement.GoTo("OnBed");
		yield return TriggerAnim("Lie Down");
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

		State = HumanActionState.Eating;

		yield return Human.Movement.GoTo("Fridge");
		// Animators.TriggerAnim("Fridge", "Open");
		yield return TriggerAnim("Access Fridge");
		yield return Human.Movement.GoTo("Counter");
		yield return TriggerAnim("Use Counter");

		Human.Tracker.ResetHunger();
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
		State = HumanActionState.Petting;
		yield return TriggerAnim("Pet");
		processing = false;
	}

	IEnumerator DoWork() {
		yield return null;
		State = HumanActionState.Working;
		processing = false;
	}

	IEnumerator OpenMenuDoor() {
		yield return new WaitForSeconds(2);
		yield return TriggerAnim("Use Counter");
		Human.Dialog.TriggerDialog(HumanEmotion.Question);
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
	Pet,
	OpenMenuDoor
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
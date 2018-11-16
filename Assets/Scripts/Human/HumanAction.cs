using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		if (Human.Tracker.Cleanliness < 5) {
			processing = false;
			yield break;
		}

		if (State == HumanActionState.Working) {
			Human.Tracker.annoyance++;
			processing = false;
			yield break;
		}
		State = HumanActionState.Walking;
		yield return Human.Movement.GoTo("Door");
		InputManager.SetState(false);
		yield return Pause.Access.FadeColor(Color.clear, Color.black);
		TimeCycle.Access.SkipHour(1);
		yield return new WaitForSeconds(2);
		yield return Pause.Access.FadeColor(Color.black, Color.clear);
		InputManager.SetState(true);

		Human.Tracker.AddHappiness(10);
		Human.Tracker.SetCleanliness(3);
		Human.Tracker.SetHunger(2);
	}

	IEnumerator DoPet() {
		State = HumanActionState.Petting;
		yield return Human.Movement.RotateTowards(Quaternion.Inverse(Dog.Transform.rotation));
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
		StartCoroutine(Animators.TriggerAnim("Door", "Open"));
		yield return TriggerAnim("Use Counter");
		Human.Dialog.TriggerDialog(HumanEmotion.Question);
		yield return DoPet();

		yield return Pause.Access.FadeColor(Color.clear, Color.black);
		SceneManager.LoadScene("MainGame");
	}

	IEnumerator TriggerAnim(string trigger) {
		Human.Animator.SetTrigger(trigger);
		var state = Human.Animator.GetCurrentAnimatorStateInfo(0);
		var length = state.length - state.normalizedTime;
		yield return new WaitForSeconds(length);
	}
}

public enum HumanActionType {
	Sleep,
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
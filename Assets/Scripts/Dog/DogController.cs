using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour {
	public float BarkCooldown;

	float barkTimer;

	void Update() {
		if (barkTimer > 0) barkTimer -= Time.deltaTime;

		var input = InputManager.Axis();
		var velocity = new Vector3(input.x, 0f, input.y);
		velocity = transform.InverseTransformDirection(velocity);
		Dog.Animator.SetFloat("Velocity", velocity.z);

		if (InputManager.Shift()) {
			Sit();
		}
		if (InputManager.Ctrl()) {
			StartCoroutine(LieDown());
		}
		if (InputManager.Space()) {
			Bark();
		}
	}

	void Sit() {
		Dog.Animator.SetTrigger("Sit");
	}

	IEnumerator LieDown() {
		yield return TriggerAnim("Lie Down");

		if (TimeCycle.Access.IsNight()) {
			Pause.Access.PauseScene(true);
			yield return Pause.Access.FadeColor(Color.clear, Color.black);
			TimeCycle.Access.SkipToMorning();
			yield return Pause.Access.FadeColor(Color.black, Color.clear);
			Pause.Access.PauseScene(false);
		}
	}

	void Bark() {
		if (barkTimer > 0) return;
		barkTimer = BarkCooldown;

		Dog.Dialog.TriggerDialog(DogEmotion.Bark);
		Dog.Animator.SetTrigger("Bark");
		Dog.Interactor.Bark();
	}

	IEnumerator TriggerAnim(string trigger) {
		Dog.Animator.SetTrigger(trigger);
		var state = Dog.Animator.GetCurrentAnimatorStateInfo(0);
		var length = state.length + state.normalizedTime;
		yield return new WaitForSeconds(length);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour {
	void Update() {
		var input = InputManager.Axis();
		var velocity = Mathf.Max(Mathf.Abs(input.x), Mathf.Abs(input.y));
		Dog.Animator.SetFloat("Velocity", velocity);

		if (InputManager.Shift()) {
			Dog.Animator.SetTrigger("Sit");
		}
		if (InputManager.Ctrl()) {
			Dog.Animator.SetTrigger("Down");
		}
		if (InputManager.Space()) {
			Dog.Animator.SetTrigger("Bark");
		}
	}
}

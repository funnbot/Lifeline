using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInteractor : MonoBehaviour {
	public float InteractionDist;
	public Transform CastPoint;

	void Start() {

	}

	void Update() {
		if (InputManager.Space()) {
			Bark();
		}
	}

	void Bark() {
		Dog.Dialog.TriggerDialog(DogEmotion.Bark);

		RaycastHit hit;
		if (BarkRaycast(out hit)) {
			var tag = hit.transform.tag;
			if (tag == "Food") {
				Human.Action.Queue(HumanActionType.Eat);
			}
			if (tag == "Bed") {
				Human.Action.Queue(HumanActionType.Sleep);
			}
			if (tag == "Human") {
				Human.Action.Queue(HumanActionType.Pet);
			}
		}
	}

	bool BarkRaycast(out RaycastHit hit) {
		Ray ray = new Ray(CastPoint.position, CastPoint.forward);
		return Physics.Raycast(ray, out hit, InteractionDist);
	}
}
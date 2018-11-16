using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInteractor : MonoBehaviour {
	public float InteractionDist;
	public Transform CastPoint;

	void Start() {

	}

	public void Bark() {
		RaycastHit hit;
		if (BarkRaycast(out hit)) {
			var tag = hit.transform.tag;
			if (tag == "Food") {
				Human.Action.Queue(HumanActionType.Eat);
			}
			else if (tag == "Bed") {
				Human.Action.Queue(HumanActionType.Sleep);
			}
			else if (tag == "Human") {
				Human.Action.Queue(HumanActionType.Pet);
			}
			else if (tag == "MenuDoor") {
				Human.Action.Queue(HumanActionType.OpenMenuDoor);
			}
			else if (tag == "FrontDoor") {
				Human.Action.Queue(HumanActionType.Walk);
			}
		}
	}

	bool BarkRaycast(out RaycastHit hit) {
		Ray ray = new Ray(CastPoint.position, CastPoint.forward);
		return Physics.Raycast(ray, out hit, InteractionDist);
	}
}
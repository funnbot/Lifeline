using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInteractor : MonoBehaviour {
	public float InteractionDist;
	public Transform CastPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (InputManager.Space()) {
			Bark();
		}
	}

	void Bark() {

	}

	bool BarkRaycast(out RaycastHit hit) {
		Ray ray = new Ray(CastPoint.position, CastPoint.forward);
		return Physics.Raycast(ray, out hit, InteractionDist);
	}
}

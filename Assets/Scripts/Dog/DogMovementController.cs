using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovementController : MonoBehaviour {
	public float MoveSpeed;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		var axis = InputManager.Axis();
		var input = new Vector3(-axis.y, 0, axis.x);
		if (input == Vector3.zero) return;
		MovePosition(input);
		MoveRotation(input);
	}

	void MovePosition(Vector3 input) {
		var newPos = transform.position + input.normalized * MoveSpeed * Time.deltaTime;
		rb.MovePosition(newPos);
	}

	void MoveRotation(Vector3 input) {
		var newRot = Quaternion.LookRotation(input, Vector3.up);
		rb.MoveRotation(newRot);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovementController : MonoBehaviour {
	public float MoveSpeed;
	public float RotateSpeed;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		var input = InputManager.Axis();
		if (input == Vector2.zero) return;
		MovePosition(input);
		MoveRotation(input);
	}

	void MovePosition(Vector2 input) {
		var newPos = transform.position + transform.forward * input.y * MoveSpeed * Time.deltaTime;
		rb.MovePosition(newPos);
	}

	void MoveRotation(Vector2 input) {
		transform.Rotate(Vector3.up * input.x * RotateSpeed * Time.deltaTime);
		//var newRot = Quaternion.LookRotation(input, Vector3.up);
		//rb.MoveRotation(newRot);
	}
}

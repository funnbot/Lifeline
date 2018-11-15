using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public bool active;

	public static Vector2 Axis() {
		if (!Instance.active) return Vector2.zero;
		return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	}

	public static bool Shift() {
		if (!Instance.active) return false;
		return Input.GetKeyDown(KeyCode.LeftShift);
	}

	public static bool Ctrl() {
		if (!Instance.active) return false;
		return Input.GetKeyDown(KeyCode.LeftControl);
	}

	public static bool Space() {
		if (!Instance.active) return false;
		return Input.GetKeyDown(KeyCode.Space);
	}

	public static void SetState(bool active) {
		Instance.active = active;
	}

	public static InputManager Instance;
	void Awake() {
		if (Instance != null) Destroy(gameObject);
		Instance = this;
	}
}
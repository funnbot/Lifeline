using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public bool active;
	public bool pauseWalking;

	public static Vector2 Axis() {
		if (!Instance.active || Instance.pauseWalking) return Vector2.zero;
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

	public static bool Escape() {
		// Only active in menu scene anyway
		if (Instance.pauseWalking) return false;
		return Input.GetKeyDown(KeyCode.Escape);
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
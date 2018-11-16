using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
	public float FadeSpeed;
	public Image PauseImage;

	public bool isPaused;

	void Start() {
		LockCursor();
	}

	void Update() {
		if (InputManager.Escape()) {
			if (isPaused) {
				isPaused = false;
				PauseScene(false);
				LockCursor();
			} else {
				isPaused = true;
				PauseScene(true);
			}
		}
	}

	public void PauseScene(bool pause) {
		InputManager.SetState(!pause);
		TimeCycle.Access.paused = pause;
		Human.Action.SetActive(!pause);
	}

	public IEnumerator FadeColor(Color col1, Color col2) {
		float lerp = 0;
		while (lerp < 1f) {
			lerp += Time.deltaTime * FadeSpeed;
			PauseImage.color = Color.Lerp(col1, col2, lerp);
			yield return null;
		}
	}

	public void LockCursor() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public static Pause Access;
	void Awake() {
		if (Access != null) Destroy(gameObject);
		Access = this;
	}
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HumanDialog : MonoBehaviour {
	public float DialogDecayTime;
	public EmotionData[] Emotions;

	public Text dialogText;
	public Image dialogPanel;

	float decayTimer;

	void Start() {
		dialogPanel.gameObject.SetActive(false);
	}

	void Update() {
		if (decayTimer > 0f) {
			decayTimer -= Time.deltaTime;
			if (decayTimer < 0f) DecayDialog();
		}
	}

	public void TriggerDialog(HumanEmotion em) {
		var data = GetEmotion(em);
		dialogText.text = data.text;
		dialogPanel.color = data.color;
		dialogPanel.gameObject.SetActive(true);
		MusicPlayer.Instance.DialogPop();
		decayTimer = DialogDecayTime;
	}

	void DecayDialog() {
		dialogPanel.gameObject.SetActive(false);
	}

	EmotionData GetEmotion(HumanEmotion em) {
		string val = System.Enum.GetName(typeof(HumanEmotion), em);
		return Emotions.Single(e => e.name == val);
	}

	[System.Serializable]
	public struct EmotionData {
		public string name;
		public string text;
		public Color color;
	}
}

public enum HumanEmotion {
	Happy,
	Sad,
	Angry,
	Command,
	Question
}
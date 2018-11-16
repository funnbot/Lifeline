using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HumanDialog : MonoBehaviour {
	public float DialogDecayTime;
	public EmotionData[] Emotions;

	public Text dialogText;
	public Image dialogPanel;

	void Start() {
		dialogPanel.gameObject.SetActive(false);
	}

	public void TriggerDialog(HumanEmotion em) {
		var data = GetEmotion(em);
		dialogText.text = data.text;
		dialogPanel.color = data.color;
		dialogPanel.gameObject.SetActive(true);
		StartCoroutine(DecayDialog());
	}

	IEnumerator DecayDialog() {
		yield return new WaitForSeconds(DialogDecayTime);
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
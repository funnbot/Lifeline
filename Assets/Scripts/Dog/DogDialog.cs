using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DogDialog : MonoBehaviour {
	public float DialogDecayTime;
	public EmotionData[] Emotions;

	public Text dialogText;
	public Image dialogPanel;

	void Start() {
		dialogPanel.gameObject.SetActive(false);
	}

	void TriggerDialog(DogEmotion em) {
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

	EmotionData GetEmotion(DogEmotion em) {
		string val = System.Enum.GetName(typeof(DogEmotion), em);
		return Emotions.Single(e => e.name == val);
	}

	[System.Serializable]
	public struct EmotionData {
		public string name;
		public string text;
		public Color color;
	}
}

public enum DogEmotion {
	Bark
}
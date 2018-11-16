using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Animators : MonoBehaviour {

	public Animator[] animators;

	public static Animator Get(string name) {
		return Instance.animators.Single(a => a.gameObject.name == name);
	}

	public static  IEnumerator TriggerAnim(string name, string trigger) {
		var anim = Get(name);
		Debug.Log(anim.ToString());
		anim.SetTrigger(trigger);
		var state = anim.GetCurrentAnimatorStateInfo(0);
		var length = state.length + state.normalizedTime;
		yield return new WaitForSeconds(length);
	}

	public static Animators Instance;
	void Awake() {
		if (Instance != null) Destroy(Instance);
		Instance = this;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {
	private HumanMovementController movement;
	public static HumanMovementController Movement {
		get { return Instance.movement; }
	}

	private HumanDialog dialog;
	public static HumanDialog Dialog {
		get { return Instance.dialog; }
	}

	private HumanAction action;
	public static HumanAction Action {
		get { return Instance.action; }
	}

	private HumanTracker tracker;
	public static HumanTracker Tracker {
		get { return Instance.tracker; }
	}

	private Animator animator;
	public static Animator Animator {
		get { return Instance.animator; }
	}

	public AudioSource audioSource;
	public static AudioSource Audio {
		get { return Instance.audioSource; }
	}

	void Start() {
		movement = GetComponent<HumanMovementController>();
		dialog = GetComponent<HumanDialog>();
		action = GetComponent<HumanAction>();
		tracker = GetComponent<HumanTracker>();
		animator = GetComponentInChildren<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	public static Human Instance;
	void Awake() {
		if (Instance != null) Destroy(gameObject);
		Instance = this;
	}
}
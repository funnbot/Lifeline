using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {
	private DogController controller;
	public static DogController Controller {
		get { return Instance.controller; }
	}

	private DogDialog dialog;
	public static DogDialog Dialog {
		get { return Instance.dialog; }
	}

	private DogInteractor interactor;
	public static DogInteractor Interactor {
		get { return Instance.interactor; }
	}

	private DogMovementController movement;
	public static DogMovementController Movement {
		get { return Instance.movement; }
	}

	private Animator animator;
	public static Animator Animator {
		get { return Instance.animator; }
	}

	public static Transform Transform {
		get { return Instance.transform; }
	}

	void Start() {
		controller = GetComponent<DogController>();
		dialog = GetComponent<DogDialog>();
		interactor = GetComponent<DogInteractor>();
		movement = GetComponent<DogMovementController>();
		animator = GetComponentInChildren<Animator>();
	}

	public static Dog Instance;
	void Awake() {
		if (Instance != null) Destroy(gameObject);
		Instance = this;
	}
}
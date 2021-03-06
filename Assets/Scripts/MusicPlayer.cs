﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
	public bool InMenu;
	public float FadeSpeed;

	public AudioClip DialogPopClip;
	public AudioClip DogBarkClip;

	private AudioSource Player;

	void Start() {
		Player = GetComponent<AudioSource>();
		Player.volume = 0;

		if (InMenu) {
			Player.Play();
		}

		StartCoroutine(FadeIn());
	}

	public void DogBark() {
		Dog.Audio.PlayOneShot(DogBarkClip);
	}

	public void DialogPop() {
		Human.Audio.PlayOneShot(DialogPopClip);
	}

	public IEnumerator FadeOut() {
		float lerp = 1f;
		while (lerp >= 0f) {
			lerp -= Time.deltaTime * FadeSpeed;
			Player.volume = lerp;
			yield return null;
		}
	}

	public IEnumerator FadeIn() {
		float lerp = 0f;
		while (lerp <= 1f) {
			lerp += Time.deltaTime * FadeSpeed;
			Player.volume = lerp;
			yield return null;
		}
	}

	public static MusicPlayer Instance;
	void Awake() {
		if (Instance != null) Destroy(gameObject);
		Instance = this;
	}
}
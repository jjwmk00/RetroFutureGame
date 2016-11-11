﻿using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	GameObject[] gameoverObjects;
	GameObject player;
	InputManager input;

	void Start() {
		gameoverObjects = GameObject.FindGameObjectsWithTag("ShowOnDeath");
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
		player = GameObject.Find ("Char");
		HideGameover ();
	}

	void Update () {
	
	}

	public void ShowGameover() {
		foreach (GameObject g in gameoverObjects) {
			g.SetActive(true);
		}
		Time.timeScale = 0;
	}

	public void HideGameover() {
		foreach (GameObject g in gameoverObjects) {
			g.SetActive(false);
		}
		Time.timeScale = 1;
	}

	public void Respawn() {
		player.GetComponent<CharHealth>().MaximizeHealth();
		player.transform.position = player.GetComponent<Checkpoint> ().activeCheckpoint.position;
		HideGameover ();
	}
}

﻿using UnityEngine;
using System.Collections;

// The object we attach this script to, has to have a Collider on it
[RequireComponent(typeof(Collider))]
public class GazeInteraction : MonoBehaviour, IGvrGazeResponder {

	// Use this for initialization
	void Start () {
		SetGazedAt(false);
	}

	// LateUpdate is called every frame
	// and is called after all Update functions have been called
	void LateUpdate () {
		GvrViewer.Instance.UpdateState();
		if (GvrViewer.Instance.BackButtonPressed) {
			Application.Quit();
		}
	}

	public void SetGazedAt(bool gazedAt) {
		this.GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
	}

	public void GazeTriggerAction() {
		teleportRandomly();
	}

	void teleportRandomly() {
    Vector3 direction = Random.onUnitSphere;
    direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
    float distance = 2 * Random.value + 1.5f;
    transform.localPosition = direction * distance;
  }

	// This is where we implement the IGvrGazeResponder class (interface).
	#region IGvrGazeResponder implementation

    /// Called when the user is looking on a GameObject with this script,
    /// as long as it is set to an appropriate layer (see GvrGaze).
    public void OnGazeEnter() {
		SetGazedAt(true);
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit() {
		SetGazedAt(false);
    }

    /// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
    public void OnGazeTrigger() {
		GazeTriggerAction();
    }

    #endregion
}

using UnityEngine;
using System.Collections;

public class Test : FageEventDispatcher {
	private	FageAudioSourceControl control;

	void OnEnable() {
	}

	void OnDisable() {
	}

	void OnGUI() {
		if (GUI.Button (new Rect (0,0,Screen.width/2, Screen.height/2), "REQUEST1")) {
			if (control!=null) {
				control.onLoop -= OnAudioLoop;
				control.onStatus -= OnAudioStatus;
				control = null;
			}

			FageAudioManager.Instance.Play("background", "clips/POL-lunar-love-short", ref control, true);
			control.onLoop += OnAudioLoop;
			control.onStatus += OnAudioStatus;
		}
		if (GUI.Button (new Rect (Screen.width/2,0,Screen.width/2, Screen.height/2), "REQUEST2")) {
			FageAudioManager.Instance.Play("effect", "clips/NFF-coin-03");

			FageUIManager.Instance.Change(FageUIRoot.Instance.FindUISet("testui"));
			gameObject.SetActive(false);
		}
	}

	private	void OnAudioLoop(FageAudioSourceControl control) {
		Debug.Log("LOOP");
	}

	private	void OnAudioStatus(FageAudioSourceControl control) {
		Debug.Log("STATUS:"+control.audioStatus.ToString());
	}
}
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

			FageAudioRequest request = new FageAudioRequest(name, FageAudioCommand.PLAY, "background", "clips/POL-lunar-love-short", true, true);
			FageEvent fevent = new FageEvent(FageEvent.AUDIO_REQUEST, request);
			AddEventListener(FageEvent.AUDIO_RESPONSE, OnResponse);
			DispatchEvent(fevent);
		}
		if (GUI.Button (new Rect (Screen.width/2,0,Screen.width/2, Screen.height/2), "REQUEST2")) {
			FageAudioRequest request = new FageAudioRequest(name, FageAudioCommand.PLAY, "effect", "clips/NFF-coin-03");
			FageEvent fevent = new FageEvent(FageEvent.AUDIO_REQUEST, request);
			DispatchEvent(fevent);
		}
	}


	private	void OnResponse(FageEvent fevent) {
		RemoveEventListener(FageEvent.AUDIO_RESPONSE, OnResponse);
		FageAudioResponse response = fevent.data as FageAudioResponse;
		if (response.control!=null) {
			control = response.control;
			control.onLoop += OnAudioLoop;
			control.onStatus += OnAudioStatus;
		}
	}

	private	void OnAudioLoop(FageAudioSourceControl control) {
		Debug.Log("LOOP");
	}

	private	void OnAudioStatus(FageAudioSourceControl control) {
		Debug.Log("STATUS:"+control.audioStatus.ToString());
	}
}
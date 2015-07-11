using UnityEngine;
using System.Collections;

public class Test : FageEventDispatcher {

	void OnEnable() {
	}

	void OnDisable() {
	}

	void OnGUI() {
		if (GUI.Button (new Rect (0,0,Screen.width, Screen.height/2), "REQUEST1")) {
			FageAudioRequest request = new FageAudioRequest(name, FageAudioCommand.PLAY, "background", "clips/POL-lunar-love-short", true);
			FageEvent fevent = new FageEvent(FageEvent.AUDIO_REQUEST, request);
			DispatchEvent(fevent);
		}
		if (GUI.Button (new Rect (0,Screen.height/2,Screen.width, Screen.height/2), "REQUEST2")) {
			AddEventListener(FageEvent.AUDIO_RESPONSE, OnResponse);
			FageAudioRequest request = new FageAudioRequest(name, FageAudioCommand.STATUS, "background", "clips/POL-lunar-love-short");
			FageEvent fevent = new FageEvent(FageEvent.AUDIO_REQUEST, request);
			DispatchEvent(fevent);
		}
	}

	private void OnResponse(FageEvent fevent) {
		RemoveEventListener(FageEvent.AUDIO_RESPONSE, OnResponse);
		FageAudioResponse response = fevent.data as FageAudioResponse;
		Debug.Log(response.status);
	}
}
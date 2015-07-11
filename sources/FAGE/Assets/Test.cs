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
			FageAudioRequest request = new FageAudioRequest(name, FageAudioCommand.PLAY, "effect", "clips/NFF-coin-03");
			FageEvent fevent = new FageEvent(FageEvent.AUDIO_REQUEST, request);
			DispatchEvent(fevent);
		}
	}

}
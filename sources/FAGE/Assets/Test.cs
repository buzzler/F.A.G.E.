using UnityEngine;
using System.Collections;

public class Test : FageEventDispatcher {

	void OnEnable() {
	}

	void OnDisable() {
	}

	void OnGUI() {
		if (GUI.Button (new Rect (0,0,Screen.width, Screen.height/2), "REQUEST")) {
			AddEventListener(FageEvent.SENSOR_RESPONSE, OnResponse);
			DispatchEvent(new FageEvent(FageEvent.SENSOR_REQUEST, new FageRequest(name, "http://google.com")));
		}
	}

	private void OnResponse(FageEvent fevent) {
		FageResponse response = fevent.data as FageResponse;
		Debug.Log (response.www.text);
		RemoveEventListener(FageEvent.SENSOR_RESPONSE, OnResponse);
	}
}
using UnityEngine;
using System.Collections;

public class Test : FageEventDispatcher {

	void OnEnable() {
	}

	void OnDisable() {
	}

	void OnGUI() {
		if (GUI.Button (new Rect (0,0,Screen.width, Screen.height/2), "REQUEST")) {
			AddEventListener(FageEvent.FILE_RESPONSE, OnResponse);
			byte[] data = new byte[100];
			for (int i = 0 ; i < 100 ; i++) {
				data[i] = (byte)i;
			}
			DispatchEvent(new FageEvent(FageEvent.FILE_REQUEST, new FageFileRequest(name, Application.persistentDataPath + "/ghghgh.gh", FageFileMode.SAVE_ASYNC, data) ));
		}
	}

	private void OnResponse(FageEvent fevent) {
		Debug.Log ("SAVED!");
		RemoveEventListener(FageEvent.FILE_RESPONSE, OnResponse);
	}
}
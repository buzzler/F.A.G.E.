using UnityEngine;
using System.Collections;

public class Test : FageEventDispatcher {

	void Awake() {
		FageEventDispatcher.AddEventListener (FageEvent.SENSOR_ONLINE, new FageEventHandler (OnLine));
		FageEventDispatcher.AddEventListener (FageEvent.SENSOR_OFFLINE, new FageEventHandler (OffLine));
	}

	public	void OnLine(FageEvent fevent) {
		Debug.Log ("ONLINE!");
	}

	public	void OffLine(FageEvent fevent) {
		Debug.Log ("OFFLINE!");
	}
}

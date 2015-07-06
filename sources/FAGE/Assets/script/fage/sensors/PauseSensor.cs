using UnityEngine;

public class PauseSensor : FageEventDispatcher {
	void OnApplicationPause(bool status) {
		FageEvent fevent;
		if (status) {
			fevent = new FageEvent (FageEvent.SENSOR_PAUSE);
		} else {
			fevent = new FageEvent (FageEvent.SENSOR_RESUME);
		}
		DispatchEvent (fevent);
	}

	void OnApplicationQuit() {
		DispatchEvent (new FageEvent (FageEvent.SENSOR_QUIT));
	}
}

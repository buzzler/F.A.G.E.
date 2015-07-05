using UnityEngine;
using Fage.Events;
using Fage.FSM;

public class ErrorMonitor : StateMachine {

	// Use this for initialization
	void Awake () {
		AddEventListener (FageEvent.ERROR, new FageEventHandler(OnError));
	}
	
	void OnError(FageEvent fevent) {
		Debug.Log (fevent.data);
	}
}

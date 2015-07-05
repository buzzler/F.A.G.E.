using UnityEngine;
using System.Collections;
using Fage;
using Fage.Events;

public class Tester : EventDispatcher {

	bool dispatched;

	// Use this for initialization
	void Start () {
		AddEventListener ("eventType", new FageEventHandler (OnHandler));
		dispatched = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!dispatched) {
			DispatchEvent (new FageEvent ("eventType", "Hello World"));
			DispatchEvent (new FageEvent ("eventType2", "Hello World"));
			DispatchEvent (new FageEvent ("eventType", 23456789));
			dispatched = true;
		}
	}

	void OnHandler(FageEvent fevent) {
		Debug.Log (fevent.data);
	}
}

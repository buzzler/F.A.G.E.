using UnityEngine;
using System.Collections;
using Fage;
using Fage.Events;
using Fage.FSM;

public	class TestState : State {
	public	TestState() {
	}

	public new void AfterSwitch (StateMachine stateMachine, string beforeId)
	{
	}

	public new void Excute (StateMachine stateMachine)
	{
	}

	public new void BeforeSwitch (StateMachine stateMachine, string afterId)
	{
	}
}

public	class Tester : StateMachine {

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
			DispatchEvent (new FageEvent (FageEvent.ERROR, 17223));
			dispatched = true;
		}
	}

	void OnHandler(FageEvent fevent) {
		Debug.Log (fevent.data);
	}
}

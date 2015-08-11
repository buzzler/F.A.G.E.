using UnityEngine;
using System.Collections;

public class ConnectionStateMachine : FageStateMachine {
	private	static ConnectionStateMachine _instance;
	public	static ConnectionStateMachine Instance { get { return _instance; } }

	void Awake() {
		_instance = this;
	}

	public	bool IsOnline() {
		if (this.current is ConnectionPing) {
			return (current as ConnectionPing).IsOnline ();
		} else {
			return false;
		}
	}
}

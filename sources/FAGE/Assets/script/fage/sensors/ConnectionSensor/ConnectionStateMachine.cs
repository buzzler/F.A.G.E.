using UnityEngine;
using System.Collections;

public class ConnectionStateMachine : FageStateMachine {
	public	bool IsOnline() {
		if (this.current is ConnectionPing) {
			return (current as ConnectionPing).IsOnline ();
		} else {
			return false;
		}
	}
}

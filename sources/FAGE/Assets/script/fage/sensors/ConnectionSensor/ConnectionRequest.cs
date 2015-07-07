using UnityEngine;
using System.Collections;

public class ConnectionRequest : FageState {
	private	FageRequest _current;

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
	}

	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
	}

	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		ConnectionSensor cs = stateMachine as ConnectionSensor;

		if (Application.internetReachability == NetworkReachability.NotReachable) {
			FageEventDispatcher.DispatchEvent (new FageEvent (FageEvent.SENSOR_OFFLINE));
			cs.ReserveState ("ConnectionStart");
			return;
		}

		if (_current == null) {
			_current = cs.ShiftRequest ();
			if (_current == null) {
				cs.ReserveState ("ConnectionPing");
				return;
			} else {
				cs.StartCoroutine (CheckWWW (cs, _current));
			}
		}
	}

	IEnumerator CheckWWW(ConnectionSensor cs, FageRequest request) {
		WWW w;
		if (request.form != null) {
			w = new WWW (request.url, request.form);
		} else {
			w = new WWW (request.url);
		}

		yield return w;

		if (!string.IsNullOrEmpty (w.error)) {
			cs.UnshiftRequest (request);
		} else {
			FageEventDispatcher.DispatchEvent (new FageEvent (FageEvent.SENSOR_RESPONSE, new FageResponse (request.sender, w)));
			_current = null;
		}
	}
}

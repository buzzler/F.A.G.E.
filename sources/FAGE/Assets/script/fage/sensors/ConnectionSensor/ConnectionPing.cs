using UnityEngine;

public class ConnectionPing : FageState {
	private const float _TIMEOUT = 10;
	private	Ping	_ping;
	private float	_start_time;
	private	bool	_online;

	private	void InitPing() {
		if (_ping != null) {
			_ping.DestroyPing ();
		}
		_ping = null;
		_start_time = 0;
	}

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		InitPing ();
	}

	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
	}

	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			if (_online) {
				FageEventDispatcher.DispatchEvent (new FageEvent (FageEvent.SENSOR_OFFLINE));
			}
			stateMachine.ReserveState ("ConnectionStart");
			return;
		}

		if (_ping != null) {
			bool last = _online;
			if (_ping.isDone) {
				InitPing();
				_online = true;
			} else if ((Time.realtimeSinceStartup - _start_time) > _TIMEOUT) {
				InitPing();
				_online = false;
			}

			if (last != _online) {
				if (_online) {
					FageEventDispatcher.DispatchEvent (new FageEvent (FageEvent.SENSOR_ONLINE));
				} else {
					FageEventDispatcher.DispatchEvent (new FageEvent (FageEvent.SENSOR_OFFLINE));
				}
			}
		}

		if (_ping == null) {
			ConnectionSensor cs = stateMachine as ConnectionSensor;
			if (cs.GetMessageCount () > 0) {
				cs.ReserveState ("ConnectionRequest");
			} else {
				_ping = new Ping ("8.8.8.8");
				_start_time = Time.realtimeSinceStartup;
			}
		}
	}
}

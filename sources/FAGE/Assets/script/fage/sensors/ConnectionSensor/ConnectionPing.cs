using UnityEngine;

public class ConnectionPing : FageState {
//	private	const string _URL_1		= "http://www.google.com/blank.html";
//	private const string _URL_2		= "http://www.msftncsi.com/ncsi.txt";
	private const float _TIMEOUT	= 10;
	private	const float _ITERATE	= 20;

	private	Ping	_ping;
	private float	_start_time;
	private float	_last_time;
	private	bool	_online;

	private	void InitPing() {
		if (_ping != null) {
			_ping.DestroyPing ();
		}
		_ping = null;
		_start_time = 0;
		_last_time = 0;
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
				_last_time = Time.realtimeSinceStartup;
				_online = true;
			} else if ((Time.realtimeSinceStartup - _start_time) > _TIMEOUT) {
				InitPing();
				_last_time = Time.realtimeSinceStartup;
				_online = false;
			} else {
				return;
			}

			if (last != _online) {
				if (_online) {
					FageEventDispatcher.DispatchEvent (new FageEvent (FageEvent.SENSOR_ONLINE));
				} else {
					FageEventDispatcher.DispatchEvent (new FageEvent (FageEvent.SENSOR_OFFLINE));
				}
			} else {
				FageEventDispatcher.DispatchEvent (new FageEvent (FageEvent.SENSOR_PING));
			}
		}

		if (_ping == null) {
			ConnectionSensor cs = stateMachine as ConnectionSensor;
			if (cs.GetMessageCount () > 0) {
				cs.ReserveState ("ConnectionRequest");
			} else if ((Time.realtimeSinceStartup - _last_time) >= _ITERATE) {
				_ping = new Ping ("8.8.8.8");
				_start_time = Time.realtimeSinceStartup;
			}
		}
	}
}

using UnityEngine;
using System;
using System.Collections;

public class SystemCheckComponent : MonoBehaviour {
	public	static bool network { get { return _network; } }
	public	static bool data	{ get { return _data; } }
	public	static bool wifi	{ get { return _wifi; } }
	public	static int	pingTime{ get { return _pingTime; } }
	private	static bool _network;
	private	static bool _data;
	private	static bool	_wifi;
	private	static int	_pingTime;

	public	float	duration;
	public	float	pingTimeout;
	private	float	pingStart;
	private	Ping	ping;
	private	bool	_last;
	
	void Start() {
		_network = false;
		_data = false;
		_wifi = false;
		_pingTime = 0;
		_last = false;

		if (duration > 0) {
			InvokeRepeating("Check", 0, duration);
		}
	}

	void OnApplicationPause(bool pauseStatus) {
		if (pauseStatus) {
			CancelInvoke();
		} else {
			InvokeRepeating("Check", 0, duration);
		}
	}

	void Update() {
		if (ping != null) {
			if (ping.isDone) {
				OnSuccess();
			} else if (Time.time-pingStart < pingTimeout) {
				// wait..
			} else {
				// timeout!!
				_pingTime = ping.time;
				ping.DestroyPing();
				ping = null;
				pingStart = 0;
				OnFail("time out");
			}
		}

		if (_last != _network) {
			_last = _network;
			// do something
		}
	}
	
	private	void Check() {
		CheckPing ();
	}

	private	void OnFail(string error = "") {
		_network = false;
	}

	private	void OnSuccess() {
		_network = true;
	}

	private	bool CheckNetwork() {
		_data = false;
		_wifi = false;

		switch (Application.internetReachability) {
		case NetworkReachability.ReachableViaCarrierDataNetwork:
			_data = true;
			break;
		case NetworkReachability.ReachableViaLocalAreaNetwork:
			_wifi = true;
			break;
		default:
			break;
		}
		return _data || _wifi;
	}

	private	void CheckPing() {
		if (CheckNetwork ()) {
			ping = new Ping ("8.8.8.8");
			pingStart = Time.time;
		} else {
			OnFail();
		}
	}
}

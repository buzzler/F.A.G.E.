using UnityEngine;
using System.Collections;

public class FageWebLoader : FageEventDispatcher {
	private	const int			_MAX_QUEUE = 100;
	private	FageWebRequest[]	_requests;
	private int					_index_push;
	private int					_index_pop;
	private	WWW					_www;
	private	FageWebRequest		_current;
	private	bool				_excute;

	void Awake() {
		_requests	= new FageWebRequest[_MAX_QUEUE];
		_index_push	= 0;
		_index_pop	= 0;
	}

	void OnEnable() {
		AddEventListener (FageEvent.WEB_REQUEST,	OnRequestEvent);
		AddEventListener (FageEvent.SENSOR_ONLINE,	OnOnline);
		AddEventListener (FageEvent.SENSOR_OFFLINE,	OnOffline);
	}

	void OnDisable() {
		RemoveEventListener (FageEvent.WEB_REQUEST, new FageEventHandler (OnRequestEvent));
		RemoveEventListener (FageEvent.SENSOR_ONLINE,	OnOnline);
		RemoveEventListener (FageEvent.SENSOR_OFFLINE,	OnOffline);
	}

	void Update() {
		if (_www != null) {
			if (_www.isDone) {
				DispatchEvent (new FageEvent (FageEvent.WEB_RESPONSE, new FageWebResponse (_current.sender, _www)));
				_www = null;
				_current = null;
			}
		} else if ((GetRequestCount () > 0) && _excute) {
			_current = ShiftRequest ();
			if (_current.form != null) {
				_www = new WWW (_current.url, _current.form);
			} else {
				_www = new WWW (_current.url);
			}
		}
	}

	private	void OnOnline(FageEvent fevent) {
		_excute = true;
	}

	private void OnOffline(FageEvent fevent) {
		_excute = false;
		if ((_www != null) && (_current != null)) {
			UnshiftRequest (_current);
			_www = null;
			_current = null;
		}
	}

	private	void OnRequestEvent(FageEvent fevent) {
		FageWebRequest request = fevent.data as FageWebRequest;
		if (request == null) {
			throw new UnityException ();
		}
		
		int temp = (_index_push + 1) % _MAX_QUEUE;
		if (temp == _index_pop) {
			throw new UnityException ();
		}
		
		_requests [_index_push] = request;
		_index_push = temp;
	}
	
	private	int GetRequestCount() {
		if (_index_push >= _index_pop) {
			return _index_push - _index_pop;
		} else {
			return (_index_push + _MAX_QUEUE) - _index_pop;
		}
	}
	
	private	void UnshiftRequest(FageWebRequest request) {
		if (request == null) {
			throw new UnityException ();
		}
		
		int temp = (_index_pop - 1 + _MAX_QUEUE) % _MAX_QUEUE;
		if (temp == _index_push) {
			throw new UnityException ();
		}
		
		_requests [_index_pop] = request;
		_index_pop = temp;
	}
	
	private	FageWebRequest ShiftRequest() {
		if (_index_pop == _index_push) {
			return null;
		}
		
		FageWebRequest result = _requests [_index_pop];
		_requests [_index_pop] = null;
		_index_pop = (_index_pop + 1) % _MAX_QUEUE;
		return result;
	}
}

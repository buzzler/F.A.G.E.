using UnityEngine;

public class ConnectionSensor : FageStateMachine {
	private	const int		_MAX_QUEUE = 100;
	private	FageRequest[]	_requests;
	private int				_index_push;
	private int				_index_pop;

	void Awake() {
		_requests	= new FageRequest[_MAX_QUEUE];
		_index_push	= 0;
		_index_pop	= 0;
		AddEventListener (FageEvent.SENSOR_REQUEST, new FageEventHandler (OnRequestEvent));
		AddEventListener (FageEvent.SENSOR_QUIT, new FageEventHandler (OnQuit));
	}

	private void OnQuit(FageEvent fevent) {
		RemoveEventListener (FageEvent.SENSOR_REQUEST, new FageEventHandler (OnRequestEvent));
		RemoveEventListener (FageEvent.SENSOR_QUIT, new FageEventHandler (OnQuit));
	}

	private	void OnRequestEvent(FageEvent fevent) {
		FageRequest request = fevent.data as FageRequest;
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

	public	int GetRequestCount() {
		if (_index_push >= _index_pop) {
			return _index_push - _index_pop;
		} else {
			return (_index_push + _MAX_QUEUE) - _index_pop;
		}
	}

	public	void UnshiftRequest(FageRequest request) {
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

	public	FageRequest ShiftRequest() {
		if (_index_pop == _index_push) {
			return null;
		}

		FageRequest result = _requests [_index_pop];
		_requests [_index_pop] = null;
		_index_pop = (_index_pop + 1) % _MAX_QUEUE;
		return result;
	}
}

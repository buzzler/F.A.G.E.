using UnityEngine;
using System.Collections;

public class FageAudioManager : FageEventDispatcher {
	public	FageAudioNode[]		nodes;
	private	const int			_MAX_QUEUE = 20;
	private	FageAudioRequest[]	_requests;
	private	int					_index;
	private	Hashtable			_hashtable;
	private	GameObject			_listener;

	void Awake() {
		_requests = new FageAudioRequest[_MAX_QUEUE];
		_index = 0;
		_hashtable = new Hashtable ();
		_listener = new GameObject("AudioChannels", typeof(AudioListener));
		_listener.transform.SetParent (transform);
		foreach (FageAudioNode node in nodes) {
			node.Align ();
			_hashtable.Add(node, new FageAudioPooler(node, _listener)); 
		}
	}

	void OnEnable() {
		AddEventListener (FageEvent.AUDIO_REQUEST, OnRequest);
	}

	void OnDisable() {
		RemoveEventListener (FageEvent.AUDIO_REQUEST, OnRequest);
	}

	void Update() {
		while (_index > 0) {
			FageAudioRequest request = _requests[_index];
			if (request!=null) {
				FageAudioNode node = FageAudioNode.Find(request.node);
				if (node!=null) {
					switch (request.command) {
					case FageAudioCommand.PAUSE:	OnPause(request, node);	break;
					case FageAudioCommand.PLAY:		OnPlay(request, node);	break;
					case FageAudioCommand.RESUME:	OnResume(request, node);break;
					case FageAudioCommand.STOP:		OnStop(request, node);	break;
					case FageAudioCommand.VOLUMN:	OnVolumn(request, node);break;
					}
				}
			}
			_index--;
		}
	}

	private	void OnRequest(FageEvent fevent) {
		if (_index >= _MAX_QUEUE) {
			throw new UnityException ();
		}

		_requests [_index] = fevent.data as FageAudioRequest;
		_index++;
	}

	private	void OnPause(FageAudioRequest request, FageAudioNode node) {
	}

	private	void OnPlay(FageAudioRequest request, FageAudioNode node) {
	}

	private	void OnResume(FageAudioRequest request, FageAudioNode node) {
	}

	private	void OnStop(FageAudioRequest request, FageAudioNode node) {
	}

	private	void OnVolumn(FageAudioRequest request, FageAudioNode node) {
	}
}


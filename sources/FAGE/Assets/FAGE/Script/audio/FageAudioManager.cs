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
			_index--;
			FageAudioRequest request = _requests[_index];
			if (request==null) {
				continue;
			}

			FageAudioNode node = FageAudioNode.Find(request.node);
			FageAudioPooler pooler = _hashtable[node] as FageAudioPooler;
			if (node!=null) {
				switch (request.command) {
				case FageAudioCommand.PLAY:		OnPlay(request, node, pooler);	break;
				case FageAudioCommand.VOLUME:	OnVolume(request, node,pooler);	break;
				}
			}
		}
	}

	private	void OnRequest(FageEvent fevent) {
		if (_index >= _MAX_QUEUE) {
			throw new UnityException ();
		}

		_requests [_index] = fevent.data as FageAudioRequest;
		_index++;
	}

	private	void OnPlay(FageAudioRequest request, FageAudioNode node, FageAudioPooler pooler) {
		FageAudioSourceControl control = pooler.GetFreeAudioSourceControl();
		control.Play(CachedResource.Load<AudioClip>(request.source), request.loop, node.GetVolume(), request.control);

		if (request.control) {
			DispatchEvent(new FageEvent(FageEvent.AUDIO_RESPONSE, new FageAudioResponse(request.sender, control.GetComponent<FageAudioSourceControl>())));
		}
	}

	private	void OnVolume(FageAudioRequest request, FageAudioNode node, FageAudioPooler pooler) {
		node.volumn = Mathf.Clamp(request.volumn, 0f, 1f);
		float v = node.GetVolume();
		FageAudioSourceControl[] controls = pooler.GetAudioSourceControls();
		foreach (FageAudioSourceControl control in controls) {
			control.volume = v;
		}
	}
}


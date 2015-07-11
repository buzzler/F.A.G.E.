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
			if (request!=null) {
				FageAudioNode node = FageAudioNode.Find(request.node);
				FageAudioPooler pooler = _hashtable[node] as FageAudioPooler;
				if (node!=null) {
					switch (request.command) {
					case FageAudioCommand.PAUSE:	OnPause(request, node, pooler);	break;
					case FageAudioCommand.PLAY:		OnPlay(request, node, pooler);	break;
					case FageAudioCommand.RESUME:	OnResume(request, node, pooler);break;
					case FageAudioCommand.STOP:		OnStop(request, node, pooler);	break;
					case FageAudioCommand.VOLUMN:	OnVolumn(request, node,pooler);	break;
					case FageAudioCommand.STATUS:	OnStatus(request, node, pooler);break;
					}
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

	private	void OnPause(FageAudioRequest request, FageAudioNode node, FageAudioPooler pooler) {
		AudioSource[] asources = pooler.FindAudioSources(CachedResource.Load<AudioClip>(request.source));
		foreach (AudioSource asource in asources) {
			asource.Pause();
		}
	}

	private	void OnPlay(FageAudioRequest request, FageAudioNode node, FageAudioPooler pooler) {
		AudioSource asource = pooler.GetFreeAudioSource();
		asource.clip = CachedResource.Load<AudioClip>(request.source);
		asource.loop = request.loop;
		asource.volume = node.GetVolumn();
		asource.Play();
	}

	private	void OnResume(FageAudioRequest request, FageAudioNode node, FageAudioPooler pooler) {
		AudioSource[] asources = pooler.FindAudioSources(CachedResource.Load<AudioClip>(request.source));
		foreach (AudioSource asource in asources) {
			asource.volume = node.GetVolumn();
			asource.UnPause();
		}
	}

	private	void OnStop(FageAudioRequest request, FageAudioNode node, FageAudioPooler pooler) {
		AudioSource[] asources = pooler.FindAudioSources(CachedResource.Load<AudioClip>(request.source));
		foreach (AudioSource asource in asources) {
			asource.Stop();
		}
	}

	private	void OnVolumn(FageAudioRequest request, FageAudioNode node, FageAudioPooler pooler) {
		node.volumn = Mathf.Clamp(request.volumn, 0f, 1f);
		float v = node.GetVolumn();
		AudioSource[] asources = pooler.GetAudioSources();
		foreach (AudioSource asource in asources) {
			asource.volume = v;
		}
	}

	private	void OnStatus(FageAudioRequest request, FageAudioNode node, FageAudioPooler pooler) {
		FageAudioStatus status = FageAudioStatus.NONE;
		AudioSource[] asources = pooler.FindAudioSources(CachedResource.Load<AudioClip>(request.source));
		if (asources.Length>0) {
			AudioSource asource = asources[0];
			if (asource.isPlaying) {
				status = FageAudioStatus.PLAYING;
			} else if (asource.time > 0) {
				status = FageAudioStatus.PAUSED;
			} else {
				status = FageAudioStatus.STOPPED;
			}
		}
		DispatchEvent(new FageEvent(FageEvent.AUDIO_RESPONSE, new FageAudioResponse(request.sender, status)));
	}
}


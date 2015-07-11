using UnityEngine;
using System.Collections.Generic;

public class FageAudioPooler {
	private	FageAudioNode 	_node;
	private	AudioSource[]	_sources;
	private	int				_index;

	public	FageAudioPooler(FageAudioNode node, GameObject listener) {
		_node = node;
		_sources = new AudioSource[_node.channels];
		_index = 0;

		for (int i = 1; i <= _node.channels; i++) {
			GameObject child = new GameObject (node.name + " " + i.ToString (), typeof(AudioSource));
			child.transform.SetParent (listener.transform);
			_sources [i - 1] = child.GetComponent<AudioSource> ();
		}
	}

	public	AudioSource[] GetAudioSources() {
		return _sources;
	}

	public	AudioSource GetFreeAudioSource() {
		if (_node.channels == 0) {
			return null;
		}

		AudioSource src = _sources [_index];
		if (src.isPlaying) {
			src.Stop ();
		}
		_index = (_index + 1) % _node.channels;
		return src;
	}

	public	AudioSource[] FindAudioSources(AudioClip clip) {
		List<AudioSource> buffer = new List<AudioSource>();
		foreach (AudioSource asource in _sources) {
			if (asource.clip==clip) {
				buffer.Add(asource);
			}
		}
		return buffer.ToArray();
	}
}

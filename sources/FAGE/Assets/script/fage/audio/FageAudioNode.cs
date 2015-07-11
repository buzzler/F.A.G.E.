using UnityEngine;
using System.Collections;

[System.Serializable]
public	class FageAudioNode {
	private	static Hashtable _hashtable = new Hashtable();

	public	string	name;
	public	string	linkTo;
	public	float	volumn;
	public	int		channels;

	public	static FageAudioNode Find(string name) {
		if (_hashtable.ContainsKey (name)) {
			return _hashtable [name] as FageAudioNode;
		} else {
			return null;
		}
	}

	public	void Align() {
		if (string.IsNullOrEmpty(name)) {
			throw new UnityException();
		}
		_hashtable [name] = this;
	}

	public	FageAudioNode GetParent() {
		if (!string.IsNullOrEmpty (linkTo)) {
			return Find (linkTo);
		}
		return null;
	}

	public	float GetVolumn() {
		float vol = volumn;
		FageAudioNode ancient = GetParent ();
		while (ancient!=null) {
			vol *= ancient.volumn;
			ancient = GetParent ();
		}
		return vol;
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("UIRoot")]
public	class FageUIRoot {
	private	static FageUIRoot _instance;
	public	static FageUIRoot Instance { get { return _instance; } }

	[XmlAttribute("url")]
	public	string				url;
	[XmlElement("UITransition")]
	public	FageUITransition[]	transitions;
	[XmlElement("UIDetail")]
	public	FageUIDetail[]		details;
	[XmlElement("UISet")]
	public	FageUISet[]			sets;
	[XmlElement("UIBundle")]
	public	FageUIBundle[]		bundles;

	private	Dictionary<string, FageUITransition>	_dicTransition;
	private	Dictionary<string, FageUIDetail>		_dicDetail;
	private	Dictionary<string, FageUISet>			_dicSet;
	private	Dictionary<string, FageUIBundle> 		_dictionary;

	public	FageUIRoot() {
		transitions = new FageUITransition[0];
		details = new FageUIDetail[0];
		sets = new FageUISet[0];
		bundles = new FageUIBundle[0];
		_dicTransition = new Dictionary<string, FageUITransition>();
		_dicDetail = new Dictionary<string, FageUIDetail>();
		_dicSet = new Dictionary<string, FageUISet>();
		_dictionary = new Dictionary<string, FageUIBundle>();
	}

	private	void Hashing() {
		_dicTransition.Clear();
		_dicDetail.Clear();
		_dicSet.Clear();
		_dictionary.Clear();

		foreach (FageUITransition trans in transitions) {
			_dicTransition.Add(trans.id, trans);
		}
		foreach (FageUIDetail detail in details) {
			_dicDetail.Add(detail.id, detail);
		}
		foreach (FageUISet set in sets) {
			_dicSet.Add(set.id, set);
		}
		foreach(FageUIBundle bundle in bundles) {
			_dictionary.Add(bundle.id, bundle);
		}
	}

	public	FageUITransition FindUITransition(string id) {
		if (_dicTransition.ContainsKey(id))
			return _dicTransition[id];
		else
			return null;
	}

	public	FageUIDetail FindUIDetail(string id) {
		if (_dicDetail.ContainsKey(id))
			return _dicDetail[id];
		else
			return null;
	}

	public	FageUISet FindUISet(string id) {
		if (_dicSet.ContainsKey(id))
			return _dicSet[id];
		else
			return null;
	}

	public	FageUIBundle FindUIBundle(string id) {
		if (_dictionary.ContainsKey(id)) {
			return _dictionary[id];
		} else {
			return null;
		}
	}

	public	static void LoadFromText(string text) {
		var serializer = new XmlSerializer(typeof(FageUIRoot));
		_instance = serializer.Deserialize(new StringReader(text)) as FageUIRoot;
		_instance.Hashing();
	}
}

public	class FageUIBundle {
	[XmlAttribute("id")]
	public	string	id;
	[XmlAttribute("url")]
	public	string	url;
	[XmlAttribute("version")]
	public	int		version;
	[XmlAttribute("dynamic")]
	public	bool	isDynamic;
	[XmlAttribute("cached")]
	public	bool	isCached;
	public	bool	isStatic { get { return !isDynamic; } }
}

public	class FageUISet {
	[XmlAttribute("id")]
	public	string id;
	[XmlAttribute("onPortrait")]
	public	string onPortrait;
	[XmlAttribute("onLandscape")]
	public	string onLandscape;

	public	FageUIDetail GetCurrentUIDetail () {
		switch (FageScreenManager.Instance.orientation) {
		case DeviceOrientation.Portrait:
		case DeviceOrientation.PortraitUpsideDown:
			return FageUIRoot.Instance.FindUIDetail(onPortrait);
		case DeviceOrientation.LandscapeLeft:
		case DeviceOrientation.LandscapeRight:
			return FageUIRoot.Instance.FindUIDetail(onLandscape);
		default:
			return FageUIRoot.Instance.FindUIDetail(onPortrait);
		}
	}
}

public	class FageUIDetail {
	[XmlAttribute("id")]
	public	string		id;
	[XmlAttribute("resource")]
	public	string		resource;
	[XmlAttribute("posX")]
	public	float		posX;
	[XmlAttribute("posY")]
	public	float		posY;
	[XmlAttribute("posZ")]
	public	float		posZ;
	[XmlAttribute("rotX")]
	public	float		rotX;
	[XmlAttribute("rotY")]
	public	float		rotY;
	[XmlAttribute("rotZ")]
	public	float		rotZ;
	[XmlAttribute("scaleX")]
	public	float		scaleX;
	[XmlAttribute("scaleY")]
	public	float		scaleY;
	[XmlAttribute("scaleZ")]
	public	float		scaleZ;

	[XmlAttribute("onInstantiate")]
	public	string		onInstantiate;
	[XmlAttribute("onDestroy")]
	public	string		onDestroy;
	[XmlAttribute("onPause")]
	public	string		onPause;
	[XmlAttribute("onResume")]
	public	string		onResume;
	[XmlAttribute("onSwitchIn")]
	public	string		onSwitchIn;
	[XmlAttribute("onSwitchOut")]
	public	string		onSwitchOut;

	public	Vector3 GetPosition() {
		return new Vector3 (posX, posY, posZ);
	}

	public	Quaternion GetRotation() {
		return new Quaternion (rotX, rotY, rotZ, 1f);
	}

	public	Vector3 GetScale() {
		return new Vector3 (scaleX, scaleY, scaleZ);
	}
	
	public	FageUITransition GetTransitionOnInstantiate() {
		return FageUIRoot.Instance.FindUITransition (onInstantiate);
	}

	public	FageUITransition GetTransitionOnDestroy() {
		return FageUIRoot.Instance.FindUITransition (onDestroy);
	}

	public	FageUITransition GetTransitionOnPause() {
		return FageUIRoot.Instance.FindUITransition (onPause);
	}

	public	FageUITransition GetTransitionOnResume() {
		return FageUIRoot.Instance.FindUITransition (onResume);
	}

	public	FageUITransition GetTransitionOnSwitchIn() {
		return FageUIRoot.Instance.FindUITransition (onSwitchIn);
	}

	public	FageUITransition GetTransitionOnSwitchOut() {
		return FageUIRoot.Instance.FindUITransition (onSwitchOut);
	}
}

public	class FageUITransition {
	[XmlAttribute("id")]
	public	string		id;
	[XmlAttribute("posX")]
	public	float		posX;
	[XmlAttribute("posY")]
	public	float		posY;
	[XmlAttribute("posZ")]
	public	float		posZ;
	[XmlAttribute("rotX")]
	public	float		rotX;
	[XmlAttribute("rotY")]
	public	float		rotY;
	[XmlAttribute("rotZ")]
	public	float		rotZ;
	[XmlAttribute("scaleX")]
	public	float		scaleX;
	[XmlAttribute("scaleY")]
	public	float		scaleY;
	[XmlAttribute("scaleZ")]
	public	float		scaleZ;
	[XmlAttribute("time")]
	public	float		time;
	[XmlAttribute("delay")]
	public	float		delay;
	[XmlAttribute("easetype")]
	public	LeanTweenType ease;

	public	FageUITransition() {
		posX = posY = posZ = 0f;
		rotX = rotY = rotZ = 1f;
		scaleX = scaleY = scaleZ = 1f;
		time = delay = 0f;
		ease = LeanTweenType.linear;
	}

	public	Vector3 GetPosition() {
		return new Vector3 (posX, posY, posZ);
	}
	
	public	Quaternion GetRotation() {
		return new Quaternion (rotX, rotY, rotZ, 1f);
	}
	
	public	Vector3 GetScale() {
		return new Vector3 (scaleX, scaleY, scaleZ);
	}
}
using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("UIRoot")]
public	class FageUIRoot {
	private	static FageUIRoot _instance;
	public	static FageUIRoot Instance { get { return _instance; } }

	[XmlElement("UITransition")]
	public	FageUITransition[]	transitions;
	[XmlElement("UIDetail")]
	public	FageUIDetail[]		details;
	[XmlElement("UISet")]
	public	FageUISet[]			sets;
	[XmlElement("UIBundle")]
	public	FageUIBundle[]		bundles;

	public	FageUIRoot() {
		transitions = new FageUITransition[0];
		details = new FageUIDetail[0];
		sets = new FageUISet[0];
		bundles = new FageUIBundle[0];;
	}

	public	FageUITransition FindUITransition(string id) {
		return System.Array.Find<FageUITransition> (transitions, t => t.id == id);
	}

	public	FageUIDetail FindUIDetail(string id) {
		return System.Array.Find<FageUIDetail> (details, d => d.id == id);
	}

	public	FageUISet FindUISet(string id) {
		return System.Array.Find<FageUISet> (sets, s => s.id == id);
	}

	public	FageUIBundle FindUIBundle(string id) {
		return System.Array.Find<FageUIBundle> (bundles, b => b.id == id);
	}

	public	static void LoadFromText(string text) {
		var serializer = new XmlSerializer(typeof(FageUIRoot));
		_instance = serializer.Deserialize(new StringReader(text)) as FageUIRoot;
	}
}

public	class FageUIBundle {
	[XmlAttribute("id")]
	public	string	id;
	[XmlAttribute("source")]
	public	string	source;
	[XmlAttribute("dynamic")]
	public	bool	isDynamic;
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
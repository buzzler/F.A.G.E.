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

	public	FageUIRoot() {
		transitions = new FageUITransition[0];
		details = new FageUIDetail[0];
		sets = new FageUISet[0];
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
/*
	public	void Add(FageUITransition transition) {
		ArrayList list = new ArrayList (transitions);
		list.Add (transition);
		transitions = list.ToArray (typeof(FageUITransition)) as FageUITransition[];
	}

	public	void Add(FageUIDetail mode) {
		ArrayList list = new ArrayList (details);
		list.Add (mode);
		details = list.ToArray (typeof(FageUIDetail)) as FageUIDetail[];
	}

	public	void Add(FageUISet set) {
		ArrayList list = new ArrayList (sets);
		list.Add (set);
		sets = list.ToArray (typeof(FageUISet)) as FageUISet[];
	}
*/	
/*
	public	void Save() {
		string path = Utility.GetDataPath ("Setting/ui.xml");
		var serializer = new XmlSerializer(typeof(FageUIRoot));
		using(var stream = new FileStream(path, FileMode.Create)) {
			serializer.Serialize(stream, this);
		}
	}
	
	public	static FageUIRoot Load() {
		string path = Utility.GetDataPath ("Setting/ui.xml");
		var serializer = new XmlSerializer(typeof(FageUIRoot));
		using(var stream = new FileStream(path, FileMode.Open)) {
			return serializer.Deserialize(stream) as FageUIRoot;
		}
	}
*/
	public	static void LoadFromText(string text) {
		var serializer = new XmlSerializer(typeof(FageUIRoot));
		_instance = serializer.Deserialize(new StringReader(text)) as FageUIRoot;
	}
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
	public	FageUIEase	ease;

	public	FageUITransition() {
		posX = posY = posZ = 0f;
		rotX = rotY = rotZ = 1f;
		scaleX = scaleY = scaleZ = 1f;
		time = delay = 0f;
		ease = FageUIEase.LINEAR;
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

public	enum FageUIEase {
	LINEAR,
	SPRING,
	QUAD_IN,
	QUAD_OUT,
	QUAD_INOUT,
	CUBIC_IN,
	CUBIC_OUT,
	CUBIC_INOUT,
	QUART_IN,
	QUAT_OUT,
	QUAT_INOUT,
	QUINT_IN,
	QUINT_OUT,
	QUINT_INOUT,
	SINE_IN,
	SINE_OUT,
	SINE_INOUT,
	EXPO_IN,
	EXPO_OUT,
	EXPO_INOUT,
	CIRC_IN,
	CIRC_OUT,
	CIRC_INOUT,
	BOUNCE_IN,
	BOUNCE_OUT,
	BOUNCE_INOUT,
	BACK_IN,
	BACK_OUT,
	BACK_INOUT,
	ELASTIC_IN,
	ELASTIC_OUT,
	ELASTIC_INOUT
}
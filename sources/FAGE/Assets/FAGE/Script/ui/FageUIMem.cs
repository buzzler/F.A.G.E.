using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public	class FageUIMem : FageCommonMem {
	private	string				_resourcePath;
	private	IFageUIComponent	_component;
	private	Vector3				_position;
	private	Quaternion			_rotation;
	
	public	string				resourcePath	{ get { return _resourcePath; } }
	public	IFageUIComponent	component		{ get { return _component; } }
	
	public	FageUIMem(string resourcePath) : base() {
		_resourcePath		= resourcePath;
		_component			= null;
		_position			= Vector3.zero;
		_rotation			= Quaternion.identity;
	}
	
	public	void Instantiate(Transform canvas, Vector3 position, Quaternion rotation, params object[] param) {
		GameObject cach = CachedResource.Load<GameObject>(_resourcePath);
		_component = (GameObject.Instantiate (cach, position, rotation) as GameObject).GetComponent<IFageUIComponent>();
		_component.GetGameObject().transform.SetParent(canvas, false);
		_component.OnUIInstantiate (this, param);
	}
	
	public	void Destroy() {
		_component.OnUIDestroy (this);
		GameObject.Destroy (_component.GetGameObject());
	}
	
	public	void Resume(Transform canvas, params object[] param) {
		GameObject cach = CachedResource.Load<GameObject>(_resourcePath);
		_component = (GameObject.Instantiate (cach, _position, _rotation) as GameObject).GetComponent<IFageUIComponent>();
		_component.GetGameObject().transform.SetParent(canvas, false);
		_component.OnUIResume(this, param);
	}
	
	public	void Pause() {
		GameObject go = _component.GetGameObject();
		_position = go.transform.localPosition;
		_rotation = go.transform.localRotation;
		_component.OnUIPause(this);
		GameObject.Destroy (go);
	}
}
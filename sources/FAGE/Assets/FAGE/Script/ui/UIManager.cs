using UnityEngine;
using System;
using System.Collections;

public class UIManager : FageEventDispatcher {
	public	Transform canvas;
	private	Stack _stack;

	void Awake() {
		_stack = new Stack ();
	}

	void OnEnable() {
		AddEventListener(FageEvent.UI_CHANGE,	OnUIChange);
		AddEventListener(FageEvent.UI_POPUP,	OnUIPopup);
		AddEventListener(FageEvent.UI_RETURN,	OnUIReturn);
		AddEventListener(FageEvent.UI_FLUSH,	OnUIFlush);
	}

	void OnDisable() {
		RemoveEventListener(FageEvent.UI_CHANGE,OnUIChange);
		RemoveEventListener(FageEvent.UI_POPUP,	OnUIPopup);
		RemoveEventListener(FageEvent.UI_RETURN,OnUIReturn);
		RemoveEventListener(FageEvent.UI_FLUSH,	OnUIFlush);
	}

	private	void OnUIChange(FageEvent fevent) {
		if (_stack.Count != 0) {
			UIMem before = _stack.Pop() as UIMem;
			before.Destroy();
		}

		UIParam param = fevent.data as UIParam;
		UIMem after = new UIMem(param.resourcePath);
		_stack.Push(after);
		after.Instantiate(canvas, param.position, param.rotation, param.param);
	}

	private	void OnUIPopup(FageEvent fevent) {
		if (_stack.Count != 0) {
			UIMem before = _stack.Peek() as UIMem;
			before.Pause();
		}

		UIParam param = fevent.data as UIParam;
		UIMem after = new UIMem(param.resourcePath);
		_stack.Push(after);
		after.Instantiate(canvas, param.position, param.rotation, param.param);
	}

	private	void OnUIReturn(FageEvent fevent) {
		if (_stack.Count > 0) {
			UIMem before = _stack.Pop() as UIMem;
			before.Destroy();
		}

		if (_stack.Count > 0) {
			UIParam param = fevent.data as UIParam;
			UIMem after = _stack.Peek() as UIMem;
			after.Resume(canvas, param.param);
		}
	}

	private	void OnUIFlush(FageEvent fevent) {
		while (_stack.Count > 0) {
			UIMem before = _stack.Pop() as UIMem;
			before.Destroy();
		}
		
		UIParam param = fevent.data as UIParam;
		UIMem after = new UIMem(param.resourcePath);
		_stack.Push(after);
		after.Instantiate(canvas, param.position, param.rotation, param.param);
	}
}

public	class UIParam {
	private	string		_resourcePath;
	private	Vector3		_position;
	private	Quaternion	_rotation;
	private object[]	_param;

	public	string		resourcePath	{ get { return _resourcePath; } }
	public	Vector3		position		{ get { return _position; } }
	public	Quaternion	rotation		{ get { return _rotation; } }
	public	object[]	param			{ get { return _param; } }

	public	UIParam(string resourcePath, params object[] param) {
		_resourcePath = resourcePath;
		_position = Vector3.zero;
		_rotation = Quaternion.identity;
		_param = param;
	}

	public	UIParam(string resourcePath, Vector3 position, Quaternion rotation, params object[] param) {
		_resourcePath = resourcePath;
		_position = position;
		_rotation = rotation;
		_param = param;
	}
}

public	class UIMem {
	private	string		_resourcePath;
	private	IUIComponent	_component;
	private	Vector3		_position;
	private	Quaternion	_rotation;

	public	string		resourcePath	{ get { return _resourcePath; } }
	public	IUIComponent	component		{ get { return _component; } }

	public	UIMem(string resourcePath) {
		_resourcePath	= resourcePath;
		_component		= null;
		_position		= Vector3.zero;
	}

	public	void Instantiate(Transform canvas, Vector3 position, Quaternion rotation, params object[] param) {
		GameObject cach = CachedResource.Load<GameObject>(_resourcePath);
		_component = (GameObject.Instantiate (cach, position, rotation) as GameObject).GetComponent<IUIComponent>();
		_component.GetGameObject().transform.SetParent(canvas);
		_component.OnUIInstantiate (this, param);
	}

	public	void Destroy() {
		_component.OnUIDestroy (this);
		GameObject.Destroy (_component.GetGameObject());
	}

	public	void Resume(Transform canvas, params object[] param) {
		GameObject cach = CachedResource.Load<GameObject>(_resourcePath);
		_component = (GameObject.Instantiate (cach, _position, _rotation) as GameObject).GetComponent<IUIComponent>();
		_component.GetGameObject().transform.SetParent(canvas);
		_component.OnUIResume(this, param);
	}

	public	void Pause() {
		_position = _component.GetPosition();
		_rotation = _component.GetRotation();
		_component.OnUIPause(this);
		GameObject.Destroy (_component.GetGameObject());
	}
}

public	interface IUIComponent {
	void		OnUIInstantiate(UIMem mem, params object[] param);
	void		OnUIDestroy(UIMem mem);
	void		OnUIPause(UIMem mem);
	void		OnUIResume(UIMem mem, params object[] param);
	Vector3		GetPosition();
	Quaternion	GetRotation();
	GameObject	GetGameObject();
}
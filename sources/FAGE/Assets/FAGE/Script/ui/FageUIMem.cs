using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public	class FageUIMem : FageCommonMem {
	private	FageUISet			_uiSet;
	private	IFageUIComponent	_component;
	private	string				_resource;

	public	FageUISet			uiSet			{ get { return _uiSet; } }
	public	IFageUIComponent	component		{ get { return _component; } }

	public	FageUIMem(FageUISet uiSet) : base() {
		_uiSet = uiSet;
		_component = null;
		_resource = null;
	}

	public	void Instantiate(Transform canvas, params object[] param) {
		FageUIDetail uiDetail = _uiSet.GetCurrentUIDetail ();
		_resource = uiDetail.resource;
		GameObject cach = CachedResource.Load<GameObject> (_resource);
		_component = (GameObject.Instantiate (cach, uiDetail.GetPosition(), uiDetail.GetRotation()) as GameObject).GetComponent<IFageUIComponent> ();
		_component.GetGameObject ().transform.SetParent (canvas, false);
		_component.OnUIInstantiate (this, param);
		FageScreenManager.Instance.AddEventListener (FageScreenEvent.ORIENTATION, OnScreenOrientation);
	}
	
	public	void Destroy() {
		FageScreenManager.Instance.RemoveEventListener (FageScreenEvent.ORIENTATION, OnScreenOrientation);
		_component.OnUIDestroy (this);
		_resource = null;
		GameObject.Destroy (_component.GetGameObject ());
	}
	
	public	void Resume(Transform canvas, params object[] param) {
		FageUIDetail uiDetail = _uiSet.GetCurrentUIDetail ();
		_resource = uiDetail.resource;
		GameObject cach = CachedResource.Load<GameObject> (_resource);
		_component = (GameObject.Instantiate (cach, uiDetail.GetPosition(), uiDetail.GetRotation()) as GameObject).GetComponent<IFageUIComponent> ();
		_component.GetGameObject ().transform.SetParent (canvas, false);
		_component.OnUIResume (this, param);
		FageScreenManager.Instance.AddEventListener (FageScreenEvent.ORIENTATION, OnScreenOrientation);
	}
	
	public	void Pause() {
		FageScreenManager.Instance.RemoveEventListener (FageScreenEvent.ORIENTATION, OnScreenOrientation);
		_component.OnUIPause (this);
		_resource = null;
		GameObject.Destroy (_component.GetGameObject ());
	}

	private	void OnScreenOrientation(FageEvent fevent) {
		FageUIDetail uiDetail = _uiSet.GetCurrentUIDetail ();
		if (uiDetail.resource == _resource)
			return;
		else
			_resource = uiDetail.resource;

		GameObject go = _component.GetGameObject ();
		Transform canvas = go.transform.parent;
		_component.OnSwitchOut (this);
		GameObject.Destroy (go);

		GameObject cach = CachedResource.Load<GameObject> (_resource);
		_component = (GameObject.Instantiate (cach, uiDetail.GetPosition(), uiDetail.GetRotation()) as GameObject).GetComponent<IFageUIComponent> ();
		_component.GetGameObject ().transform.SetParent (canvas, false);
		_component.OnSwitchIn (this);
	}
}
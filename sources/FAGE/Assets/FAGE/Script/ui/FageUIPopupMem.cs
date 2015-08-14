using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public	class FageUIPopupMem : FageCommonMem {
	private	FageUISet				_uiSet;
	private	IFageUIPopupComponent	_component;
	private	string					_resource;
	
	public	FageUISet				uiSet			{ get { return _uiSet; } }
	public	IFageUIPopupComponent	component		{ get { return _component; } }
	
	public	FageUIPopupMem(FageUISet uiSet) : base() {
		_uiSet = uiSet;
		_component = null;
	}

	public	void Instantiate(Transform canvas, params object[] param) {
		FageUIDetail uiDetail = _uiSet.GetCurrentUIDetail ();
		_resource = uiDetail.resource;
		GameObject cach = CachedResource.Load<GameObject> (_resource);
		_component = (GameObject.Instantiate (cach, uiDetail.GetPosition(), uiDetail.GetRotation()) as GameObject).GetComponent<IFageUIPopupComponent>();
		_component.GetGameObject().transform.SetParent(canvas, false);
		_component.OnUIInstantiate (this, param);
		FageScreenManager.Instance.AddEventListener(FageScreenEvent.ORIENTATION, OnScreenOrientation);
	}

	public	void Destroy() {
		FageScreenManager.Instance.RemoveEventListener(FageScreenEvent.ORIENTATION, OnScreenOrientation);
		_component.OnUIDestroy (this);
		_resource = null;
		GameObject.Destroy (_component.GetGameObject());
	}

	private	void OnScreenOrientation(FageEvent fevent) {
		FageUIDetail uiDetail = _uiSet.GetCurrentUIDetail ();
		if (_resource == uiDetail.resource)
			return;
		else
			_resource = uiDetail.resource;

		GameObject go = _component.GetGameObject();
		Transform canvas = go.transform.parent;
		_component.OnSwitchOut(this);
		GameObject.Destroy (go);

		GameObject cach = CachedResource.Load<GameObject> (_resource);
		_component = (GameObject.Instantiate (cach, uiDetail.GetPosition(), uiDetail.GetRotation()) as GameObject).GetComponent<IFageUIPopupComponent>();
		_component.GetGameObject().transform.SetParent(canvas, false);
		_component.OnSwitchIn (this);
	}
}
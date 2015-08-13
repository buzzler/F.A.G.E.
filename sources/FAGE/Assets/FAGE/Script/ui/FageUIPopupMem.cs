using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public	class FageUIPopupMem : FageCommonMem {
	private	FageUIInfo				_uiInfo;
	private	IFageUIPopupComponent	_component;
	
	public	FageUIInfo				uiInfo			{ get { return _uiInfo; } }
	public	IFageUIPopupComponent	component		{ get { return _component; } }
	
	public	FageUIPopupMem(FageUIInfo uiInfo) : base() {
		_uiInfo = uiInfo;
		_component = null;
	}

	public	void Instantiate(Transform canvas, params object[] param) {
		GameObject cach = CachedResource.Load<GameObject> (_uiInfo.GetCurrentOrientedResourcePath ());
		_component = (GameObject.Instantiate (cach, _uiInfo.position, _uiInfo.rotation) as GameObject).GetComponent<IFageUIPopupComponent>();
		_component.GetGameObject().transform.SetParent(canvas, false);
		_component.OnUIInstantiate (this, param);
		FageScreenManager.Instance.AddEventListener(FageScreenEvent.ORIENTATION, OnScreenOrientation);
	}

	public	void Destroy() {
		FageScreenManager.Instance.RemoveEventListener(FageScreenEvent.ORIENTATION, OnScreenOrientation);
		_component.OnUIDestroy (this);
		GameObject.Destroy (_component.GetGameObject());
	}

	private	void OnScreenOrientation(FageEvent fevent) {
		GameObject go = _component.GetGameObject();
		Transform canvas = go.transform.parent;
		_component.OnSwitchOut(this);
		GameObject.Destroy (go);
		
		GameObject cach = CachedResource.Load<GameObject> (_uiInfo.GetCurrentOrientedResourcePath ());
		_component = (GameObject.Instantiate (cach, _uiInfo.position, _uiInfo.rotation) as GameObject).GetComponent<IFageUIPopupComponent>();
		_component.GetGameObject().transform.SetParent(canvas, false);
		_component.OnSwitchIn (this);
	}
}
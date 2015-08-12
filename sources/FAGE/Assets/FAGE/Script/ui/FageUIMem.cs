using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public	class FageUIMem : FageCommonMem {
	private	FageUIInfo			_uiInfo;
	private	IFageUIComponent	_component;

	public	FageUIInfo			uiInfo			{ get { return _uiInfo; } }
	public	IFageUIComponent	component		{ get { return _component; } }

	public	FageUIMem(FageUIInfo uiInfo) : base() {
		_uiInfo = uiInfo;
		_component = null;
	}

	public	void Instantiate(Transform canvas, params object[] param) {
		GameObject cach = CachedResource.Load<GameObject>(GetCurrentOrientedResourcePath());
		_component = (GameObject.Instantiate (cach, _uiInfo.position, _uiInfo.rotation) as GameObject).GetComponent<IFageUIComponent>();
		_component.GetGameObject().transform.SetParent(canvas, false);
		_component.OnUIInstantiate (this, param);
		FageScreenManager.Instance.AddEventListener(FageScreenEvent.ORIENTATION, OnScreenOrientation);
	}
	
	public	void Destroy() {
		FageScreenManager.Instance.RemoveEventListener(FageScreenEvent.ORIENTATION, OnScreenOrientation);
		_component.OnUIDestroy (this);
		GameObject.Destroy (_component.GetGameObject());
	}
	
	public	void Resume(Transform canvas, params object[] param) {
		GameObject cach = CachedResource.Load<GameObject>(GetCurrentOrientedResourcePath());
		_component = (GameObject.Instantiate (cach, _uiInfo.position, _uiInfo.rotation) as GameObject).GetComponent<IFageUIComponent>();
		_component.GetGameObject().transform.SetParent(canvas, false);
		_component.OnUIResume(this, param);
		FageScreenManager.Instance.AddEventListener(FageScreenEvent.ORIENTATION, OnScreenOrientation);
	}
	
	public	void Pause() {
		FageScreenManager.Instance.RemoveEventListener(FageScreenEvent.ORIENTATION, OnScreenOrientation);
		_component.OnUIPause(this);
		GameObject.Destroy (_component.GetGameObject());
	}

	private	void OnScreenOrientation(FageEvent fevent) {
		GameObject go = _component.GetGameObject();
		Transform canvas = go.transform.parent;
		_component.OnSwitchOut(this);
		GameObject.Destroy (go);

		GameObject cach = CachedResource.Load<GameObject>(GetCurrentOrientedResourcePath());
		_component = (GameObject.Instantiate (cach, _uiInfo.position, _uiInfo.rotation) as GameObject).GetComponent<IFageUIComponent>();
		_component.GetGameObject().transform.SetParent(canvas, false);
		_component.OnSwitchIn (this);
	}

	private	string GetCurrentOrientedResourcePath() {
		switch (FageScreenManager.Instance.orientation) {
		case DeviceOrientation.Portrait:
		case DeviceOrientation.PortraitUpsideDown:
			return _uiInfo.resourcePathPortrait;
		case DeviceOrientation.LandscapeLeft:
		case DeviceOrientation.LandscapeRight:
			return _uiInfo.resourcePathLandscape;
		default:
			return null;
		}
	}
}

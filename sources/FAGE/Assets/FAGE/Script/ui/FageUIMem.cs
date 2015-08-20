using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public	class FageUIMem : FageUICommonMem {
	private	FageUISet			_uiSet;
	private	IFageUIComponent	_component;
	private	FageUIDetail		_uiDetail;

	public	FageUISet			uiSet			{ get { return _uiSet; } }
	public	IFageUIComponent	component		{ get { return _component; } }
	public	FageUIDetail		uiDetail		{ get { return _uiDetail; } }

	public	FageUIMem(FageUISet uiSet) : base() {
		_uiSet = uiSet;
		_component = null;
		_uiDetail = null;
	}

	public	void Instantiate(Transform canvas, params object[] param) {
		_uiDetail = _uiSet.GetCurrentUIDetail ();
		FageUITransition transition = _uiDetail.GetTransitionOnInstantiate ();
		GameObject cach = CachedResource.Load<GameObject> (_uiDetail.resource);
		_component = (GameObject.Instantiate (cach, transition.GetPosition (), transition.GetRotation ()) as GameObject).GetComponent<IFageUIComponent> ();
		_component.GetGameObject ().transform.SetParent (canvas, false);
		_component.OnUIInstantiate (this, param);
		LeanTween.moveLocal (_component.GetGameObject (), _uiDetail.GetPosition (), transition.time).setDelay (transition.delay).setEase (transition.ease).setOnComplete (OnInstantiateComplete);
	}

	private void OnInstantiateComplete() {
		FageScreenManager.Instance.AddEventListener (FageScreenEvent.ORIENTATION, OnScreenOrientation);
		SetState (FageUICommonMem.INTANTIATED);
	}

	public	void Destroy() {
		FageUITransition transition = _uiDetail.GetTransitionOnDestroy ();
		LeanTween.moveLocal (_component.GetGameObject (), transition.GetPosition (), transition.time).setDelay (transition.delay).setEase (transition.ease).setOnComplete (OnDestroyComplete);
	}

	private	void OnDestroyComplete() {
		FageScreenManager.Instance.RemoveEventListener (FageScreenEvent.ORIENTATION, OnScreenOrientation);
		_component.OnUIDestroy (this);
		_uiDetail = null;
		GameObject.Destroy (_component.GetGameObject ());
		SetState (FageUICommonMem.DESTROIED);
	}
	
	public	void Resume(Transform canvas, params object[] param) {
		_uiDetail = _uiSet.GetCurrentUIDetail ();
		FageUITransition transition = _uiDetail.GetTransitionOnResume ();
		GameObject cach = CachedResource.Load<GameObject> (_uiDetail.resource);
		_component = (GameObject.Instantiate (cach, transition.GetPosition(), transition.GetRotation()) as GameObject).GetComponent<IFageUIComponent> ();
		_component.GetGameObject ().transform.SetParent (canvas, false);
		_component.OnUIResume (this, param);
		LeanTween.moveLocal (_component.GetGameObject (), _uiDetail.GetPosition (), transition.time).setDelay (transition.delay).setEase (transition.ease).setOnComplete (OnResumeComplete);
	}

	private	void OnResumeComplete() {
		FageScreenManager.Instance.AddEventListener (FageScreenEvent.ORIENTATION, OnScreenOrientation);
		SetState (FageUICommonMem.INTANTIATED);
	}
	
	public	void Pause() {
		FageUITransition transition = _uiDetail.GetTransitionOnPause ();
		LeanTween.moveLocal (_component.GetGameObject (), transition.GetPosition (), transition.time).setDelay (transition.delay).setEase (transition.ease).setOnComplete (OnPauseComplete);
	}

	private	void OnPauseComplete() {
		FageScreenManager.Instance.RemoveEventListener (FageScreenEvent.ORIENTATION, OnScreenOrientation);
		_component.OnUIPause (this);
		_uiDetail = null;
		GameObject.Destroy (_component.GetGameObject ());
		SetState (FageUICommonMem.PAUSED);
	}

	private	void OnScreenOrientation(FageEvent fevent) {
		FageUIDetail bakDetail = _uiDetail;
		_uiDetail = _uiSet.GetCurrentUIDetail ();
		if (bakDetail == _uiDetail)
			return;

		FageUITransition transition = bakDetail.GetTransitionOnSwitchOut ();
		LeanTween.moveLocal (_component.GetGameObject (), transition.GetPosition (), transition.time).setDelay (transition.delay).setEase (transition.ease).setOnComplete (OnScreenOrientationOut);
	}

	private	void OnScreenOrientationOut() {
		GameObject go = _component.GetGameObject ();
		Transform canvas = go.transform.parent;
		_component.OnSwitchOut (this);
		GameObject.Destroy (go);
		
		FageUITransition transition = _uiDetail.GetTransitionOnSwitchIn ();
		GameObject cach = CachedResource.Load<GameObject> (_uiDetail.resource);
		_component = (GameObject.Instantiate (cach, transition.GetPosition(), transition.GetRotation()) as GameObject).GetComponent<IFageUIComponent> ();
		_component.GetGameObject ().transform.SetParent (canvas, false);
		_component.OnSwitchIn (this);
		LeanTween.moveLocal (_component.GetGameObject (), _uiDetail.GetPosition (), transition.time).setDelay (transition.delay).setEase (transition.ease);
	}
}
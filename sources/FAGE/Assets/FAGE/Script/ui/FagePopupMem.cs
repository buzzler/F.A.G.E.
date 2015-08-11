using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public	class FagePopupMem : FageCommonMem {
	private	string						_resourcePath;
	private	FageUIParam					_param;
	private	IFagePopupComponent			_component;
	
	public	string				resourcePath	{ get { return _resourcePath; } }
	public	FageUIParam			param			{ get { return _param; } }
	public	IFagePopupComponent	component		{ get { return _component; } }
	
	public	FagePopupMem(string resourcePath, FageUIParam param) : base() {
		_resourcePath		= resourcePath;
		_param				= param;
		_component			= null;
	}
	
	public	void Instantiate(Transform canvas) {
		GameObject cach = CachedResource.Load<GameObject>(_resourcePath);
		_component = (GameObject.Instantiate (cach, param.position, param.rotation) as GameObject).GetComponent<IFagePopupComponent>();
		_component.GetGameObject().transform.SetParent(canvas, false);
		_component.OnUIInstantiate (this, param.param);
	}
	
	public	void Destroy() {
		_component.OnUIDestroy (this);
		GameObject.Destroy (_component.GetGameObject());
	}
}
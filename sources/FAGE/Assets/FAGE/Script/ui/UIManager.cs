using UnityEngine;
using System.Collections;

public class UIManager : FageEventDispatcher {
	public	Stack _stack;

	void Awake() {
		_stack = new Stack ();
	}
}


public	class UIMem {
	private	string		_resourcePath;
	private	UIComponent	_component;

	public	string		resourcePath	{ get { return _resourcePath; } }
	public	UIComponent	component		{ get { return _component; } }

	public	UIMem(string resourcePath) {
		_resourcePath = resourcePath;
	}

	public	void Instantiate(Vector3 position) {
		UIComponent cach = CachedResource.Load<UIComponent>(_resourcePath);
		_component = GameObject.Instantiate (cach, position, Quaternion.identity) as UIComponent;
		_component.OnInstantiate (this);
	}

	public	void Destroy() {
		_component.OnDestroy (this);
		GameObject.Destroy (_component.gameObject);
	}
}

public	class UIComponent : MonoBehaviour {
	virtual public	void OnInstantiate(UIMem mem) {
	}

	virtual public	void OnDestroy(UIMem mem) {
	}
}
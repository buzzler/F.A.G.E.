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
	private	GameObject	_gameObject;

	public	string		resourcePath	{ get { return _resourcePath; } }
	public	GameObject	gameObject		{ get { return _gameObject; } }

	public	UIMem(string resourcePath) {
		_resourcePath = resourcePath;
	}

	public	void Instantiate() {
		GameObject cach = CachedResource.Load<GameObject>(_resourcePath);
		_gameObject = GameObject.Instantiate<GameObject>(cach);
	}

	public	void Destroy() {
		_gameObject.DestroyPooled();
	}
}

public	class UIComponent : MonoBehaviour {
}
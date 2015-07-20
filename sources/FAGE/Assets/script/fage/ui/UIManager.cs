using UnityEngine;
using System.Collections;

public class UIManager : FageEventDispatcher {
	public	Stack _stack;

	void Awake() {
		_stack = new Stack ();
	}
}


public	class UIMem {
	private	string _resource;

	public	string resource { get { return _resource; } }

	public	UIMem(string resource) {
		_resource = resource;
	}
}

public	class UIComponent : MonoBehaviour {
}
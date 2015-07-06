using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

[AddComponentMenu("Fage/FSM/FageStateMachine")]
public	class FageStateMachine : FageEventDispatcher {
	public	string		reserve;
	private string		_id;
	private	FageState	_current;
	private	Hashtable	_bool;
	private	Hashtable	_trigger;
	private	Hashtable	_int;
	private	Hashtable	_float;
	
	public	string id {
		get {
			return _id;
		}
	}

	public	FageStateMachine() {
		_id			= GetType().FullName + "(" + GetInstanceID().ToString() + ")";
		_bool		= new Hashtable ();
		_trigger	= new Hashtable ();
		_int		= new Hashtable ();
		_float		= new Hashtable ();
	}
	
	public	FageState current {
		get {
			return _current;
		}
	}
	
	public	virtual void ReserveState(string id) {
		reserve = id;
	}
	
	public	virtual void SetState(string id) {
		if (_current != null) {
			_current.BeforeSwitch (this, id);
		}
		
		string temp = (_current != null) ? _current.id : null;
		Type stateType = Type.GetType (id, false, true);
		if (stateType == null) {
			throw new UnityException ();
		}
		ConstructorInfo stateConstructor = stateType.GetConstructor (Type.EmptyTypes);
		object stateObject = stateConstructor.Invoke (new object[]{});
		if (stateObject is FageState) {
			_current = (FageState)stateObject;
		} else {
			throw new UnityException ();
		}
		
		if (_current != null) {
			_current.AfterSwitch (this, temp);
		}
	}

	public	virtual void SendMessage(FageMessage message) {
		if (_current != null) {
			_current.ReceiveMessage(this, message);
		}
	}
	
	public	virtual	void SetBool(string key, bool value) {
		_bool [key] = value;
	}
	
	public	virtual	bool GetBool(string key) {
		return (bool)GetObject (_bool, key);
	}
	
	public	virtual	void SetTrigger(string key) {
		_trigger [key] = true;
	}
	
	public	virtual	bool GetTrigger(string key) {
		bool result = (bool)GetObject (_trigger, key);
		_trigger [key] = false;
		return result;
	}
	
	public	virtual	void SetInt(string key, int value) {
		_int [key] = value;
	}
	
	public	virtual	int GetInt(string key) {
		return (int)GetObject (_int, key);
	}
	
	public	virtual	void SetFloat(string key, float value) {
		_float [key] = value;
	}
	
	public	virtual	float GetFloat(string key) {
		return (float)GetObject (_float, key);
	}
	
	private object GetObject(Hashtable hash, string key) {
		if ((hash != null) && (hash.ContainsKey (key))) {
			return hash [key];
		} else {
			throw new UnityException ();
		}
	}

	void LateUpdate() {
		if (_current != null) {
			_current.Excute (this);
		}
		if (!String.IsNullOrEmpty (reserve)) {
			SetState (reserve);
			reserve = null;
		}
	}
}
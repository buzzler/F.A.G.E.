using UnityEngine;
using System.Collections;

namespace Fage {
	
	namespace Events {
		public	class FageEvent {
			public const string ERROR = "ERROR";

			private	string _type;
			private	object _data;
			
			public	string type {
				get {
					return _type;
				}
			}
			
			public	object data {
				get {
					return _data;
				}
			}
			
			public	FageEvent(string type, object data) {
				_type = type;
				_data = data;
			}
		}
		
		public	delegate void FageEventHandler(FageEvent fevent);

		public	class EventDispatcher : MonoBehaviour {
			private		const	int			MAX_LOG		= 50;
			private		static	object[]	log_stack	= new object[MAX_LOG];
			private		static	int			log_index	= 0;
			private		static	Hashtable	event_hash	= new Hashtable ();
			
			protected	static void Log(object message) {
				log_stack [log_index] = message;
				log_index = (log_index + 1) % MAX_LOG;
			}
			
			protected	static void AddEventListener(string type, FageEventHandler func) {
				if (event_hash.ContainsKey (type)) {
					FageEventHandler handler = event_hash [type] as FageEventHandler;
					handler += func;
				} else {
					event_hash.Add(type, func);
				}
			}
			
			protected	static void RemoveEventListener(string type, FageEventHandler func) {
				if (event_hash.ContainsKey (type)) {
					FageEventHandler handler = event_hash [type] as FageEventHandler;
					handler -= func;
				}
			}
			
			protected	static void DispatchEvent(FageEvent fevent) {
				if (event_hash.ContainsKey (fevent.type)) {
					FageEventHandler handler = event_hash [fevent.type] as FageEventHandler;
					handler(fevent);
				}
			}
		}
	}

	namespace FSM {
		using Events;
		using System;
		using System.Reflection;

		public	class State {
			private string _id;

			public	string id {
				get {
					return _id;
				}
			}

			public State() {
				_id = GetType().FullName;
			}

			public	virtual	void BeforeSwitch(StateMachine stateMachine, string afterId = null) {
			}

			public	virtual	void AfterSwitch(StateMachine stateMachine, string beforeId = null) {
			}

			public	virtual	void Excute(StateMachine stateMachine) {
			}
		}

		public	class StateMachine : EventDispatcher {
			public	string		reserve;
			private	State		_current;
			private	Hashtable	_bool;
			private	Hashtable	_trigger;
			private	Hashtable	_int;
			private	Hashtable	_float;

			public	StateMachine() {
				_bool		= new Hashtable ();
				_trigger	= new Hashtable ();
				_int		= new Hashtable ();
				_float		= new Hashtable ();
			}

			public	State current {
				get {
					return _current;
				}
			}

			public	void ReserveState(string id) {
				reserve = id;
			}

			public	void SetState(string id) {
				if (_current != null) {
					_current.BeforeSwitch(this, id);
				}
				
				string temp = (_current!=null) ? _current.id:null;
				Type stateType = Type.GetType (id, false, true);
				if (stateType == null) {
					throw new UnityException ();
				}
				ConstructorInfo stateConstructor = stateType.GetConstructor (Type.EmptyTypes);
				object stateObject =  stateConstructor.Invoke (new object[]{});
				if (stateObject is State) {
					_current = (State)stateObject;
				} else {
					throw new UnityException ();
				}
				
				if (_current != null) {
					_current.AfterSwitch(this, temp);
				}
			}

			public	void SetBool(string key, bool value) {
				_bool [key] = value;
			}

			public	bool GetBool(string key) {
				return (bool)GetObject (_bool, key);
			}

			public	void SetTrigger(string key) {
				_trigger [key] = true;
			}
			
			public	bool GetTrigger(string key) {
				bool result = (bool)GetObject (_trigger, key);
				_trigger [key] = false;
				return result;
			}

			public	void SetInt(string key, int value) {
				_int [key] = value;
			}
			
			public	int GetInt(string key) {
				return (int)GetObject (_int, key);
			}

			public	void SetFloat(string key, float value) {
				_float [key] = value;
			}

			public	float GetFloat(string key) {
				return (float)GetObject (_float, key);
			}

			private object GetObject(Hashtable hash, string key) {
				if ((hash != null) && (hash.ContainsKey (key))) {
					return hash [key];
				} else {
					throw new UnityException();
				}
			}

			void LateUpdate() {
				if (_current!=null) {
					_current.Excute(this);
				}
				if (!String.IsNullOrEmpty(reserve)) {
					SetState (reserve);
					reserve = null;
				}
			}
		}
	}

}
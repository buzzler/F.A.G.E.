using UnityEngine;
using System.Collections;

namespace Fage {
	
	namespace Events {
		public	class FageEvent {
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

		public	class State {

			public	State() {
			}
		}

		public	class StateMachine : EventDispatcher {

		}
	}

}
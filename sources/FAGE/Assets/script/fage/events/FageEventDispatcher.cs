using UnityEngine;
using System.Collections;

[AddComponentMenu("Fage/Events/FageEventDispatcher")]
public	class FageEventDispatcher : MonoBehaviour {
	private		static event FageEventHandler dummy;
	private		const	int			MAX_LOG		= 50;
	private		static	object[]	log_stack	= new object[MAX_LOG];
	private		static	int			log_index	= 0;
	private		static	Hashtable	event_hash	= new Hashtable ();
	
	public	static void Log(object message) {
		log_stack [log_index] = message;
		log_index = (log_index + 1) % MAX_LOG;
		Debug.Log (message);
	}
	
	public	static void AddEventListener(string type, FageEventHandler func) {
		if (event_hash.ContainsKey (type)) {
			FageEventHandler handler = event_hash [type] as FageEventHandler;
			handler += func;
			event_hash [type] = handler;
		} else {
			dummy += func;
			FageEventHandler handler = dummy.Clone() as FageEventHandler;
			dummy -= func;
			event_hash.Add (type, handler);
		}
	}
	
	public	static void RemoveEventListener(string type, FageEventHandler func) {
		if (event_hash.ContainsKey (type)) {
			FageEventHandler handler = event_hash [type] as FageEventHandler;
			handler -= func;
			event_hash [type] = handler;
		}
	}
	
	public	static void DispatchEvent(FageEvent fevent) {
		if ((fevent != null) && event_hash.ContainsKey (fevent.type)) {
			FageEventHandler handler = event_hash [fevent.type] as FageEventHandler;
			handler (fevent);
		}
	}
}
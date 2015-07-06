using UnityEngine;

public class FageMessage {
	private	string _sender;
	private object _message;

	public	string sender { get { return _sender; } }
	public	object message{ get { return _message; } }

	public FageMessage(string sender, object message) {
		_sender = sender;
		_message = message;
	}
}

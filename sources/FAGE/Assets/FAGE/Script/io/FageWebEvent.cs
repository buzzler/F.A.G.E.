using UnityEngine;

public class FageWebEvent : FageEvent {
	private	int		_requestId;
	private WWW		_www;
	
	public	int		requestId	{ get { return _requestId; } }
	public	string	url			{ get { return _www.url; } }
	public	WWW		www			{ get { return _www; } }
	
	public	FageWebEvent(string type, int requestId, WWW www) : base(type) {
		_requestId = requestId;
		_www = www;
	}
}
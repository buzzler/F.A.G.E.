using UnityEngine;

public class FageWebResponse {
	private	string	_receiver;
	private WWW		_www;
	
	public	string	receiver	{ get { return _receiver; } }
	public	string	url			{ get { return _www.url; } }
	public	WWW		www			{ get { return _www; } }
	
	public	FageWebResponse(string receiver, WWW www) {
		if (!www.isDone) {
			new UnityException();
		}
		_receiver = receiver;
		_www = www;
	}
}
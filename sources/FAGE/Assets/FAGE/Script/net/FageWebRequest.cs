using UnityEngine;

public	class FageWebRequest {
	private	string	_sender;
	private	string	_url;
	private	WWWForm	_form;
	
	public	string	sender	{ get { return _sender; } }
	public	string	url		{ get { return _url; } }
	public	WWWForm	form	{ get { return _form; } }
	
	public	FageWebRequest(string sender, string url, WWWForm form = null) {
		_sender = sender;
		_url = url;
		_form = form;
	}
}
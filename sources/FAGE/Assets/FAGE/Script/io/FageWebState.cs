using UnityEngine;

public	class FageWebState {
	private	int		_requestId;
	private	string	_url;
	private	WWWForm	_wwwForm;
	
	public	int		requestId	{ get { return _requestId; } }
	public	string	url			{ get { return _url; } }
	public	WWWForm	wwwForm		{ get { return _wwwForm; } }
	
	public	FageWebState(int requestId, string url, WWWForm wwwForm = null) {
		_requestId = requestId;
		_url = url;
		_wwwForm = wwwForm;
	}
}
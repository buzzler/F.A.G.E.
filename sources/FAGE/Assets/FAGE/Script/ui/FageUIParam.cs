using UnityEngine;

public	class FageUIParam {
	private	string		_resourcePath;
	private	Vector3		_position;
	private	Quaternion	_rotation;
	private object[]	_param;
	
	public	string		resourcePath	{ get { return _resourcePath; } }
	public	Vector3		position		{ get { return _position; } }
	public	Quaternion	rotation		{ get { return _rotation; } }
	public	object[]	param			{ get { return _param; } }
	
	public	FageUIParam(string resourcePath, params object[] param) {
		_resourcePath = resourcePath;
		_position = Vector3.zero;
		_rotation = Quaternion.identity;
		_param = param;
	}
	
	public	FageUIParam(string resourcePath, Vector3 position, Quaternion rotation, params object[] param) {
		_resourcePath = resourcePath;
		_position = position;
		_rotation = rotation;
		_param = param;
	}
}
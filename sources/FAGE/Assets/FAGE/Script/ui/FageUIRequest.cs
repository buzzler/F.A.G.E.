using UnityEngine;
using System.Collections;

public class FageUIRequest {
	public	const string POPUP		= "popup";
	public	const string POPDOWN	= "popdown";
	public	const string PUSH		= "push";
	public	const string POP		= "pop";
	public	const string CHANGE		= "change";
	public	const string FLUSH		= "flush";
	public	const string LEVEL		= "level";

	private	string			_command;
	private	FageUISet		_uiSet;
	private	FageUIScene		_uiScene;
	private	FageUICurtain	_uiCurtain;
	private	object[]		_param;

	public	string			command		{ get { return _command; } }
	public	FageUISet		uiSet		{ get { return _uiSet; } }
	public	FageUIScene		uiScene		{ get { return _uiScene; } }
	public	FageUICurtain	uiCurtain	{ get { return _uiCurtain; } }
	public	object[]		param		{ get { return _param; } }

	public	FageUIRequest(string command, FageUISet uiSet, params object[] param) {
		_command = command;
		_uiSet = uiSet;
		_param = param;
	}

	public	FageUIRequest(string command, FageUIScene uiScene, FageUICurtain uiCurtain) {
		_command = command;
		_uiScene = uiScene;
		_uiCurtain = uiCurtain;
	}

	public	FageUIRequest(string command, params object[] param) {
		_command = command;
		_uiSet = null;
		_param = param;
	}

	public	FageUIRequest(string command) {
		_command = command;
		_uiSet = null;
		_param = null;
	}
}

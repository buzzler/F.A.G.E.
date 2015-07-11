using UnityEngine;

[System.Serializable]
public	class FageAudioRequest {
	private	string				_sender;
	private	FageAudioCommand	_command;
	private	string				_node;
	private	string				_source;
	private	bool				_loop;
	private	float				_volumn;
	
	public	string				sender	{ get { return _sender; } }
	public	FageAudioCommand	command { get { return _command; } }
	public	string				node	{ get { return _node; } }
	public	string				source	{ get { return _source; } }
	public	bool				loop	{ get { return _loop; } }
	public	float				volumn	{ get { return _volumn; } }
	
	private	void Init() {
		_sender	= null;
		_command= FageAudioCommand.NONE;
		_node	= null;
		_source	= null;
		_loop	= false;
		_volumn	= 1;
	}
	
	public	FageAudioRequest(string sender, FageAudioCommand command, string node) {
		Init ();
		_sender = sender;
		_command = command;
		_node = node;
	}
	
	public	FageAudioRequest(string sender, FageAudioCommand command, string node, string source) {
		Init ();
		_sender = sender;
		_command = command;
		_node = node;
		_source = source;
	}
	
	public	FageAudioRequest(string sender, FageAudioCommand command, string node, string source, bool loop) {
		Init ();
		_sender = sender;
		_command = command;
		_node = node;
		_source = source;
		_loop = loop;
	}
	
	public	FageAudioRequest(string sender, FageAudioCommand command, string node, float volumn) {
		Init ();
		_sender = sender;
		_command = command;
		_node = node;
		_volumn = volumn;
	}
}
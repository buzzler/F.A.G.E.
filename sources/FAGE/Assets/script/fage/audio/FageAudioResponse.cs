public	class FageAudioResponse {
	private	string			_receiver;
	private	FageAudioStatus	_status;
	
	public	string			receiver	{ get { return _receiver; } }
	public	FageAudioStatus	status		{ get { return _status; } }
	
	public	FageAudioResponse(string receiver, FageAudioStatus status) {
		_receiver = receiver;
		_status = status;
	}
}
public	class FageAudioResponse {
	private	string					_receiver;
	private	FageAudioSourceControl	_control;
	
	public	string					receiver	{ get { return _receiver; } }
	public	FageAudioSourceControl	control		{ get { return _control; } }
	
	public	FageAudioResponse(string receiver, FageAudioSourceControl control) {
		_receiver = receiver;
		_control = control;
	}
}
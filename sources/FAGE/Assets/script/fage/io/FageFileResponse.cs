public	class FageFileResponse {
	private	string	_receiver;
	private	string	_filepath;
	private	byte[]	_data;
	
	public	string	receiver	{ get { return _receiver; } }
	public	string	filepath	{ get { return _filepath; } }
	public	byte[]	data		{ get { return _data; } }
	
	public	FageFileResponse(string receiver, string filepath, byte[] data = null) {
		_receiver = receiver;
		_filepath = filepath;
		_data = data;
	}
}
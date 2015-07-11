public	class FageFileRequest {
	private	string			_sender;
	private	string			_filepath;
	private FageFileMode	_mode;
	private	byte[]			_data;
	
	public	string			sender		{ get { return _sender; } }
	public	string			filepath	{ get { return _filepath; } }
	public	FageFileMode	mode		{ get { return _mode; } }
	public	byte[]			data		{ get { return _data; } }
	
	public FageFileRequest(string sender, string filepath, FageFileMode mode, byte[] data = null) {
		_sender = sender;
		_filepath = filepath;
		_mode = mode;
		_data = data;
	}
}
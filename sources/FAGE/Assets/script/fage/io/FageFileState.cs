using System.IO;

public	class FageFileState {
	private FileStream		_stream;
	private	FageFileRequest	_request;
	private	byte[]			_data;
	
	public	FileStream		stream	{ get { return _stream; } }
	public	FageFileRequest	request	{ get { return _request; } }
	public	byte[]			data	{ get { return _data; } }
	
	public	FageFileState(FageFileRequest request, FileStream stream, byte[] data) {
		_stream = stream;
		_request = request;
		_data = data;
	}
}
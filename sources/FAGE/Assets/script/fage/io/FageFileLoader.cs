using UnityEngine;
using System.IO;

public class FageFileLoader : FageEventDispatcher {
	private	const int			_MAX_QUEUE = 100;
	private	FageFileRequest[]	_requests;
	private int					_index_push;
	private int					_index_pop;
	private	FageFileRequest		_current;

	void OnEnable() {
		AddEventListener (FageEvent.FILE_REQUEST, OnRequestEvent);
	}
	
	void OnDisable() {
		RemoveEventListener (FageEvent.FILE_REQUEST, new FageEventHandler (OnRequestEvent));
	}

	private	void OnRequestEvent(FageEvent fevent) {
		FageFileRequest request = fevent.data as FageFileRequest;
		switch (request.mode) {
		case FageFileMode.LOAD_ASYNC:
			Load (request);
			break;
		case FageFileMode.SAVE_ASYNC:
			Save (request);
			break;
		}
	}

	private	void Load(FageFileRequest request) {
		byte[] data;
		using (FileStream stream = new FileStream (request.filepath, FileMode.OpenOrCreate)) {
			data = new byte[stream.Length];
			stream.BeginRead(data, 0, data.Length, new System.AsyncCallback(OnLoad), new FageFileState(request, stream, data));
		}
	}

	private void OnLoad(System.IAsyncResult result) {
		FageFileState state		= result.AsyncState as FageFileState;
		FileStream stream		= state.stream;
		FageFileRequest request	= state.request;
		byte[] data				= state.data;

		DispatchEvent (new FageEvent(FageEvent.FILE_RESPONSE, new FageFileResponse(request.sender, request.filepath, data)));
		stream.Close ();
	}

	private void Save(FageFileRequest request) {
		using (FileStream stream = new FileStream (request.filepath, FileMode.OpenOrCreate)) {
			stream.BeginWrite(request.data, 0, request.data.Length, new System.AsyncCallback(OnSave), new FageFileState(request, stream, request.data));
		}
	}

	private	void OnSave(System.IAsyncResult result) {
		FageFileState state		= result.AsyncState as FageFileState;
		FileStream stream		= state.stream;
		FageFileRequest request	= state.request;

		DispatchEvent (new FageEvent(FageEvent.FILE_RESPONSE, new FageFileResponse(request.sender, request.filepath)));
		stream.Close ();
	}
}


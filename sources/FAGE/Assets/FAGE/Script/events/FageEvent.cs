public	class FageEvent {
	public	const string SENSOR_ERROR	= "sensorError";
	public	const string SENSOR_SCREEN	= "sensorScreen";
	public	const string SENSOR_PAUSE	= "sensorPause";
	public	const string SENSOR_RESUME	= "sensorResume";
	public	const string SENSOR_QUIT	= "sensorQuit";
	public	const string SENSOR_ONLINE	= "sensorOnline";
	public	const string SENSOR_OFFLINE	= "sensorOffline";
	public	const string SENSOR_PING	= "sensorPing";
	public	const string WEB_REQUEST	= "webRequest";
	public	const string WEB_RESPONSE	= "webResponse";
	public	const string FILE_REQUEST	= "fileRequest";
	public	const string FILE_RESPONSE	= "fileResponse";
	public	const string AUDIO_REQUEST	= "audioRequest";
	public	const string AUDIO_RESPONSE	= "audioResponse";
	
	private	string _type;
	private	object _data;
	
	public	string type {
		get {
			return _type;
		}
	}
	
	public	object data {
		get {
			return _data;
		}
	}
	
	public	FageEvent(string type, object data = null) {
		_type = type;
		_data = data;
	}
}

public	delegate void FageEventHandler(FageEvent fevent);
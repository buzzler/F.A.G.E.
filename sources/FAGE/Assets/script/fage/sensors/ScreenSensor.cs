using UnityEngine;

public class ScreenSensor : FageEventDispatcher {
	private	DeviceOrientation	_lastOrientation;
	private	int					_lastWidth;
	private	int					_lastHeight;
	private	float				_lastDpi;

	public	DeviceOrientation	orientation { get { return _lastOrientation; } }
	public	int					width		{ get { return _lastWidth; } }
	public	int					height		{ get { return _lastHeight; } }
	public	float				dpi			{ get { return _lastDpi; } }

	private	void DumpInfo() {
		_lastOrientation = Input.deviceOrientation;
		_lastWidth = Screen.width;
		_lastHeight = Screen.height;
		_lastDpi = Screen.dpi;
	}

	void Awake() {
		DumpInfo ();
	}

	void Update () {
		if ((_lastOrientation != Input.deviceOrientation) ||
			(_lastWidth != Screen.width) ||
			(_lastHeight != Screen.height) ||
			(_lastDpi != Screen.dpi)) {
			DumpInfo ();
			DispatchEvent(new FageEvent(FageEvent.SENSOR_SCREEN, this));
		}
	}
}
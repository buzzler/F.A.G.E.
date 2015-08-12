using UnityEngine;

[AddComponentMenu("Fage/Display/FageScreenManager")]
public class FageScreenManager : FageEventDispatcher {
	private	static FageScreenManager _instance;
	public	static FageScreenManager Instance { get { return _instance; } }
	private	DeviceOrientation	_lastOrientation;
	private	int					_lastWidth;
	private	int					_lastHeight;
	private	float				_lastDpi;

	public	DeviceOrientation	orientation { get { return _lastOrientation; } }
	public	int					width		{ get { return _lastWidth; } }
	public	int					height		{ get { return _lastHeight; } }
	public	float				dpi			{ get { return _lastDpi; } }

	void Awake() {
		_instance = this;
		DumpInfo ();
	}

	void Update () {
		if (_lastOrientation != Input.deviceOrientation)
			DispatchEvent (new FageScreenEvent (_lastOrientation, Input.deviceOrientation));

		if ((_lastWidth != Screen.width) || (_lastHeight != Screen.height))
			DispatchEvent (new FageScreenEvent (_lastWidth, _lastHeight, Screen.width, Screen.height));

		if (_lastDpi != Screen.dpi)
			DispatchEvent (new FageScreenEvent (_lastDpi, Screen.dpi));

		DumpInfo ();
	}

	private	void DumpInfo() {
		_lastOrientation = Input.deviceOrientation;
		_lastWidth = Screen.width;
		_lastHeight = Screen.height;
		_lastDpi = Screen.dpi;
	}
}
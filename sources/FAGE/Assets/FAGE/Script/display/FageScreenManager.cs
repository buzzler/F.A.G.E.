using UnityEngine;
using System.Collections;

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
		ArrayList list = new ArrayList ();

		if (_lastOrientation != Input.deviceOrientation)
			list.Add(new FageScreenEvent (_lastOrientation, Input.deviceOrientation));

		if ((_lastWidth != Screen.width) || (_lastHeight != Screen.height))
			list.Add (new FageScreenEvent (_lastWidth, _lastHeight, Screen.width, Screen.height));

		if (_lastDpi != Screen.dpi)
			list.Add (new FageScreenEvent (_lastDpi, Screen.dpi));
		DumpInfo ();
		foreach (FageScreenEvent fsevent in list) {
			DispatchEvent(fsevent);
		}
	}

	private	void DumpInfo() {
		_lastOrientation = Input.deviceOrientation;
		_lastWidth = Screen.width;
		_lastHeight = Screen.height;
		_lastDpi = Screen.dpi;
	}
}
using UnityEngine;
using System.Collections;

public class FageUIInfo {
	private	string		_resourcePathPortrait;
	private	string		_resourcePathLandscape;
	private	Vector2		_position;
	private	Quaternion	_rotation;

	public	string		resourcePathPortrait	{ get { return _resourcePathPortrait; } }
	public	string		resourcePathLandscape	{ get { return _resourcePathLandscape; } }
	public	Vector2		position				{ get { return _position; } }
	public	Quaternion	rotation				{ get { return _rotation; } }

	public	FageUIInfo(string resourcePathPortrait, string resourcePathLandscape) {
		_resourcePathPortrait = resourcePathPortrait;
		_resourcePathLandscape = resourcePathLandscape;
		_position = Vector2.zero;
		_rotation = Quaternion.identity;
	}

	public	FageUIInfo(string resourcePathPortrait, string resourcePathLandscape, Vector2 position, Quaternion rotation) {
		_resourcePathPortrait = resourcePathPortrait;
		_resourcePathLandscape = resourcePathLandscape;
		_position = position;
		_rotation = rotation;
	}

	public	string GetCurrentOrientedResourcePath() {
		switch (FageScreenManager.Instance.orientation) {
		case DeviceOrientation.Portrait:
		case DeviceOrientation.PortraitUpsideDown:
			return resourcePathPortrait;
		case DeviceOrientation.LandscapeLeft:
		case DeviceOrientation.LandscapeRight:
			return resourcePathLandscape;
		default:
			return resourcePathPortrait;
		}
	}
}

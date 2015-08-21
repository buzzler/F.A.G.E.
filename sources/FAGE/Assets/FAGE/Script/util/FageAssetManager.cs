using UnityEngine;
using System.Collections;

public class FageAssetManager : FageStateMachine {
	private	static FageAssetManager _instance;
	public	static FageAssetManager Instance { get { return _instance; } }

	public	TextAsset		setting;

	void Awake() {
		_instance = this;
		FageAssetRoot.LoadFromText (setting.text);
	}
}

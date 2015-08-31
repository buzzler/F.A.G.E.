using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FageBundleLoader : FageStateMachine {
	private	static FageBundleLoader _instance;
	public	static FageBundleLoader Instance { get { return _instance; } }

	public	TextAsset					setting;
	public	bool						flagUpdateBoot;
	public	bool						flagUpdate;
	public	float						expireTime;
	private	float						_timeLastUpdate;
	private	List<string>				_loadedScene;
	private	List<string>				_loadedBundle;
	private	Dictionary<string, object>	_loadedAsset;
	private	Dictionary<string, AssetBundle>	_downloadedBundle;

	void Awake() {
		_instance = this;
		_timeLastUpdate = Time.unscaledTime;
		_loadedScene = new List<string> ();
		_loadedBundle = new List<string> ();
		_loadedAsset = new Dictionary<string, object> ();
		_downloadedBundle = new Dictionary<string, AssetBundle> ();
		FageBundleRoot.LoadFromText (setting.text);

		if (flagUpdateBoot) {
			ReserveState ("FageBundleLoaderCheck");
		}
	}

	public	float GetUpdateTime() {
		return _timeLastUpdate;
	}

	public	void SetUpdateTime() {
		flagUpdate = false;
		_timeLastUpdate = Time.unscaledTime;
	}

	public	void ReserveUpdate() {
		flagUpdate = true;
	}

	public	List<string> GetLoadedScene() {
		return _loadedScene;
	}

	public	List<string> GetLoadedBundles() {
		return _loadedBundle;
	}

	public	Dictionary<string, object> GetLoadedAssets() {
		return _loadedAsset;
	}

	public	Dictionary<string, AssetBundle> GetDownloadedBundles() {
		return _downloadedBundle;
	}

	public	AsyncOperation LoadLevel(string id) {
		if (_loadedScene.Contains (id))
			return Application.LoadLevelAsync (id);
		else
			throw new UnityException ("unknown scene id : " + id);
	}

	public	AsyncOperation LoadLevelAdditive(string id) {
		if (_loadedScene.Contains (id))
			return Application.LoadLevelAdditiveAsync (id);
		else
			throw new UnityException ("unknown scene id : " + id);
	}

	public	object Load(string id) {
		if (_loadedAsset.ContainsKey (id))
			return _loadedAsset [id];
		else
			return Resources.Load (id);
	}

	public	object Load(FageUIDetail uiDetail) {
		return Load (uiDetail.asset, uiDetail.bundle);
	}

	public	object Load(FageUICurtain uiCurtain) {
		return Load (uiCurtain.asset, uiCurtain.bundle);
	}

	public	object Load(string asset, string bundle) {
		if (_loadedAsset.ContainsKey (asset))
			return _loadedAsset [asset];
		else if (_downloadedBundle.ContainsKey (bundle))
			return _downloadedBundle [bundle].LoadAsset (asset);
		else
			return Resources.Load (asset);
	}
}

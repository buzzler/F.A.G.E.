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
	private	List<string>				_loadedBundle;
	private	Dictionary<string, object>	_loadedAsset;
	private	Dictionary<string, AssetBundle>	_downloadedBundle;

	void Awake() {
		_instance = this;
		_timeLastUpdate = Time.unscaledTime;
		_loadedBundle = new List<string>();
		_loadedAsset = new Dictionary<string, object>();
		_downloadedBundle = new Dictionary<string, AssetBundle>();
		FageBundleRoot.LoadFromText(setting.text);

		if (flagUpdateBoot) {
			ReserveState("FageBundleLoaderCheck");
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

	public	List<string> GetLoadedBundles() {
		return _loadedBundle;
	}

	public	Dictionary<string, object> GetLoadedAssets() {
		return _loadedAsset;
	}

	public	Dictionary<string, AssetBundle> GetDownloadedBundles() {
		return _downloadedBundle;
	}

	public	object Load(string id) {
		if (_loadedAsset.ContainsKey (id))
			return _loadedAsset [id];
		else
			return Resources.Load (id);
	}

	public	object Load(FageUIDetail uiDetail) {
		if (_loadedAsset.ContainsKey(uiDetail.id))
			return _loadedAsset[uiDetail.id];
		else if (_downloadedBundle.ContainsKey(uiDetail.resource))
			return _downloadedBundle[uiDetail.resource].LoadAsset(uiDetail.id);
		else
			return Resources.Load(uiDetail.resource);
	}
}

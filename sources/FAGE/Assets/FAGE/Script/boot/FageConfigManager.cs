using UnityEngine;
using System.Collections;

public class FageConfigManager : FageStateMachine {
	public	TextAsset	setting;

	void Awake() {
		FageConfig.LoadFromText(setting.text);
	}

	void Start() {
		FageBundleLoader loader = FageBundleLoader.Instance;
		loader.AddEventListener(FageBundleEvent.CHECK_UPDATE,	OnCheck);
		loader.AddEventListener(FageBundleEvent.DOWNLOADING,	OnDownloading);
		loader.AddEventListener(FageBundleEvent.LOADING,		OnLoading);
		loader.AddEventListener(FageBundleEvent.COMPLETE,		OnComplete);
		loader.ReserveUpdate();
	}

	private	void OnCheck(FageEvent fevent) {
		FageBundleEvent bevent = fevent as FageBundleEvent;
		Debug.Log("OnCheck");
	}

	private	void OnDownloading(FageEvent fevent) {
		FageBundleEvent bevent = fevent as FageBundleEvent;
		Debug.Log("OnDownloading (" + bevent.progress.ToString() + ")");
	}

	private	void OnLoading(FageEvent fevent) {
		FageBundleEvent bevent = fevent as FageBundleEvent;
		Debug.Log("OnLoading");
	}

	private	void OnComplete(FageEvent fevent) {
		FageBundleEvent bevent = fevent as FageBundleEvent;
		Debug.Log("OnComplete");
	}

	private	void OnError(FageEvent fevent) {
		FageBundleEvent bevent = fevent as FageBundleEvent;
		Debug.Log("OnError");
	}
}

using UnityEngine;
using System.Collections;

public class FageBundleLoaderCheck : FageState {
	private	int _requestId;
	private	const string _KEY = "config.xml";

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		FageConnectionManager.Instance.AddEventListener(FageEvent.SENSOR_ONLINE, OnOnline);
		FageConnectionManager.Instance.AddEventListener(FageEvent.SENSOR_OFFLINE, OnOffline);

		if (FageConnectionManager.Instance.IsOnline()) {
			FageWebLoader.Instance.AddEventListener(FageEvent.COMPLETE, OnResponse);
			OnOnline(null);
		} else {
			OnOffline(null);
		}
	}

	private	void OnOnline(FageEvent fevent) {
		_requestId = FageWebLoader.Instance.Request(FageConfig.Instance.url);
		FageBundleLoader.Instance.DispatchEvent (new FageBundleEvent(FageBundleEvent.CHECK_UPDATE));
	}

	private	void OnOffline(FageEvent fevent) {
		_requestId = -1;

		FageBundleLoader loader = FageBundleLoader.Instance;
		if (Utility.HasKey(_KEY)) {
			FageConfig.LoadFromText(Utility.GetPrefString(_KEY));
			loader.ReserveState("FageBundleLoaderDownload");
		} else {
			loader.SetUpdateTime();
			loader.ReserveState("FageBundleLoaderDownload");
		}
	}

	private	void OnResponse(FageEvent fevent) {
		FageWebEvent wevent = fevent as FageWebEvent;
		if ((wevent == null) || (wevent.requestId != _requestId))
			return;

		FageWebLoader.Instance.RemoveEventListener(FageEvent.COMPLETE, OnResponse);
		if (string.IsNullOrEmpty(wevent.www.error)) {
			string str = wevent.www.text;
			FageConfig.LoadFromText(str);
			Utility.SetPrefString(_KEY, str);
			FageBundleLoader.Instance.ReserveState("FageBundleLoaderDownload");
		} else {
			OnOffline(null);
		}
	}

	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
		_requestId = -1;
	}
}

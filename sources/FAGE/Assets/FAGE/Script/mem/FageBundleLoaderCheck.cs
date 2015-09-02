using UnityEngine;
using System.Collections;

public class FageBundleLoaderCheck : FageState {
	private	int _requestId;
	private	const string _KEY = "config.xml";

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		FageWebLoader.Instance.AddEventListener(FageEvent.COMPLETE, OnResponse);
		_requestId = FageWebLoader.Instance.Request(FageConfig.Instance.url);

		stateMachine.DispatchEvent (new FageBundleEvent(FageBundleEvent.CHECK_UPDATE));
	}

	private	void OnResponse(FageEvent fevent) {
		FageWebEvent wevent = fevent as FageWebEvent;
		if ((wevent == null) || (wevent.requestId != _requestId))
			return;

		FageBundleLoader loader = FageBundleLoader.Instance;
		FageWebLoader.Instance.RemoveEventListener(FageEvent.COMPLETE, OnResponse);
		if (string.IsNullOrEmpty(wevent.www.error)) {
			string str = wevent.www.text;
			FageConfig.LoadFromText(str);
			Utility.SetPrefString(_KEY, str);
			loader.ReserveState("FageBundleLoaderDownload");
			Debug.Log(str);
		} else if (Utility.HasKey(_KEY)) {
			FageConfig.LoadFromText(Utility.GetPrefString(_KEY));
			loader.ReserveState("FageBundleLoaderDownload");
		} else {
			loader.SetUpdateTime();
			loader.ReserveState("FageBundleLoaderDownload");
		}
	}

	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
		_requestId = -1;
	}
}

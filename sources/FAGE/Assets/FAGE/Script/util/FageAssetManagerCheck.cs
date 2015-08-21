using UnityEngine;
using System.Collections;

public class FageAssetManagerCheck : FageState {
	int requestId;
	FageStateMachine fsm;

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		FageWebLoader.Instance.AddEventListener(FageEvent.COMPLETE, OnResponse);
		requestId = FageWebLoader.Instance.Request(FageAssetRoot.Instance.url);
		fsm = stateMachine;
	}

	private	void OnResponse(FageEvent fevent) {
		FageWebEvent wevent = fevent as FageWebEvent;
		if ((wevent == null) || (wevent.requestId != requestId))
			return;

		WWW www = wevent.www;
		if ((www != null) && (string.IsNullOrEmpty(www.error))) {
			string xml = www.text;
			FageAssetRoot.LoadFromText(xml);
			Utility.SetPrefString("Assets", xml);
			www.Dispose();
		} else if (Utility.HasKey("Assets")) {
			FageAssetRoot.LoadFromText(Utility.GetPrefString("Assets"));
		}
		fsm.ReserveState("FageAssetManagerLoad");
	}

	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
		requestId = 0;
		fsm = null;
	}
}

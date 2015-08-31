using UnityEngine;
using System.Collections;

public class FageUIManagerLoad : FageState {
	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		FageUIManager manager = stateMachine as FageUIManager;
		FageUIRequest request = manager.GetRequests ().Peek () as FageUIRequest;
		FageUIScene uiScene = request.uiScene;
	}

	private	void OnLoadComplete() {
		FageUIManager.Instance.ReserveState ("FageUIManagerCurtOpen");
	}
}

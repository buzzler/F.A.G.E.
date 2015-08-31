using UnityEngine;
using System.Collections;

public class FageUIManagerLoad : FageState {
	private	AsyncOperation _async;

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		FageUIManager manager = stateMachine as FageUIManager;
		FageUIRequest request = manager.GetRequests ().Peek () as FageUIRequest;
		FageUIScene uiScene = request.uiScene;

		_async = FageBundleLoader.Instance.LoadLevel(uiScene.asset);
	}

	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		FageUIManager manager = stateMachine as FageUIManager;

		if (_async != null) {
			if (_async.isDone)
				manager.ReserveState ("FageUIManagerCurtOpen");
			else
				Debug.Log(_async.progress);
		} else {
			manager.GetRequests().Dequeue();
			manager.ReserveState ("FageUIManagerIdle");
		}
	}

	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
		_async = null;
	}
}

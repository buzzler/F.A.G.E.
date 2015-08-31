using UnityEngine;
using System.Collections;

public class FageUIManagerCurtOpen : FageState {
	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
	}

	private	void OnOpenComplete() {
		FageUIManager.Instance.ReserveState ("FageUIManagerIdle");
	}
}

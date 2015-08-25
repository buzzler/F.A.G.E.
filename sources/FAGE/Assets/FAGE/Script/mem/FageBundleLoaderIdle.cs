using UnityEngine;
using System.Collections;

public class FageBundleLoaderIdle : FageState {
	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		FageBundleLoader loader = stateMachine as FageBundleLoader;
		if (loader.expireTime < (Time.unscaledTime - loader.GetUpdateTime())) {
			loader.ReserveState("FageBundleLoaderCheck");
		} else if (loader.flagUpdate) {
			loader.ReserveState("FageBundleLoaderLoad");
		}
	}
}

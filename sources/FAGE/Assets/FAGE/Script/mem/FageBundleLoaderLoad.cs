using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FageBundleLoaderLoad : FageState {
	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		Debug.Log(this);
		List<string> resources = new List<string>();
		List<string> loadBundles = new List<string>();
		List<string> unloadBundles = new List<string>();

		FageBundleLoader loader = stateMachine as FageBundleLoader;
		FageUIManager manager = FageUIManager.Instance;
		Stack stack = manager.GetStack();
		Queue queue = manager.GetQueue();
		if (stack.Count > 0) {
			FageUIMem mem = stack.Peek() as FageUIMem;
			resources.AddRange(mem.uiSet.GetResources());
		}
		if (queue.Count > 0) {
			FageUIPopupMem mem = queue.Peek() as FageUIPopupMem;
			resources.AddRange(mem.uiSet.GetResources());
		}
		foreach (FageBundle bundle in FageBundleRoot.Instance.bundles) {
			if ((!bundle.isDynamic) && (!loadBundles.Contains(bundle.id))) {
				loadBundles.Add(bundle.id);
			} else if ((resources.Contains(bundle.id)) && (!loadBundles.Contains(bundle.id))) {
				loadBundles.Add(bundle.id);
			}
		}

		foreach (string id in loader.GetLoadedBundles()) {
			if (loadBundles.Contains(id)) {
				loadBundles.Remove(id);
			} else if (!unloadBundles.Contains(id)) {
				unloadBundles.Add(id);
			}
		}

		// for LOAD
//		loadBundles;

		// for UNLOAD
//		unloadBundles;
	}

	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		FageBundleLoader loader = stateMachine as FageBundleLoader;
		if (true) {
			loader.ReserveState("FageBundleLoaderIdle");
			loader.SetUpdateTime();
			loader.DispatchEvent(new FageEvent(FageEvent.COMPLETE));
		}
	}
}

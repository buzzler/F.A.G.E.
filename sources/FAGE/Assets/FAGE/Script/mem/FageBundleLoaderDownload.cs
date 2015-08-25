using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FageBundleLoaderDownload : FageState {
	private	Hashtable _hash;

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		FageWebLoader loader = FageWebLoader.Instance;
		loader.AddEventListener(FageEvent.COMPLETE, OnResponse);
		_hash = new Hashtable();
		foreach (FageBundle bundle in FageBundleRoot.Instance.bundles) {
			_hash.Add(loader.Request(bundle.url, bundle.version), bundle);
		}
	}

	private	void OnResponse(FageEvent fevent) {
		FageWebEvent wevent = fevent as FageWebEvent;
		if ((wevent == null) || (!_hash.ContainsKey(wevent.requestId)))
			return;

		if (string.IsNullOrEmpty(wevent.www.error)) {
			FageBundle bundle = _hash[wevent.requestId] as FageBundle;
			Dictionary<string, AssetBundle> bundles = FageBundleLoader.Instance.GetDownloadedBundles();
			if (bundles.ContainsKey(bundle.id))
				bundles[bundle.id] = wevent.www.assetBundle;
			else
				bundles.Add(bundle.id, wevent.www.assetBundle);
			wevent.www.Dispose();
		}

		_hash.Remove(wevent.requestId);
		if (_hash.Count == 0) {
			FageWebLoader.Instance.RemoveEventListener(FageEvent.COMPLETE, OnResponse);
			FageBundleLoader.Instance.ReserveState("FageBundleLoaderLoad");
		}
	}

	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
	}

	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
		_hash = null;
	}
}
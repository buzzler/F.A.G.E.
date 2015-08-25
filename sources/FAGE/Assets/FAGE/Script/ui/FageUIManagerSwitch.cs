using UnityEngine;
using System.Collections;

public class FageUIManagerSwitch : FageState {
	private	bool _excuted;

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		Debug.Log(this);
		_excuted = false;
		FageBundleLoader.Instance.AddEventListener(FageEvent.COMPLETE, OnLoadBundleComplete);
	}
	
	private	void ExcutePush (FageUIManager manager, FageUIRequest request) {
		Stack stack = manager.GetStack ();

		FageUIMem current = new FageUIMem (request.uiSet);
		stack.Push (current);
		// load bundle
		FageBundleLoader.Instance.ReserveUpdate();
	}

	private	void ExcuteChange (FageUIManager manager, FageUIRequest request) {
		Stack stack = manager.GetStack ();
		if (stack.Count > 1) {
			stack.Pop();
		}

		FageUIMem current = new FageUIMem (request.uiSet);
		stack.Push (current);
		// load bundle
		FageBundleLoader.Instance.ReserveUpdate();
	}


	private	void ExcutePop (FageUIManager manager, FageUIRequest request) {
		Stack stack = manager.GetStack ();
		if (stack.Count > 0) {
			stack.Pop ();
		}
		
		if (stack.Count == 0) {
			manager.GetRequests ().Dequeue ();
			manager.ReserveState ("FageUIManagerIdle");
			return;
		}
		FageUIMem current = stack.Peek () as FageUIMem;
		// load bundle
		FageBundleLoader.Instance.ReserveUpdate();
	}

	private	void ExcutePopup (FageUIManager manager, FageUIRequest request) {
		Queue queue = manager.GetQueue ();
		FageUIPopupMem current = queue.Peek() as FageUIPopupMem;
		// load bundle
		FageBundleLoader.Instance.ReserveUpdate();
	}

	private	void ExcutePopdown (FageUIManager manager, FageUIRequest request) {
		Queue queue = manager.GetQueue ();
		if (queue.Count > 0) {
			queue.Dequeue ();
		}

		if (queue.Count == 0) {
			manager.GetRequests ().Dequeue ();
			manager.ReserveState ("FageUIManagerIdle");
			return;
		}
		FageUIPopupMem current = queue.Peek() as FageUIPopupMem;
		// load bundle
		FageBundleLoader.Instance.ReserveUpdate();
	}
	
	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
			if (!_excuted) {
			FageUIManager manager = stateMachine as FageUIManager;
			Queue queue = manager.GetRequests ();
			if (queue.Count > 0) {
				FageUIRequest request = queue.Peek () as FageUIRequest;
				switch (request.command) {
				case FageUIRequest.CHANGE:
					ExcuteChange(manager, request);
					break;
				case FageUIRequest.PUSH:
					ExcutePush (manager, request);
					break;
				case FageUIRequest.POP:
					ExcutePop(manager, request);
					break;
				case FageUIRequest.POPUP:
					ExcutePopup(manager, request);
					break;
				case FageUIRequest.POPDOWN:
					ExcutePopdown (manager, request);
					break;
				default:
					throw new UnityException ("unkown command");
				}
			} else {
				throw new UnityException ("request lost");
			}
			_excuted = true;
		}
	}

	private	void OnLoadBundleComplete(FageEvent fevent) {
		FageUIManager.Instance.ReserveState("FageUIManagerTransIn");
	}
	
	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
		FageBundleLoader.Instance.RemoveEventListener(FageEvent.COMPLETE, OnLoadBundleComplete);
	}
}

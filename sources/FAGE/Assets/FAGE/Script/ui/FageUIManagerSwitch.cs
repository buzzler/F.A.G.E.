using UnityEngine;
using System.Collections;

public class FageUIManagerSwitch : FageState {
	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		FageUIManager manager = stateMachine as FageUIManager;
		Queue queue = manager.GetRequests ();
		if (queue.Count > 0) {
			FageUIRequest request = queue.Peek () as FageUIRequest;
			switch (request.command) {
			case FageUIRequest.CHANGE:
			case FageUIRequest.PUSH:
				ExcuteUINew (manager, request);
				break;
			case FageUIRequest.POP:
				ExcuteUIResume(manager, request);
				break;
			case FageUIRequest.POPDOWN:
				ExcuteUIPopdown (manager, request);
				break;
			default:
				throw new UnityException ("unkown command");
			}
		} else {
			throw new UnityException ("request lost");
		}
	}
	
	private	void ExcuteUINew (FageUIManager manager, FageUIRequest request) {
		Stack stack = manager.GetStack ();
		if (stack.Count > 0) {
			stack.Pop ();
		}
		
		FageUIMem current = new FageUIMem (request.uiSet);
		stack.Push (current);
		// load bundle
	}

	private	void ExcuteUIResume (FageUIManager manager, FageUIRequest request) {
		Stack stack = manager.GetStack ();
		if (stack.Count > 0) {
			stack.Pop ();
		}
		
		if (stack.Count == 0) {
			manager.GetRequests ().Dequeue ();
			manager.ReserveState ("FageUIIdle");
			return;
		}
		FageUIMem current = stack.Peek () as FageUIMem;
		// load bundle
	}

	private	void ExcuteUIPopdown (FageUIManager manager, FageUIRequest request) {
		Queue queue = manager.GetQueue ();
		if (queue.Count > 0) {
			queue.Dequeue ();
		}

		if (queue.Count == 0) {
			manager.GetRequests ().Dequeue ();
			manager.ReserveState ("FageUIIdle");
			return;
		}
		FageUIPopupMem current = queue.Peek () as FageUIPopupMem;
		// load bundle
	}
	
	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);

		// if load complete, change state to FageUIManagerTransIn
	}
	
	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
	}
}

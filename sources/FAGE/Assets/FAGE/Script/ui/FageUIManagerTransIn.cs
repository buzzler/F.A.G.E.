using UnityEngine;
using System.Collections;

public class FageUIManagerTransIn : FageState {
	private	FageUICommonMem current;

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		FageUIManager manager = stateMachine as FageUIManager;
		Queue queue = manager.GetRequests ();
		if (queue.Count > 0) {
			FageUIRequest request = queue.Peek () as FageUIRequest;
			switch (request.command) {
			case FageUIRequest.CHANGE:
			case FageUIRequest.PUSH:
			case FageUIRequest.POP:
				ExcuteUI (manager, request);
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

	private	void ExcuteUI(FageUIManager manager, FageUIRequest request) {
		Stack stack = manager.GetStack ();
		if (stack.Count > 0) {
			FageUIMem current = stack.Peek () as FageUIMem;
			this.current = current;
			current.Instantiate (manager.canvas, request.param);
		} else {
			manager.GetRequests ().Dequeue ();
			manager.ReserveState ("FageUIIdle");
			return;
		}
	}

	private	void ExcuteUIPopdown(FageUIManager manager, FageUIRequest request) {
		Queue queue = manager.GetQueue ();
		if (queue.Count > 0) {
			FageUIPopupMem current = queue.Peek () as FageUIPopupMem;
			this.current = current;
			current.Instantiate (manager.canvas, request.param);
		} else {
			manager.GetRequests ().Dequeue ();
			manager.ReserveState ("FageUIIdle");
			return;
		}
	}

	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		FageUIManager manager = stateMachine as FageUIManager;
		if (current.state == FageUICommonMem.INTANTIATED) {
			manager.GetRequests ().Dequeue ();
			manager.ReserveState ("FageUIIdle");
		}
	}
	
	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
	}
}

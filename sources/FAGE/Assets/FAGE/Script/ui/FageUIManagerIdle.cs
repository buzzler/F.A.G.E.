using UnityEngine;
using System.Collections;

public class FageUIManagerIdle : FageState {
	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
	}

	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		FageUIManager manager = stateMachine as FageUIManager;
		Queue queue = manager.GetRequests ();
		if (queue.Count > 0) {
			FageUIRequest request = queue.Peek() as FageUIRequest;
			switch(request.command) {
			case FageUIRequest.CHANGE:
			case FageUIRequest.POP:
			case FageUIRequest.PUSH:
				ExcuteUI(manager, request);
				break;
			case FageUIRequest.FLUSH:
				ExcuteFlush(manager, request);
				break;
			case FageUIRequest.POPUP:
				ExcuteUIPopup(manager, request);
				break;
			case FageUIRequest.POPDOWN:
				ExcuteUIPopdown(manager, request);
				break;
			default:
				throw new UnityException("unknown commnad");
			}
		}
	}

	private	void ExcuteUI(FageUIManager manager, FageUIRequest request) {
		Stack stack = manager.GetStack ();
		if (stack.Count > 0) {
			manager.ReserveState("FageUIManagerTransOut");
		} else {
			manager.ReserveState("FageUIManagerSwitch");
		}
	}

	private void ExcuteUIPopup(FageUIManager manager, FageUIRequest request) {
		Queue queue = manager.GetQueue ();
		if (queue.Count == 0) {
			manager.ReserveState("FageUIManagerSwitch");
		} else {
			FageUIPopupMem mem = new FageUIPopupMem (request.uiSet);
			queue.Enqueue(mem);
		}
	}

	private	void ExcuteUIPopdown(FageUIManager manager, FageUIRequest request) {
		Queue queue = manager.GetQueue ();
		if (queue.Count > 0) {
			manager.ReserveState ("FageUIManagerTransOut");
		} else {
			manager.GetRequests ().Dequeue ();
		}
	}

	private void ExcuteFlush(FageUIManager manager, FageUIRequest request) {
		Stack stack = manager.GetStack ();
		FageUIMem current = stack.Peek () as FageUIMem;
		stack.Clear ();
		stack.Push (current);

		manager.GetRequests ().Dequeue ();
	}

	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
	}
}

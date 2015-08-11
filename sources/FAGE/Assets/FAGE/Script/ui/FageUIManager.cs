using UnityEngine;
using System.Collections;

public class FageUIManager : FageEventDispatcher {
	public	Transform canvas;
	private	Stack _stack;
	private	Queue _queue;

	void Awake() {
		_stack = new Stack ();
		_queue = new Queue ();
	}

	void OnEnable() {
		AddEventListener(FageEvent.UI_CHANGE,	OnUIChange);
		AddEventListener(FageEvent.UI_INDEPTH,	OnUIIndepth);
		AddEventListener(FageEvent.UI_OUTDEPTH,	OnUIOutdepth);
		AddEventListener(FageEvent.UI_FLUSH,	OnUIFlush);
		AddEventListener(FageEvent.UI_POPUP,	OnPopup);
		AddEventListener(FageEvent.UI_POPDOWN,	OnPopdown);
	}

	void OnDisable() {
		RemoveEventListener(FageEvent.UI_CHANGE,	OnUIChange);
		RemoveEventListener(FageEvent.UI_INDEPTH,	OnUIIndepth);
		RemoveEventListener(FageEvent.UI_OUTDEPTH,	OnUIOutdepth);
		RemoveEventListener(FageEvent.UI_FLUSH,		OnUIFlush);
		RemoveEventListener(FageEvent.UI_POPUP,		OnPopup);
		RemoveEventListener(FageEvent.UI_POPDOWN,	OnPopdown);
	}

	private	void OnUIChange(FageEvent fevent) {
		if (_stack.Count != 0) {
			FageUIMem before = _stack.Pop() as FageUIMem;
			before.Destroy();
		}

		FageUIParam param = fevent.data as FageUIParam;
		FageUIMem after = new FageUIMem(param.resourcePath);
		_stack.Push(after);
		after.Instantiate(canvas, param.position, param.rotation, param.param);
	}

	private	void OnUIIndepth(FageEvent fevent) {
		if (_stack.Count != 0) {
			FageUIMem before = _stack.Peek() as FageUIMem;
			before.Pause();
		}

		FageUIParam param = fevent.data as FageUIParam;
		FageUIMem after = new FageUIMem(param.resourcePath);
		_stack.Push(after);
		after.Instantiate(canvas, param.position, param.rotation, param.param);
	}

	private	void OnUIOutdepth(FageEvent fevent) {
		if (_stack.Count > 0) {
			FageUIMem before = _stack.Pop() as FageUIMem;
			before.Destroy();
		}

		if (_stack.Count > 0) {
			FageUIParam param = fevent.data as FageUIParam;
			FageUIMem after = _stack.Peek() as FageUIMem;
			after.Resume(canvas, param.param);
		}
	}

	private	void OnUIFlush(FageEvent fevent) {
		while (_stack.Count > 0) {
			FageUIMem before = _stack.Pop() as FageUIMem;
			before.Destroy();
		}
		
		FageUIParam param = fevent.data as FageUIParam;
		FageUIMem after = new FageUIMem(param.resourcePath);
		_stack.Push(after);
		after.Instantiate(canvas, param.position, param.rotation, param.param);
	}

	private	void OnPopup(FageEvent fevent) {
		FageUIParam param = fevent.data as FageUIParam;
		FagePopupMem after = new FagePopupMem(param.resourcePath, param);
		_queue.Enqueue(after);

		if (_queue.Count == 1) {
			after.Instantiate(canvas);
		}
	}

	private	void OnPopdown(FageEvent fevent) {
		if (_queue.Count > 0) {
			FagePopupMem before = _queue.Dequeue () as FagePopupMem;
			before.Destroy ();
		}

		if (_queue.Count > 0) {
			FagePopupMem after = _queue.Peek () as FagePopupMem;
			after.Instantiate(canvas);
		}
	}
}
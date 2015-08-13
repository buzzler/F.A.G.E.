using UnityEngine;
using System.Collections;

public class FageUIManager : FageEventDispatcher {
	private	static FageUIManager _instance;
	public	static FageUIManager Instance { get { return _instance; } }
	public	Transform canvas;
	private	Stack _stack;
	private	Queue _queue;
	
	void Awake() {
		_instance = this;
		_stack = new Stack ();
		_queue = new Queue ();
	}

	public	void Change(FageUIInfo uiInfo, params object[] param) {
		if (_stack.Count > 0) {
			FageUIMem before = _stack.Pop () as FageUIMem;
			before.Destroy ();
		}

		FageUIMem after = new FageUIMem (uiInfo);
		_stack.Push (after);
		after.Instantiate (canvas, param);
	}

	public	void Push(FageUIInfo uiInfo, params object[] param) {
		if (_stack.Count > 0) {
			FageUIMem before = _stack.Peek() as FageUIMem;
			before.Pause();
		}

		FageUIMem after = new FageUIMem (uiInfo);
		_stack.Push (after);
		after.Instantiate (canvas, param);
	}

	public	void Pop(params object[] param) {
		if (_stack.Count > 0) {
			FageUIMem before = _stack.Pop() as FageUIMem;
			before.Destroy();
		}

		if (_stack.Count > 0) {
			FageUIMem after = _stack.Peek() as FageUIMem;
			after.Resume(canvas, param);
		}
	}

	public	void Flush() {
		if (_stack.Count > 1) {
			FageUIMem now = _stack.Peek() as FageUIMem;
			_stack.Clear();
			_stack.Push(now);
		}
	}

	public	void Popup(FageUIInfo uiInfo, params object[] param) {
		FageUIPopupMem after = new FageUIPopupMem (uiInfo);
		_queue.Enqueue(after);

		if (_queue.Count == 1) {
			after.Instantiate(canvas, param);
		}
	}

	public	void Popdown(params object[] param) {
		if (_queue.Count > 0) {
			FageUIPopupMem before = _queue.Dequeue () as FageUIPopupMem;
			before.Destroy ();
		}

		if (_queue.Count > 0) {
			FageUIPopupMem after = _queue.Peek () as FageUIPopupMem;
			after.Instantiate(canvas, param);
		}
	}
}
using UnityEngine;
using System.Collections;

public class FageUIManager : FageEventDispatcher {
	private	static FageUIManager _instance;
	public	static FageUIManager Instance { get { return _instance; } }
	public	Transform		 canvas;
	public	TextAsset		 setting;
	private	Stack			 _stack;
	private	Queue			 _queue;
	private	FageUIMem		_delayedMem;
	private	FageUIPopupMem	_delayedPopupMem;
	private	object[]		_delayedParam;

	void Awake() {
		_instance = this;
		_stack = new Stack ();
		_queue = new Queue ();

		FageUIRoot.LoadFromText (setting.text);
	}

	public	void Change(FageUISet uiSet, params object[] param) {
		FageUIMem before = null;
		if (_stack.Count > 0) {
			before = _stack.Pop () as FageUIMem;
		}

		FageUIMem after = new FageUIMem (uiSet);
		_stack.Push (after);

		if (before == null) {
			after.Instantiate (canvas, param);
		} else {
			float trans = before.Destroy ();
			if (trans > 0) {
				_delayedMem = after;
				_delayedParam = param;
				Invoke ("DelayedInstantiate", trans);
			} else {
				after.Instantiate (canvas, param);
			}
		}
	}

	private	void DelayedInstantiate() {
		_delayedMem.Instantiate (canvas, _delayedParam);
		_delayedMem = null;
		_delayedParam = null;
	}

	public	void Push(FageUISet uiSet, params object[] param) {
		FageUIMem before = null;
		if (_stack.Count > 0) {
			before = _stack.Peek() as FageUIMem;
		}

		FageUIMem after = new FageUIMem (uiSet);
		_stack.Push (after);

		if (before == null) {
			after.Instantiate (canvas, param);
		} else {
			float trans = before.Pause ();
			if (trans > 0) {
				_delayedMem = after;
				_delayedParam = param;
				Invoke ("DelayedInstantiate", trans);
			} else {
				after.Instantiate (canvas, param);
			}
		}
	}

	public	void Pop(params object[] param) {
		FageUIMem before = null;
		if (_stack.Count > 0) {
			before = _stack.Pop() as FageUIMem;
		}

		FageUIMem after = null;
		if (_stack.Count > 0) {
			after = _stack.Peek() as FageUIMem;
		}

		if (before == null) {
			if (after != null) {
				after.Resume(canvas, param);
			}
		} else {
			float trans = before.Destroy();
			if (trans > 0) {
				_delayedMem = after;
				_delayedParam = param;
				Invoke("DelayedResume", trans);
			} else if (after != null) {
				after.Resume(canvas, param);
			}
		}
	}

	private	void DelayedResume() {
		if (_delayedMem != null) {
			_delayedMem.Resume (canvas, _delayedParam);
			_delayedMem = null;
		}
		_delayedParam = null;
	}

	public	void Flush() {
		if (_stack.Count > 1) {
			FageUIMem now = _stack.Peek() as FageUIMem;
			_stack.Clear();
			_stack.Push(now);
		}
	}

	public	void Popup(FageUISet uiSet, params object[] param) {
		FageUIPopupMem after = new FageUIPopupMem (uiSet);
		_queue.Enqueue(after);

		if (_queue.Count == 1) {
			after.Instantiate(canvas, param);
		}
	}

	public	void Popdown(params object[] param) {
		FageUIPopupMem before = null;
		if (_queue.Count > 0) {
			before = _queue.Dequeue () as FageUIPopupMem;
		}

		FageUIPopupMem after = null;
		if (_queue.Count > 0) {
			after = _queue.Peek () as FageUIPopupMem;
		}

		if (before == null) {
			if (after != null) {
				after.Instantiate (canvas, param);
			}
		} else {
			float trans = before.Destroy ();
			if (trans > 0) {
				_delayedPopupMem = after;
				_delayedParam = param;
				Invoke("DelayedPopupInstantiate", trans);
			} else if (after != null) {
				after.Instantiate (canvas, param);
			}
		}
	}

	private	void DelayedPopupInstantiate() {
		if (_delayedPopupMem != null) {
			_delayedPopupMem.Instantiate (canvas, _delayedParam);
			_delayedPopupMem = null;
		}
		_delayedParam = null;
	}
}
using UnityEngine;
using System.Collections;

public class FageUIManager : FageStateMachine {
	private	static FageUIManager _instance;
	public	static FageUIManager Instance { get { return _instance; } }
	public	Transform		canvas;
	public	TextAsset		setting;
	private	Stack			_stackUI;
	private	Queue			_queueUIPopup;
	private	Queue			_queueRequest;

	private	FageUIMem		_delayedMem;
	private	FageUIPopupMem	_delayedPopupMem;
	private	object[]		_delayedParam;

	void Awake() {
		_instance = this;
		_stackUI = new Stack ();
		_queueUIPopup = new Queue ();
		_queueRequest = new Queue ();

		FageUIRoot.LoadFromText (setting.text);
	}

	public	Queue GetRequests() {
		return _queueRequest;
	}

	public	Stack GetStack() {
		return _stackUI;
	}

	public	Queue GetQueue() {
		return _queueUIPopup;
	}

	public	void Change(FageUISet uiSet, params object[] param) {
		_queueRequest.Enqueue(new FageUIRequest(FageUIRequest.CHANGE, uiSet, param));
	}

	public	void Push(FageUISet uiSet, params object[] param) {
		_queueRequest.Enqueue(new FageUIRequest(FageUIRequest.PUSH, uiSet, param));
	}

	public	void Pop(params object[] param) {
		_queueRequest.Enqueue(new FageUIRequest(FageUIRequest.POP, param));
	}

	public	void Flush() {
		_queueRequest.Enqueue(new FageUIRequest(FageUIRequest.FLUSH));
	}

	public	void Popup(FageUISet uiSet, params object[] param) {
		_queueRequest.Enqueue(new FageUIRequest(FageUIRequest.POPUP, uiSet, param));
	}

	public	void Popdown(params object[] param) {
		_queueRequest.Enqueue(new FageUIRequest(FageUIRequest.POPDOWN, param));
	}
/*
	public	void Change(FageUISet uiSet, params object[] param) {
		FageUIMem before = null;
		if (_stackUI.Count > 0) {
			before = _stackUI.Pop () as FageUIMem;
		}

		FageUIMem after = new FageUIMem (uiSet);
		_stackUI.Push (after);

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
		if (_stackUI.Count > 0) {
			before = _stackUI.Peek() as FageUIMem;
		}

		FageUIMem after = new FageUIMem (uiSet);
		_stackUI.Push (after);

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
		if (_stackUI.Count > 0) {
			before = _stackUI.Pop() as FageUIMem;
		}

		FageUIMem after = null;
		if (_stackUI.Count > 0) {
			after = _stackUI.Peek() as FageUIMem;
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
		if (_stackUI.Count > 1) {
			FageUIMem now = _stackUI.Peek() as FageUIMem;
			_stackUI.Clear();
			_stackUI.Push(now);
		}
	}

	public	void Popup(FageUISet uiSet, params object[] param) {
		FageUIPopupMem after = new FageUIPopupMem (uiSet);
		_queueUIPopup.Enqueue(after);

		if (_queueUIPopup.Count == 1) {
			after.Instantiate(canvas, param);
		}
	}

	public	void Popdown(params object[] param) {
		FageUIPopupMem before = null;
		if (_queueUIPopup.Count > 0) {
			before = _queueUIPopup.Dequeue () as FageUIPopupMem;
		}

		FageUIPopupMem after = null;
		if (_queueUIPopup.Count > 0) {
			after = _queueUIPopup.Peek () as FageUIPopupMem;
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
*/
}
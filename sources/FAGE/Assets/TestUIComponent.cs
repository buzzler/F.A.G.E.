using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestUIComponent : MonoBehaviour, IFageUIComponent {
	private	static int i = 0;

	public	Text textLabel;

	public	void OnUIInstantiate(FageUIMem mem, params object[] param) {
		i++;
		mem.SetInt("id", i);
		textLabel.text = i.ToString();
	}

	public	void OnUIDestroy(FageUIMem mem) {
	}

	public	void OnSwitchIn(FageUIMem mem) {
	}
	
	public	void OnSwitchOut(FageUIMem mem) {
	}

	public	void OnUIPause(FageUIMem mem) {

	}

	public	void OnUIResume(FageUIMem mem, params object[] param) {
		int id = mem.GetInt("id");
		textLabel.text = id.ToString();
	}

	public	GameObject	GetGameObject() {
		return gameObject;
	}

	public	void OnClickPopup() {
		FageUISet uiSet = FageUIRoot.Instance.FindUISet ("testpop");
		FageUIManager.Instance.Popup (uiSet);
	}

	public	void OnClickIndepth() {
		FageUISet uiSet = FageUIRoot.Instance.FindUISet ("testui");
		FageUIManager.Instance.Push (uiSet);
	}

	public	void OnClickOutdepth() {
		FageUIManager.Instance.Pop ();
	}

	public	void OnClickChange() {
		FageUISet uiSet = FageUIRoot.Instance.FindUISet ("testui");
		FageUIManager.Instance.Change (uiSet);
	}
}

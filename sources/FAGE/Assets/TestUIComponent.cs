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
		FageUIInfo uiInfo = new FageUIInfo ("ui/popPortrait", "ui/popLandscape");
		FageUIManager.Instance.Popup (uiInfo);
	}

	public	void OnClickIndepth() {
		FageUIInfo uiInfo = new FageUIInfo ("ui/uiPortrait", "ui/uiLandscape");
		FageUIManager.Instance.Push (uiInfo);
	}

	public	void OnClickOutdepth() {
		FageUIManager.Instance.Pop ();
	}

	public	void OnClickChange() {
		FageUIInfo uiInfo = new FageUIInfo ("ui/uiPortrait", "ui/uiLandscape");
		FageUIManager.Instance.Change (uiInfo);
	}
}

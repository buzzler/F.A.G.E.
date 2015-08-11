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
		FageUIParam param = new FageUIParam("ui/popuptest", "Hello", "World", "POPUP!");
		FageEvent fevent = new FageEvent(FageEvent.UI_POPUP, param);
		FageUIManager.Instance.DispatchEvent(fevent);
	}

	public	void OnClickIndepth() {
		FageUIParam param = new FageUIParam("ui/uitest", "Hello", "World", "POPUP!");
		FageEvent fevent = new FageEvent(FageEvent.UI_INDEPTH, param);
		FageUIManager.Instance.DispatchEvent(fevent);
	}

	public	void OnClickOutdepth() {
		FageUIParam param = new FageUIParam("ui/uitest", "Hello", "World", "BACK!");
		FageEvent fevent = new FageEvent(FageEvent.UI_OUTDEPTH, param);
		FageUIManager.Instance.DispatchEvent(fevent);
	}

	public	void OnClickChange() {
		FageUIParam param = new FageUIParam("ui/uitest", "Hello", "World", "CHANGE!");
		FageEvent fevent = new FageEvent(FageEvent.UI_CHANGE, param);
		FageUIManager.Instance.DispatchEvent(fevent);
	}
}

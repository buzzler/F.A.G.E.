using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestPopupComponent : MonoBehaviour, IFageUIPopupComponent {
	private static int i = 0;
	public	Text textTitle;

	public	void OnUIInstantiate(FageUIPopupMem mem, params object[] param) {
		i++;
		mem.SetInt("id", i);
		textTitle.text = i.ToString();
	}

	public	void OnUIDestroy(FageUIPopupMem mem) {
	}

	public	void OnSwitchIn(FageUIPopupMem mem) {
	}

	public	void OnSwitchOut(FageUIPopupMem mem) {
	}

	public	GameObject GetGameObject() {
		return gameObject;
	}

	public	void OnClickPopup() {
		FageUIInfo uiInfo = new FageUIInfo ("ui/popPortrait", "ui/popLandscape");
		FageUIManager.Instance.Popup (uiInfo);
		FageUIManager.Instance.Popup (uiInfo);
		FageUIManager.Instance.Popup (uiInfo);
	}

	public	void OnClickClose() {
		FageUIManager.Instance.Popdown ();
	}
}

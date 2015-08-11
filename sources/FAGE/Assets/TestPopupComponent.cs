using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestPopupComponent : MonoBehaviour, IFagePopupComponent {
	private static int i = 0;
	public	Text textTitle;

	public	void OnUIInstantiate(FagePopupMem mem, params object[] param) {
		i++;
		mem.SetInt("id", i);
		textTitle.text = i.ToString();
	}

	public	void OnUIDestroy(FagePopupMem mem) {
	}

	public	GameObject GetGameObject() {
		return gameObject;
	}

	public	void OnClickPopup() {
		FageUIParam param = new FageUIParam("ui/popuptest");
		FageUIManager.Instance.DispatchEvent(new FageEvent(FageEvent.UI_POPUP, param));
		FageUIManager.Instance.DispatchEvent(new FageEvent(FageEvent.UI_POPUP, param));
		FageUIManager.Instance.DispatchEvent(new FageEvent(FageEvent.UI_POPUP, param));
	}

	public	void OnClickClose() {
		FageUIManager.Instance.DispatchEvent(new FageEvent(FageEvent.UI_POPDOWN));
	}
}

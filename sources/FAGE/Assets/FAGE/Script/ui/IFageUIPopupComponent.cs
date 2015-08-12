using UnityEngine;

public	interface IFageUIPopupComponent {
	void		OnUIInstantiate(FageUIPopupMem mem, params object[] param);
	void		OnUIDestroy(FageUIPopupMem mem);
	GameObject	GetGameObject();
}
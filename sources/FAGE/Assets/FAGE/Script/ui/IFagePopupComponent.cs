using UnityEngine;

public	interface IFagePopupComponent {
	void		OnUIInstantiate(FagePopupMem mem, params object[] param);
	void		OnUIDestroy(FagePopupMem mem);
	GameObject	GetGameObject();
}
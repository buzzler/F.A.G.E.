using UnityEngine;

public	interface IFageUIComponent {
	void		OnUIInstantiate(FageUIMem mem, params object[] param);
	void		OnUIDestroy(FageUIMem mem);
	void		OnUIPause(FageUIMem mem);
	void		OnUIResume(FageUIMem mem, params object[] param);
	GameObject	GetGameObject();
}
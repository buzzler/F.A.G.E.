using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestCurtainComponent : MonoBehaviour, IFageUICurtainComponent {
	public	Image imageCurtain;
	public	Text textProgress;

	public	void StartOpen(System.Action callback) {
		Color temp = imageCurtain.color;
		temp.a = 1f;
		imageCurtain.color = temp;
		LeanTween.alpha (imageCurtain.rectTransform, 0f, 0.7f).setEase (LeanTweenType.easeOutCubic).setOnComplete (callback);
	}

	public	void StartClose(System.Action callback) {
		Color temp = imageCurtain.color;
		temp.a = 0f;
		imageCurtain.color = temp;
		LeanTween.alpha (imageCurtain.rectTransform, 1f, 0.5f).setEase (LeanTweenType.easeInCubic).setOnComplete (callback);
	}

	public	void SetProgress(float progress) {
		textProgress.text = ((int)(progress * 100f)).ToString () + "%";
	}

	public	GameObject GetGameObject() {
		return gameObject;
	}
}

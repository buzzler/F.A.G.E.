using UnityEngine;
using System.Collections;

public class Test : FageEventDispatcher {
	private	FageAudioSourceControl control;

	void OnEnable() {
	}

	void OnDisable() {
	}

	void OnGUI() {
		if (GUI.Button (new Rect (0,0,Screen.width/2, Screen.height/2), "REQUEST1")) {
			if (control!=null) {
				control = null;
			}

			FageAudioManagerEx.Instance.Play("background", "assets/bundle/audio/bgm_lunar.wav", ref control, true);
		}
		if (GUI.Button (new Rect (Screen.width/2,0,Screen.width/2, Screen.height/2), "REQUEST2")) {
			FageAudioManagerEx.Instance.Play("coin", "assets/bundle/audio/fx_coin.wav");

			FageUIManager.Instance.Change(FageUIRoot.Instance.FindUISet("testui"));
			gameObject.SetActive(false);
		}
	}

	public	void OnClick1() {
		FageUIManager.Instance.Change (FageUIRoot.Instance.FindUISet("test1"));
	}

	public	void OnClick2() {
		FageUIManager.Instance.Change (FageUIRoot.Instance.FindUISet("test2"));
	}

	public	void OnClick3() {
		FageUIManager.Instance.Change (FageUIRoot.Instance.FindUISet("test3"));
	}
}
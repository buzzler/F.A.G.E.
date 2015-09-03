using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FageBootStraper : MonoBehaviour {
	public	Text	textMessage;
	public	Slider	sliderPercentage;
	public	string	firstScene;
	public	string	firstCurtain;
	private	float	_target;
	private	float	_step;
	private	bool	_animate;

	void Awake() {
		textMessage.text = "";
		sliderPercentage.value = 0f;
		_target = 0f;
		_step = 0.01f;
		_animate = false;
	}

	void Start() {
		FageBundleLoader loader = FageBundleLoader.Instance;
		loader.AddEventListener(FageBundleEvent.CHECK_UPDATE,	OnCheck);
		loader.AddEventListener(FageBundleEvent.DOWNLOADING,	OnDownloading);
		loader.AddEventListener(FageBundleEvent.LOADING,		OnLoading);
		loader.AddEventListener(FageBundleEvent.COMPLETE,		OnComplete);
		loader.AddEventListener(FageBundleEvent.ERROR_NODATA,	OnError);
		loader.ReserveUpdate();
	}

	void Update() {
		if (_animate) {
			return;
		}

		if (sliderPercentage.value >= 1) {
			if ((string.IsNullOrEmpty(firstScene)!=true) && (string.IsNullOrEmpty(firstCurtain)!=true)) {
				LeanTween.scale(transform as RectTransform, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(OnAnimationComplete);
				_animate = true;
			}
		} else if (sliderPercentage.value != _target) {
			float temp = _target - sliderPercentage.value;
			temp = Mathf.Min(temp, _step);
			sliderPercentage.value += temp;
		}
	}

	private	void OnAnimationComplete() {
		RemoveListeners();
		FageUIManager.Instance.Level(firstScene, FageUIRoot.Instance.FindUICurtain(firstCurtain));
		Destroy(gameObject);
	}

	private	void OnCheck(FageEvent fevent) {
		textMessage.text = "checking";
	}
	
	private	void OnDownloading(FageEvent fevent) {
		textMessage.text = "downloading";
		_target = (fevent as FageBundleEvent).progress;
	}
	
	private	void OnLoading(FageEvent fevent) {
		textMessage.text = "loading";
	}
	
	private	void OnComplete(FageEvent fevent) {
		textMessage.text = "complete";
		_target = 1f;
	}
	
	private	void OnError(FageEvent fevent) {
		textMessage.text = "data error";
		Invoke("OnExit", 3f);
	}

	private	void RemoveListeners() {
		FageBundleLoader loader = FageBundleLoader.Instance;
		loader.RemoveEventListener(FageBundleEvent.CHECK_UPDATE,OnCheck);
		loader.RemoveEventListener(FageBundleEvent.DOWNLOADING,	OnDownloading);
		loader.RemoveEventListener(FageBundleEvent.LOADING,		OnLoading);
		loader.RemoveEventListener(FageBundleEvent.COMPLETE,	OnComplete);
		loader.RemoveEventListener(FageBundleEvent.ERROR_NODATA,OnError);
	}

	private	void OnExit() {
		RemoveListeners();
		Application.Quit();
	}
}

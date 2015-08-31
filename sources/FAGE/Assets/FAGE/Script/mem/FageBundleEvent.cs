public class FageBundleEvent : FageEvent {
	public	const string CHECK_UPDATE	= "checkUpdate";
	public	const string DOWNLOADING	= "downloading";
	public	const string LOADING		= "loading";

	private	float _progress;
	public	float progress	{ get { return _progress; } }

	public	FageBundleEvent(string type, float progress = 0f) : base(type) {
		_progress = progress;
	}
}

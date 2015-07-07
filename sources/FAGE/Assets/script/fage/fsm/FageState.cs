public	class FageState {
	private string _id;
	
	public	string id {
		get {
			return _id;
		}
	}
	
	public FageState() {
		_id = GetType().FullName;
	}
	
	public	virtual	void BeforeSwitch(FageStateMachine stateMachine, string afterId = null) {
		FageEventDispatcher.Log (stateMachine.id + "/" + id + ": BeforeSwitch");
	}
	
	public	virtual	void AfterSwitch(FageStateMachine stateMachine, string beforeId = null) {
		FageEventDispatcher.Log (stateMachine.id + "/" + id + ": AfterSwitch");
	}
	
	public	virtual	void Excute(FageStateMachine stateMachine) {
	}
}
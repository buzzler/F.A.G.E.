using UnityEngine;

public class ConnectionSensor : FageStateMachine {
	private	const int		_MAX_QUEUE = 50;
	private	FageMessage[]	_messages;
	private int				_index_push;
	private int				_index_pop;

	void Awake() {
		_messages	= new FageMessage[_MAX_QUEUE];
		_index_push	= 0;
		_index_pop	= 0;

	}

	public	override void SendMessage(FageMessage message) {
		if (((_index_push+1)%_MAX_QUEUE)==_index_pop) {
			throw new UnityException ();
		}

		_messages [_index_push] = message;
		_index_push = (_index_push + 1) % _MAX_QUEUE;
	}

	public	int GetMessageCount() {
		if (_index_push >= _index_pop) {
			return _index_push - _index_pop;
		} else {
			return (_index_push + _MAX_QUEUE) - _index_pop;
		}
	}

	public	FageMessage PopMessage() {
		if (_index_pop == _index_push) {
			return null;
		}

		FageMessage result = _messages [_index_pop];
		_messages [_index_pop] = null;
		_index_pop = (_index_pop + 1) % _MAX_QUEUE;
		return result;
	}
}

using System.Collections.Generic;

namespace ToolBox.UtilityAI
{
	public class Agent
	{
		private readonly List<State> _states = new();
		private State _currentState;

		public void Update()
		{
			var nextState = GetNextState();

			if (nextState != _currentState)
			{
				_currentState?.Exit();
				_currentState = nextState;
				_currentState?.Enter();
			}

			_currentState?.Execute();
		}

		public Agent AddState(State state)
		{
			if (!_states.Contains(state))
				_states.Add(state);

			return this;
		}

		public Agent RemoveState(State state)
		{
			if (!_states.Contains(state))
				return this;

			if (_currentState == state)
			{
				_currentState.Exit();
				_currentState = null;
			}

			_states.Remove(state);

			return this;
		}

		public bool GetState<T>(out T state) where T : State
		{
			state = null;

			foreach (var value in _states)
			{
				if (value is not T correctState)
					continue;

				state = correctState;
				return true;
			}

			return false;
		}

		public bool RemoveState<T>() where T : State
		{
			bool contains = GetState<T>(out var state);

			if (!contains)
				return false;

			RemoveState(state);
			return true;
		}

		private State GetNextState()
		{
			State nextState = null;
			int bestScore = 0;
			int count = _states.Count - 1;

			for (int i = count; i >= 0; i--)
			{
				var state = _states[i];
				int score = state.GetTotalScore();

				if (score <= bestScore)
					continue;

				nextState = state;
				bestScore = score;
			}

			return nextState;
		}
	}
}
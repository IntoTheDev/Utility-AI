using System.Collections.Generic;

namespace ToolBox.UtilityAI
{
	public class State : IAction
	{
		private readonly List<IScorer> _scorers = new();
		private readonly List<IAction> _actions = new();

		public int GetTotalScore()
		{
			int totalScore = 0;
			int count = _scorers.Count - 1;

			for (int i = count; i >= 0; i--)
			{
				var scorer = _scorers[i];

				if (scorer.IsMatch())
					totalScore += scorer.Score;
			}

			return totalScore;
		}

		public void Enter()
		{
			foreach (var scorer in _scorers)
				scorer.Enter();

			foreach (var action in _actions)
				action.Enter();
		}

		public void Execute()
		{
			foreach (var action in _actions)
				action.Execute();
		}

		public void Exit()
		{
			foreach (var scorer in _scorers)
				scorer.Exit();

			foreach (var action in _actions)
				action.Exit();
		}

		public State AddScorer(IScorer scorer)
		{
			if (!_scorers.Contains(scorer))
				_scorers.Add(scorer);

			return this;
		}

		public State RemoveScorer(IScorer scorer)
		{
			if (_scorers.Contains(scorer))
				_scorers.Remove(scorer);

			return this;
		}

		public bool GetScorer<T>(out T scorer) where T : class, IScorer
		{
			scorer = null;

			foreach (var value in _scorers)
			{
				if (value is not T correctScorer)
					continue;

				scorer = correctScorer;
				return true;
			}

			return false;
		}

		public bool RemoveScorer<T>() where T : class, IScorer
		{
			bool contains = GetScorer<T>(out var scorer);

			if (!contains)
				return false;

			RemoveScorer(scorer);
			return true;
		}

		public State AddAction(IAction action)
		{
			if (!_actions.Contains(action))
				_actions.Add(action);

			return this;
		}

		public State RemoveAction(IAction action)
		{
			if (_actions.Contains(action))
				_actions.Remove(action);

			return this;
		}

		public bool GetAction<T>(out T action) where T : class, IAction
		{
			action = null;

			foreach (var value in _actions)
			{
				if (value is not T correctAction)
					continue;

				action = correctAction;
				return true;
			}

			return false;
		}

		public bool RemoveAction<T>() where T : class, IAction
		{
			bool contains = GetAction<T>(out var action);

			if (!contains)
				return false;

			RemoveAction(action);
			return true;
		}
	}
}
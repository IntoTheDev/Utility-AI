namespace ToolBox.UtilityAI
{
	public interface IScorer
	{
		public int Score { get; set; }

		public void Enter();
		public bool IsMatch();
		public void Exit();
	}
}
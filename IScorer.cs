namespace ToolBox.UtilityAI
{
	public interface IScorer
	{
		public int Score { get; set; }

		public bool IsMatch();
	}
}
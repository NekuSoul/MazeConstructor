namespace Game.Code.Enums
{
	public enum MazeTileResult
	{
		Unreachable,
		Reachable,
		VisibleDeadEnd,
		DeadEnd,
		Loop,
		ShortestPath
	}
}
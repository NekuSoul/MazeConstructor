using Game.Code.Enums;

namespace Game.Code
{
	public class MazeAnalyzerResult
	{
		public bool Solvable;
		public int ShortestPath;
		public int Rating;
		public MazeTileResult[,] MazeTileClassifications;
	}
}
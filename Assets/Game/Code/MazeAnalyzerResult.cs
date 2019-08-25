using Game.Code.Enums;

namespace Game.Code
{
	public class MazeAnalyzerResult
	{
		public bool Solvable;
		public int Rating;
		public MazeTileClassification[,] MazeTileClassifications;
	}
}
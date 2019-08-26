using System.Collections.Generic;
using System.Linq;
using Game.Code.Enums;
using Game.Scripts;

namespace Game.Code
{
	public static class MazeAnalzyer
	{
		public static MazeAnalyzerResult AnalyzeMaze(Maze maze)
		{
			var mazeAnalyzerResult = new MazeAnalyzerResult();

			var tiles = maze.MazeTiles;

			var entranceTile = tiles.Cast<MazeTile>().First(tile => tile.specialTile == TileType.Entrance);

			MazeTileResult[,] tileResults = new MazeTileResult[maze.Width, maze.Height];

			List<AnalyzerStatus> nextAnalyzerStatuses = new List<AnalyzerStatus> {new AnalyzerStatus(entranceTile)};

			while (nextAnalyzerStatuses.Count > 0)
			{
				var currentAnalyzerStatus = nextAnalyzerStatuses.ToArray();

				nextAnalyzerStatuses.Clear();

				foreach (var analyzerStatus in currentAnalyzerStatus)
				{
					var currentTile = analyzerStatus.MazeTile;
					var directions = analyzerStatus.MazeTile.openDirections;

					// Check if we found the end of the maze.
					if (currentTile.specialTile == TileType.Exit)
					{
						mazeAnalyzerResult.ShortestPath = analyzerStatus.Steps;
						foreach (var step in analyzerStatus.GetRoute())
						{
							tileResults[step.MazeTile.X, step.MazeTile.Y] = MazeTileResult.ShortestPath;
						}

						continue;
					}

					// Check all possible tiles we can go in.
					if (directions.HasFlag(Direction.Up) && tileResults[currentTile.X, currentTile.Y + 1] == MazeTileResult.Unreachable)
					{
						var nextTile = tiles[currentTile.X, currentTile.Y + 1];
						if (nextTile.openDirections.HasFlag(Direction.Down) && nextTile.specialTile != TileType.OuterWall)
						{
							tileResults[currentTile.X, currentTile.Y + 1] = MazeTileResult.Reachable;
							nextAnalyzerStatuses.Add(new AnalyzerStatus(nextTile, analyzerStatus));
						}
					}

					if (directions.HasFlag(Direction.Down) && tileResults[currentTile.X, currentTile.Y - 1] == MazeTileResult.Unreachable)
					{
						var nextTile = tiles[currentTile.X, currentTile.Y - 1];
						if (nextTile.openDirections.HasFlag(Direction.Up) && nextTile.specialTile != TileType.OuterWall)
						{
							tileResults[currentTile.X, currentTile.Y - 1] = MazeTileResult.Reachable;
							nextAnalyzerStatuses.Add(new AnalyzerStatus(nextTile, analyzerStatus));
						}
					}

					if (directions.HasFlag(Direction.Left) && tileResults[currentTile.X - 1, currentTile.Y] == MazeTileResult.Unreachable)
					{
						var nextTile = tiles[currentTile.X - 1, currentTile.Y];
						if (nextTile.openDirections.HasFlag(Direction.Right) && nextTile.specialTile != TileType.OuterWall)
						{
							tileResults[currentTile.X - 1, currentTile.Y] = MazeTileResult.Reachable;
							nextAnalyzerStatuses.Add(new AnalyzerStatus(nextTile, analyzerStatus));
						}
					}

					if (directions.HasFlag(Direction.Right) && tileResults[currentTile.X + 1, currentTile.Y] == MazeTileResult.Unreachable)
					{
						var nextTile = tiles[currentTile.X + 1, currentTile.Y];
						if (nextTile.openDirections.HasFlag(Direction.Left) && nextTile.specialTile != TileType.OuterWall)
						{
							tileResults[currentTile.X + 1, currentTile.Y] = MazeTileResult.Reachable;
							nextAnalyzerStatuses.Add(new AnalyzerStatus(nextTile, analyzerStatus));
						}
					}
				}
			}

			mazeAnalyzerResult.MazeTileClassifications = tileResults;

			// Calculate rating

			mazeAnalyzerResult.Rating += tileResults.Cast<MazeTileResult>().Count(r => r == MazeTileResult.ShortestPath) * 100;

			if (mazeAnalyzerResult.Rating > 0)
				mazeAnalyzerResult.Rating += tileResults.Cast<MazeTileResult>().Count(r => r == MazeTileResult.Reachable) * 50;

			return mazeAnalyzerResult;
		}

		class AnalyzerStatus
		{
			public MazeTile MazeTile { get; }
			public int Steps { get; } = -1;
			public AnalyzerStatus PreviousStep { get; }

			public AnalyzerStatus(MazeTile mazeTile, AnalyzerStatus analyzerStatus = null)
			{
				MazeTile = mazeTile;

				if (analyzerStatus is null)
					return;

				Steps = analyzerStatus.Steps + 1;
				PreviousStep = analyzerStatus;
			}

			public AnalyzerStatus[] GetRoute()
			{
				var previousSteps = new List<AnalyzerStatus>();
				var currentStep = this;

				while (currentStep != null)
				{
					previousSteps.Add(currentStep);
					currentStep = currentStep.PreviousStep;
				}

				return previousSteps.ToArray();
			}
		}
	}
}
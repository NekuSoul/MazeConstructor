using System;
using System.Text;
using Game.Code;
using Game.Code.Enums;
using UnityEngine;

namespace Game.Scripts
{
	public class ValidateButton : MonoBehaviour
	{
		public Maze maze;

		private void OnMouseDown()
		{
			var result = MazeAnalzyer.AnalyzeMaze(maze);
			Debug.Log(result.ShortestPath);
			var lBuilder = new StringBuilder();

			for (int y = maze.Height - 1; y >= 0; y--)
			{
				for (int x = 0; x < maze.Width; x++)
				{
					char targetChar;
					switch (result.MazeTileClassifications[x, y])
					{
						case MazeTileResult.Unreachable:
							targetChar = ' ';
							break;
						case MazeTileResult.Reachable:
							targetChar = 'X';
							break;
						case MazeTileResult.ShortestPath:
							targetChar = 'O';
							break;
						default:
							targetChar = '?';
							break;
					}

					lBuilder.Append(targetChar);
				}

				lBuilder.Append(Environment.NewLine);
			}

			Debug.Log(lBuilder.ToString());
		}
	}
}
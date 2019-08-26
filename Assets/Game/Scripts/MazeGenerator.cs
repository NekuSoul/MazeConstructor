using System;
using System.Collections.Generic;
using Game.Code.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
	[RequireComponent(typeof(Maze))]
	public class MazeGenerator : MonoBehaviour
	{
		private Maze _maze;

		public string seed = string.Empty;
		public int width = 10;
		public int height = 10;
		public int endTiles = 8;
		public int cornerTiles = 16;
		public int straightTiles = 16;
		public int threeWayIntersectionTiles = 12;
		public int fourWayIntersectionTiles = 12;

		public void Awake()
		{
			_maze = GetComponent<Maze>();
		}

		public void GenerateMaze()
		{
			_maze.InitializeMaze(width, height);

			Random.InitState(seed.GetHashCode());

			List<TileType> availableTiles = new List<TileType>();

			// Fill up available type list
			for (var i = 0; i < endTiles; i++)
				availableTiles.Add(TileType.End);

			for (var i = 0; i < cornerTiles; i++)
				availableTiles.Add(TileType.Corner);

			for (var i = 0; i < straightTiles; i++)
				availableTiles.Add(TileType.Straight);

			for (var i = 0; i < threeWayIntersectionTiles; i++)
				availableTiles.Add(TileType.ThreeWayIntersection);

			for (var i = 0; i < fourWayIntersectionTiles; i++)
				availableTiles.Add(TileType.FourWayIntersection);

			for (var x = 1; x < width - 1; x++)
			{
				for (var y = 1; y < height - 1; y++)
				{
					while (true)
					{
						var selectedTilePosition = Random.Range(0, availableTiles.Count);
						var selectedTile = availableTiles[selectedTilePosition];

						// Don't place end tiles near to entrance or exit
						if (selectedTile == TileType.End)
						{
							if (GetTileDistance(new Vector2Int(x, y), new Vector2Int(0, height - 3)) < 3)
								continue;
							if (GetTileDistance(new Vector2Int(x, y), new Vector2Int(width - 1, 2)) < 3)
								continue;
						}

						_maze.PlaceTile(x, y, Direction.None, selectedTile);
						availableTiles.RemoveAt(selectedTilePosition);
						break;
					}
				}
			}
		}

		private int GetTileDistance(Vector2Int positionA, Vector2Int positionB)
		{
			return Math.Abs(positionA.x - positionB.x) + Math.Abs(positionA.y - positionB.y);
		}
	}
}
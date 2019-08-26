using System;
using Game.Code.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
	[RequireComponent(typeof(Grid))]
	public class Maze : MonoBehaviour
	{
		public MazeTile mazeTilePrefab;

		public int Width { get; private set; }
		public int Height { get; private set; }
		private Grid _grid;
		public MazeTile[,] MazeTiles { get; private set; }

		public void Awake()
		{
			_grid = GetComponent<Grid>();
		}

		public void InitializeMaze(int width, int height)
		{
			Width = width;
			Height = height;
			MazeTiles = new MazeTile[width, height];

			PlaceOuterWall();
		}

		private void PlaceOuterWall()
		{
			// Place corners
			PlaceTile(0, 0, Direction.Up | Direction.Right, TileType.OuterWallCorner, false);
			PlaceTile(0, Height - 1, Direction.Down | Direction.Right, TileType.OuterWallCorner, false);
			PlaceTile(Width - 1, 0, Direction.Up | Direction.Left, TileType.OuterWallCorner, false);
			PlaceTile(Width - 1, Height - 1, Direction.Down | Direction.Left, TileType.OuterWallCorner, false);

			// Place horizontal walls
			for (var x = 1; x < Width - 1; x++)
			{
				PlaceTile(x, 0, Direction.Up, TileType.OuterWall, false);
				PlaceTile(x, Height - 1, Direction.Down, TileType.OuterWall, false);
			}

			// Place vertical walls
			for (var y = 1; y < Height - 1; y++)
			{
				PlaceTile(0, y, Direction.Right, TileType.OuterWall, false);
				PlaceTile(Width - 1, y, Direction.Left, TileType.OuterWall, false);
			}

			// Set entrance and exit
			MazeTiles[0, Height - 3].specialTile = TileType.Entrance;
			MazeTiles[Width - 1, 2].specialTile = TileType.Exit;
		}

		public void PlaceTile(int x, int y, Direction direction, TileType tileType, bool allowModification = true)
		{
			var mazeTile = Instantiate(mazeTilePrefab, transform, true);
			mazeTile.name = $"Tile [{x}|{y}]";
			mazeTile.openDirections = direction == Direction.None ? GenerateDirection(tileType) : direction;
			mazeTile.specialTile = tileType;
			mazeTile.allowModification = allowModification;
			mazeTile.X = x;
			mazeTile.Y = y;
			MazeTiles[x, y] = mazeTile;
			mazeTile.transform.position = _grid.CellToWorld(new Vector3Int(x, y, 0));
		}

		private Direction GenerateDirection(TileType tileType)
		{
			Direction direction;
			switch (tileType)
			{
				case TileType.End:
					direction = Direction.Down;
					break;
				case TileType.Corner:
					direction = Direction.Down | Direction.Right;
					break;
				case TileType.Straight:
					direction = Direction.Up | Direction.Down;
					break;
				case TileType.ThreeWayIntersection:
					direction = Direction.Up | Direction.Left | Direction.Right;
					break;
				case TileType.FourWayIntersection:
					direction = Direction.Up | Direction.Down | Direction.Left | Direction.Right;
					break;
				default:
					throw new Exception();
			}

			int rotateAmount = Random.Range(0, 4);
			for (var i = 0; i < rotateAmount; i++)
			{
				direction = direction.RotateClockwise();
			}

			return direction;
		}
	}
}
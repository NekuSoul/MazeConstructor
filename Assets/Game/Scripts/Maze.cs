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

		private int _width;
		private int _height;
		private Grid _grid;
		private MazeTile[,] _mazeTiles;

		public void Awake()
		{
			_grid = GetComponent<Grid>();
		}

		public void InitializeMaze(int width, int height)
		{
			_width = width;
			_height = height;
			_mazeTiles = new MazeTile[width, height];

			PlaceOuterWall();
		}

		private void PlaceOuterWall()
		{
			// Place corners
			PlaceTile(0, 0, Direction.Up | Direction.Right, TileType.OuterWallCorner, false);
			PlaceTile(0, _height - 1, Direction.Down | Direction.Right, TileType.OuterWallCorner, false);
			PlaceTile(_width - 1, 0, Direction.Up | Direction.Left, TileType.OuterWallCorner, false);
			PlaceTile(_width - 1, _height - 1, Direction.Down | Direction.Left, TileType.OuterWallCorner, false);

			// Place horizontal walls
			for (var x = 1; x < _width - 1; x++)
			{
				PlaceTile(x, 0, Direction.Up, TileType.OuterWall, false);
				PlaceTile(x, _height - 1, Direction.Down, TileType.OuterWall, false);
			}

			// Place vertical walls
			for (var y = 1; y < _height - 1; y++)
			{
				PlaceTile(0, y, Direction.Right, TileType.OuterWall, false);
				PlaceTile(_width - 1, y, Direction.Left, TileType.OuterWall, false);
			}

			// Set entrance and exit
			_mazeTiles[0, _height - 3].specialTile = TileType.Entrance;
			_mazeTiles[_width - 1, 2].specialTile = TileType.Exit;
		}

		public void PlaceTile(int x, int y, Direction direction, TileType tileType, bool allowModification = true)
		{
			var mazeTile = Instantiate(mazeTilePrefab);
			mazeTile.name = $"Tile [{x}|{y}]";
			mazeTile.openDirections = direction == Direction.None ? GenerateDirection(tileType) : direction;
			mazeTile.specialTile = tileType;
			mazeTile.allowModification = allowModification;
			_mazeTiles[x, y] = mazeTile;
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
					direction = Direction.Up | Direction.Up | Direction.Left | Direction.Right;
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
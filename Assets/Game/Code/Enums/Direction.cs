using System;

namespace Game.Code.Enums
{
	[Flags]
	public enum Direction
	{
		None = 0,
		Up = 1,
		Down = 1 << 1,
		Left = 1 << 2,
		Right = 1 << 3
	}

	public static class DirectionExtender
	{
		public static Direction RotateClockwise(this Direction direction)
		{
			var outputDirecion = Direction.None;

			if (direction.HasFlag(Direction.Up))
				outputDirecion |= Direction.Right;

			if (direction.HasFlag(Direction.Down))
				outputDirecion |= Direction.Left;

			if (direction.HasFlag(Direction.Left))
				outputDirecion |= Direction.Up;

			if (direction.HasFlag(Direction.Right))
				outputDirecion |= Direction.Down;
			
			return outputDirecion;
		}
		
		public static Direction RotateCounterClockwise(this Direction direction)
		{
			var outputDirecion = Direction.None;

			if (direction.HasFlag(Direction.Up))
				outputDirecion |= Direction.Left;

			if (direction.HasFlag(Direction.Down))
				outputDirecion |= Direction.Right;

			if (direction.HasFlag(Direction.Left))
				outputDirecion |= Direction.Down;

			if (direction.HasFlag(Direction.Right))
				outputDirecion |= Direction.Up;
			
			return outputDirecion;
		}

		public static TileType GetTileType(this Direction direction)
		{
			var openDirectionCount = 0;

			if (direction.HasFlag(Direction.Up))
				openDirectionCount++;

			if (direction.HasFlag(Direction.Down))
				openDirectionCount++;

			if (direction.HasFlag(Direction.Left))
				openDirectionCount++;

			if (direction.HasFlag(Direction.Right))
				openDirectionCount++;

			switch (openDirectionCount)
			{
				case 0:
					return TileType.Empty;
				case 1:
					return TileType.End;
				case 2:
					if (direction == (Direction.Up | Direction.Down) ||
					    direction == (Direction.Left | Direction.Right))
						return TileType.Straight;
					else
						return TileType.Corner;
				case 3:
					return TileType.ThreeWayIntersection;
				case 4:
					return TileType.FourWayIntersection;
				default:
					throw new Exception("Weird...");
			}
		}
	}
}
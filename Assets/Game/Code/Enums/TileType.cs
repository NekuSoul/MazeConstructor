using System;
using UnityEngine;

namespace Game.Code.Enums
{
	public enum TileType
	{
		None,
		Empty,
		End,
		Straight,
		Corner,
		ThreeWayIntersection,
		FourWayIntersection,
		OuterWall,
		OuterWallCorner,
		Entrance,
		Exit
	}

	public static class TileTypeExtender
	{
		public static Sprite ToSprite(this TileType tileType)
		{
			return Statics.MazeSpriteManager.GetSpriteFromSprite(tileType);
		}

		public static Rotation GetRotation(this TileType tileType, Direction openDirections)
		{
			switch (tileType)
			{
				case TileType.Empty:
					return Rotation.Rot0;
				case TileType.End:
				case TileType.OuterWall:
				case TileType.Entrance:
				case TileType.Exit:
					return GetRotationEnd(openDirections);
				case TileType.Straight:
					return GetRotationStraight(openDirections);
				case TileType.Corner:
				case TileType.OuterWallCorner:
					return GetRotationCorner(openDirections);
				case TileType.ThreeWayIntersection:
					return GetRotationThreeWayIntersection(openDirections);
				case TileType.FourWayIntersection:
					return Rotation.Rot0;
				default:
					throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null);
			}
		}

		private static Rotation GetRotationEnd(Direction openDirections)
		{
			switch (openDirections)
			{
				case Direction.Up:
					return Rotation.Rot180;
				case Direction.Down:
					return Rotation.Rot0;
				case Direction.Left:
					return Rotation.Rot270;
				case Direction.Right:
					return Rotation.Rot90;
				default:
					throw new ArgumentOutOfRangeException(nameof(openDirections), openDirections, null);
			}
		}

		private static Rotation GetRotationStraight(Direction openDirections)
		{
			if (openDirections.HasFlag(Direction.Up) || openDirections.HasFlag(Direction.Down))
				return Rotation.Rot0;

			return Rotation.Rot90;
		}

		private static Rotation GetRotationCorner(Direction openDirections)
		{
			if (openDirections.HasFlag(Direction.Up) && openDirections.HasFlag(Direction.Right))
				return Rotation.Rot90;

			if (openDirections.HasFlag(Direction.Right) && openDirections.HasFlag(Direction.Down))
				return Rotation.Rot0;

			if (openDirections.HasFlag(Direction.Down) && openDirections.HasFlag(Direction.Left))
				return Rotation.Rot270;

			return Rotation.Rot180;
		}

		private static Rotation GetRotationThreeWayIntersection(Direction openDirections)
		{
			if (!openDirections.HasFlag(Direction.Up))
				return Rotation.Rot0;

			if (!openDirections.HasFlag(Direction.Down))
				return Rotation.Rot180;

			return !openDirections.HasFlag(Direction.Right) ? Rotation.Rot270 : Rotation.Rot90;
		}
	}
}
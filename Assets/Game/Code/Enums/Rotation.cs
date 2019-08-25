using System;
using UnityEngine;

namespace Game.Code.Enums
{
	public enum Rotation
	{
		Rot0,
		Rot90,
		Rot180,
		Rot270
	}

	public static class RotationExtender
	{
		public static Vector3 ToEulerAngles(this Rotation rotation)
		{
			switch (rotation)
			{
				case Rotation.Rot0:
					return Vector3.forward * 0;
				case Rotation.Rot90:
					return Vector3.forward * 90;
				case Rotation.Rot180:
					return Vector3.forward * 180;
				case Rotation.Rot270:
					return Vector3.forward * 270;
				default:
					throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null);
			}
		}
	}
}
using System;
using Game.Code.Enums;
using UnityEngine;

namespace Game.Scripts
{
	public class MazeTile : MonoBehaviour
	{
		private SpriteRenderer _spriteRenderer;

		public Direction openDirections;
		public TileType specialTile = TileType.None;
		public bool allowModification;

		public void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		public void Start()
		{
			ApplySprite();
		}

		public void RotateClockwise()
		{
			openDirections = openDirections.RotateClockwise();
			ApplySprite();
		}

		public void RotateCounterClockwise()
		{
			openDirections = openDirections.RotateCounterClockwise();
			ApplySprite();
		}

		public void ApplySprite()
		{
			var tileType = specialTile == TileType.None ? openDirections.GetTileType() : specialTile;
			var rotation = tileType.GetRotation(openDirections);

			_spriteRenderer.sprite = tileType.ToSprite();
			transform.eulerAngles = rotation.ToEulerAngles();
		}

		private void OnMouseOver()
		{
			if(!allowModification)
				return;

			if (Input.GetMouseButtonDown(0))
			{
				RotateCounterClockwise();
				ApplySprite();
			}

			if (Input.GetMouseButtonDown(1))
			{
				RotateClockwise();
				ApplySprite();
			}
		}
	}
}
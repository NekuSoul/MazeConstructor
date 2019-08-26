using Game.Code;
using Game.Code.Enums;
using UnityEngine;

namespace Game.Scripts
{
	public class MazeTile : MonoBehaviour
	{
		public SpriteRenderer SpriteRenderer { get; private set; }

		public Direction openDirections;
		public TileType specialTile = TileType.None;
		public bool allowModification;
		public int x;
		public int y;

		public void Awake()
		{
			SpriteRenderer = GetComponent<SpriteRenderer>();
		}

		public void Start()
		{
			ApplySprite();
		}

		public void RotateClockwise()
		{
			openDirections = openDirections.RotateClockwise();
			ApplySprite();
			Statics.GameManager.AnalyzeMaze();
		}

		public void RotateCounterClockwise()
		{
			openDirections = openDirections.RotateCounterClockwise();
			ApplySprite();
			Statics.GameManager.AnalyzeMaze();
		}

		public void ApplySprite()
		{
			var tileType = specialTile == TileType.None ? openDirections.GetTileType() : specialTile;
			var rotation = tileType.GetRotation(openDirections);

			SpriteRenderer.sprite = tileType.ToSprite();
			transform.eulerAngles = rotation.ToEulerAngles();
		}

		private void OnMouseOver()
		{
			if(!allowModification)
				return;

			if (Input.GetMouseButtonDown(0))
			{
				RotateClockwise();
				ApplySprite();
			}

			if (Input.GetMouseButtonDown(1))
			{
				RotateCounterClockwise();
				ApplySprite();
			}
		}
	}
}
using System;
using Game.Code;
using Game.Code.Enums;
using UnityEngine;

namespace Game.Scripts
{
    public class MazeSpriteManager : MonoBehaviour
    {
        public Sprite emptySprite;
        public Sprite endSprite;
        public Sprite straightSprite;
        public Sprite cornerSprite;
        public Sprite threeWayIntersection;
        public Sprite fourWayIntersection;
        public Sprite outerWall;
        public Sprite outerWallCorner;
        public Sprite entrance;
        public Sprite exit;
    
        public void Awake()
        {
            Statics.MazeSpriteManager = this;
        }

        public Sprite GetSpriteFromSprite(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Empty:
                    return emptySprite;
                case TileType.End:
                    return endSprite;
                case TileType.Straight:
                    return straightSprite;
                case TileType.Corner:
                    return cornerSprite;
                case TileType.ThreeWayIntersection:
                    return threeWayIntersection;
                case TileType.FourWayIntersection:
                    return fourWayIntersection;
                case TileType.OuterWall:
                    return outerWall;
                case TileType.OuterWallCorner:
                    return outerWallCorner;
                case TileType.Entrance:
                    return entrance;
                case TileType.Exit:
                    return exit;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null);
            }
        }
    }
}

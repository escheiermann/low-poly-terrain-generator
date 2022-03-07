using UnityEngine;

namespace LowPolyTerrainGenerator.Height.Strategies {
    public class MapHeightStrategy : HeightStrategy {
        private Texture2D heightMap;
        public MapHeightStrategy(int length, int width, int maximumHeight, Texture2D heightMap): base(length, width, maximumHeight) {
            this.heightMap = heightMap;
        }

        protected override int GetHeight(int x, int y) {
            int xPos = (int)Mathf.Round(x / ((float)(length + 1) / heightMap.width));
            int yPos = (int)Mathf.Round(y / ((float)(width + 1) / heightMap.height));
            return (int)(heightMap.GetPixel(xPos, yPos).grayscale * maximumHeight);
        }
    }
}


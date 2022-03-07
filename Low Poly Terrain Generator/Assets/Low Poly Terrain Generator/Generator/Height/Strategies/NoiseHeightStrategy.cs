using UnityEngine;

namespace LowPolyTerrainGenerator.Height.Strategies {
    public class NoiseHeightStrategy : HeightStrategy {
        private float scale;

        public NoiseHeightStrategy(int length, int width, int maximumHeight, float scale): base(length, width, maximumHeight) {
            this.scale = scale;
        }

        override protected int GetHeight(int x, int y) {
            float xPos = (float)x / width * this.scale;
            float yPos = (float)y / length * this.scale;
            float height = Mathf.PerlinNoise(xPos, yPos);
            return (int)(height * maximumHeight);
        }
    }
}

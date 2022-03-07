using UnityEngine;
using LowPolyTerrainGenerator.Height;

namespace LowPolyTerrainGenerator {
    [System.Serializable]
    public class TerrainOptions {
        public int length;
        public int width;

        [Min(0)]
        public int maximumHeight;
        public Material material;
        public HeightStrategyType heightAlgorithm;
        public Texture2D heightMap;

        [Range(0.0f, 100.0f)] 
        public float scale;

        public static TerrainOptions Default() {
            TerrainOptions options = new TerrainOptions();
            options.length = 30;
            options.width = 30;
            options.maximumHeight = 10;
            options.heightAlgorithm = HeightStrategyType.Random;
            options.scale = 9.0f;
            return options;
        }
    }
}

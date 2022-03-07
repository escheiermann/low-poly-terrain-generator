using LowPolyTerrainGenerator.Height.Strategies;

namespace LowPolyTerrainGenerator.Height {
    public class HeightStrategyFactory {

        /// <summary>
        /// Creates a new HeightStrategy according to the terrain options.
        /// </summary>
        /// <param name="options">Options regarding the terrain</param>
        public static HeightStrategy Create(TerrainOptions options) {
            HeightStrategy strategy;
            switch (options.heightAlgorithm) {
                case HeightStrategyType.Noise: 
                    strategy = new NoiseHeightStrategy(options.length, options.width, options.maximumHeight, options.scale);
                    break;
                case HeightStrategyType.HeightMap: 
                    strategy = new MapHeightStrategy(options.length, options.width, options.maximumHeight, options.heightMap);
                    break;
                case HeightStrategyType.Random:
                default: 
                    strategy = new RandomHeightStrategy(options.length, options.width, options.maximumHeight);
                    break;
            }
            return strategy;
        }
    }
}

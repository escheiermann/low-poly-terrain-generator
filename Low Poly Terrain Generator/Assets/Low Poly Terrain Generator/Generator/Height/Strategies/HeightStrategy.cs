namespace LowPolyTerrainGenerator.Height {
    public abstract class HeightStrategy {
        protected int length;
        protected int width;
        protected int maximumHeight;

        /// <summary>
        /// Creates a new HeightStrategy instance.
        /// </summary>
        /// <param name="length">Length of the terrain</param>
        /// <param name="width">Width of the terrain</param>
        /// <param name="maximumHeight">Maximum Height of the terrain</param>
        public HeightStrategy(int length, int width, int maximumHeight) {
            this.length = length;
            this.width = width;
            this.maximumHeight = maximumHeight;
        }

        /// <summary>
        /// Generates height values for the terrain.
        /// </summary>
        public int[,] Generate() {
            int[,] heights = new int[length + 1, width + 1];
            for (int i = 0; i < heights.GetLength(0); i++) {
                for (int j = 0; j < heights.GetLength(1); j++) {
                    heights[i, j] = GetHeight(i, j);
                }
            }
            return heights;
        }

        /// <summary>
        /// Method to be implemented for calculating the height for a given point with a pattern.
        /// </summary>
        /// <param name="x">X coordinate of the point in the terrain</param>
        /// <param name="y">Y coordinate of the point in the terrain</param>
        protected abstract int GetHeight(int x, int y);
    }
}

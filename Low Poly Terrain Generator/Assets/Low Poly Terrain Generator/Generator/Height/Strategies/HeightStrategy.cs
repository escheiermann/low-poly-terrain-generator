/*
 * Copyright (c) 2022 Matthias Erdmann & Edgar Scheiermann
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

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

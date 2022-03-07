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

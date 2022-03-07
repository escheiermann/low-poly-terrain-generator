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
using LowPolyTerrainGenerator.Objects;

namespace LowPolyTerrainGenerator {
    /// <summary>
    /// Class TerrainGenerator generates Terrain.
    /// </summary>
    public class TerrainGenerator {

        // Because of memory issues regarding the calculation of the vertices,
        // the length and width of the terrain should not exceed a certain number.
        private static int maximumSize = 1000;

        /// <summary>
        /// Method Landscape generates a landscape GameObject containing a terrain and environment.
        /// </summary>
        /// <param name="terrainOptions">Options regarding the terrain</param>
        /// <param name="environmentOptions">Options regarding the environment</param>
        public static void Landscape(TerrainOptions terrainOptions, EnvironmentOptions environmentOptions) {
            GameObject landscape = new GameObject("Landscape");
            parseArguments(terrainOptions);

            LowPolyTerrain terrain = LowPolyTerrain.From(terrainOptions);
            AddObject(landscape, terrain.GameObject);

            Environment environment = Environment.From(environmentOptions, terrain);
            if (environment.Trees != null) {
                AddObject(landscape, environment.Trees);
            }
            if (environment.Supplies != null) {
                AddObject(landscape, environment.Supplies);
            }
        }

        private static void AddObject(GameObject parent, GameObject child) {
            child.transform.parent = parent.transform;
        }

        private static void parseArguments(TerrainOptions terrainOptions) {
            if (terrainOptions.length > maximumSize) {
                terrainOptions.length = maximumSize;
            }
            if (terrainOptions.width > maximumSize) {
                terrainOptions.width = maximumSize;
            }
        }
    }
}

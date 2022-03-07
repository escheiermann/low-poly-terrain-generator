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

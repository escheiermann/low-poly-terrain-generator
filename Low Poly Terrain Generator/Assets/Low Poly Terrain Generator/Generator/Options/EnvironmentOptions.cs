using UnityEngine;

namespace LowPolyTerrainGenerator {
    [System.Serializable]
    public class EnvironmentOptions {
        [Range(0, 400)] 
        public int treeDensity;
        public GameObject[] trees;

        [Range(0, 400)] 
        public int supplyDensity;
        public GameObject[] supplies;

        public static EnvironmentOptions Default() {
            EnvironmentOptions options = new EnvironmentOptions();
            options.treeDensity = 200;
            options.trees = new GameObject[0];
            options.supplyDensity = 200;
            options.supplies = new GameObject[0];
            return options;
        }
    }
}

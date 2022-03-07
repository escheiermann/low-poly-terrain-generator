using UnityEngine;

namespace LowPolyTerrainGenerator.Height.Strategies {
    public class RandomHeightStrategy : HeightStrategy {
        public RandomHeightStrategy(int length, int width, int maximumHeight): base(length, width, maximumHeight) {}

        override protected int GetHeight(int x, int y) {
            return Random.Range(0, maximumHeight);
        }
    }
}

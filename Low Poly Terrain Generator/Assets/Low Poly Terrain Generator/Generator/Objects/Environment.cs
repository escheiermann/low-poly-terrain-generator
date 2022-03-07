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
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace LowPolyTerrainGenerator.Objects {
    public class Environment: ScriptableObject {

        public GameObject Trees { get; private set; }
        public GameObject Supplies { get; private set; }

        /// <summary>
        /// Method From generates a environment containing trees and supplies.
        /// </summary>
        /// <param name="options">Options regarding the environment</param>
        /// <param name="terrain">LowPolyTerrain where objects are places</param>
        public static Environment From(EnvironmentOptions options, LowPolyTerrain terrain) {
            Environment environment = new Environment();
            Stack<int> randomStack = RandomSequence(terrain.Vertices);
            if (options.trees.Length != 0) { 
                environment.GenerateTrees(options, terrain.Vertices, randomStack);
            }
            if (options.supplies.Length != 0) { 
                environment.GenerateSupplies(options, terrain.Vertices, randomStack);
            }
            return environment;
        }

        private static Stack<int> RandomSequence(Vector3[] vertices) {
            List<int> randomNumbers = Enumerable.Range(0, vertices.Length - 1).ToList();
            Stack<int> randomStack = new Stack<int>();
            randomNumbers = randomNumbers.OrderBy(x => Random.value).ToList();
            for (int i = 0; i < randomNumbers.Count; i++) {
                randomStack.Push(randomNumbers[i]);
            }
            return randomStack;
        }

        private void GenerateTrees(EnvironmentOptions options, Vector3[] vertices, Stack<int> randomStack) {
            Trees = Environment.PlaceObjectsOnTerrain("Trees", options.trees, options.treeDensity, vertices, randomStack); 
        }

        private void GenerateSupplies(EnvironmentOptions options, Vector3[] vertices, Stack<int> randomStack) {
            Supplies = Environment.PlaceObjectsOnTerrain("Supplies", options.supplies, options.supplyDensity, vertices, randomStack);
        }

        public static GameObject PlaceObjectsOnTerrain(string name, GameObject[] objects, int density, Vector3[] vertices, Stack<int> randomStack) {
            GameObject container = new GameObject(name);

            for (int i = 0; i < objects.Length; i++) {
                string resource = AssetDatabase.GetAssetPath(objects[i]);
                var index = 0;
                float randomPositionX, randomPositionY, randomPositionZ;
                Vector3 randomPosition = Vector3.zero;

                do {
                    index++;         
                    int chooseRandomVertice = (int)randomStack.Pop();
                    randomPositionX = vertices[chooseRandomVertice][0];
                    randomPositionY = vertices[chooseRandomVertice][1];
                    randomPositionZ = vertices[chooseRandomVertice][2];
                    randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);

                    GameObject supply = Instantiate(AssetDatabase.LoadAssetAtPath(resource, typeof(GameObject)), randomPosition, Quaternion.identity) as GameObject;

                    supply.transform.parent = container.transform;
                    supply.transform.Rotate(0f, Random.Range(0, 360), 0f);
                } while (index < density);

            }
            return container;
        }
    }
}

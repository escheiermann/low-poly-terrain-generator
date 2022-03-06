using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public enum GenerationStrategy
{
    Random, Noise, HeightMap
}

public class CreateTerrainWizard : ScriptableWizard
{
    [Header("Terrain")]
    public TerrainOptions terrainOptions = TerrainOptions.Default();

    [Header("Objects")]
    [Range(0, 400)] public int treeDensity = 200;
    public GameObject[] trees = new GameObject[0];

    [Range(0, 400)] public int supplyDensity = 200;
    public GameObject[] supplies = new GameObject[0];

    [MenuItem("GameObject/Generate Low Poly Terrain...")]
    static void CreateWizard() {
        DisplayWizard<CreateTerrainWizard>("Generate Terrain", "Generate new");
    }

    void OnWizardCreate() {
        parseArguments();
        GenerateLandscape();
    }

    private void parseArguments() {
        if (terrainOptions.length > 1000) { terrainOptions.length = 1000; }
        if (terrainOptions.width > 1000) { terrainOptions.width = 1000; }
    }

    private void GenerateLandscape() {
        Vector3[] vertices = new Vector3[(terrainOptions.width + 1) * (terrainOptions.length + 1)];
        int[] triangles = new int[terrainOptions.width * terrainOptions.length * 2 * 3];
        GenerateVerticesAndTriangles(vertices, triangles);
        // generate random Numbers
        List<int> randomNumbers = Enumerable.Range(0, vertices.Length - 1).ToList();
        Stack<int> randomStack = new Stack<int>();

        // randomize list
        randomNumbers = randomNumbers.OrderBy(x => Random.value).ToList();

        // fill stack
        for (int i = 0; i < randomNumbers.Count; i++)
        {
            randomStack.Push(randomNumbers[i]);
        }

        GameObject Landscape = new GameObject("Landscape");
        GenerateTerrain(vertices, triangles).transform.parent = Landscape.transform;

        if (trees.Length != 0) { GenerateTrees(vertices, randomStack).transform.parent = Landscape.transform; }
        if (supplies.Length != 0) { GenerateSupplies(vertices, randomStack).transform.parent = Landscape.transform; }

    }

    private GameObject GenerateTerrain(Vector3[] vertices, int[] triangles) {
        GameObject terrain = new GameObject("Terrain");
        AddMeshRenderer(terrain);
        AddMesh(terrain, vertices, triangles);
        return terrain;
    }

    private void GenerateVerticesAndTriangles(Vector3[] vertices, int[] triangles)
    {
        int[,] heights = GenerateHeightMap();
        int baseIndex = 0;
        for (int i = 0; i <= terrainOptions.length; i++)
        {
            for (int j = 0; j <= terrainOptions.width; j++)
            {
                vertices[baseIndex] = new Vector3(10 * j, heights[i, j], 10 * i);
                baseIndex++;
            }
        }

        baseIndex = 0;
        int verticesIndex = 0;
        for (int i = 0; i < terrainOptions.length; i++)
        {
            for (int j = 0; j < terrainOptions.width; j++)
            {
                triangles[baseIndex] = verticesIndex;
                triangles[baseIndex + 1] = terrainOptions.width + verticesIndex + 1;
                triangles[baseIndex + 2] = verticesIndex + 1;

                triangles[baseIndex + 3] = terrainOptions.width + verticesIndex + 1;
                triangles[baseIndex + 4] = terrainOptions.width + verticesIndex + 2;
                triangles[baseIndex + 5] = verticesIndex + 1;
                baseIndex += 6;
                verticesIndex++;
            }
            verticesIndex++;
        }
    }

    private int[,] GenerateHeightMap()
    {
        switch (terrainOptions.generationStrategy)
        {
            case GenerationStrategy.Random: return randomHeightSource();
            case GenerationStrategy.Noise: return noiseHeightSource();
            case GenerationStrategy.HeightMap: return heightMapSource();
            default: return randomHeightSource();
        }
    }

    private int[,] randomHeightSource()
    {
        int[,] heights = new int[terrainOptions.length + 1, terrainOptions.width + 1];
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                heights[i, j] = Random.Range(0, terrainOptions.maximumHeight);
            }
        }
        return heights;
    }

    private int[,] noiseHeightSource()
    {
        int[,] heights = new int[terrainOptions.length + 1, terrainOptions.width + 1];
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                heights[i, j] = (int)((Mathf.PerlinNoise((float)i / terrainOptions.width * terrainOptions.scale, (float)j / terrainOptions.length * terrainOptions.scale) * terrainOptions.maximumHeight));
            }
        }
        return heights;
    }

    private int[,] heightMapSource()
    {
        int[,] heights = new int[terrainOptions.length + 1, terrainOptions.width + 1];
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                int x = (int)Mathf.Round(i / ((float)heights.GetLength(0) / terrainOptions.heightMap.width));
                int y = (int)Mathf.Round(j / ((float)heights.GetLength(1) / terrainOptions.heightMap.height));
                heights[i, j] = (int)(terrainOptions.heightMap.GetPixel(x, y).grayscale * terrainOptions.maximumHeight);
            }
        }
        return heights;
    }

    private static void AddMesh(GameObject terrain, Vector3[] vertices, int[] triangles)
    {
        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        MeshFilter meshFilter = terrain.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;
        terrain.AddComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void AddMeshRenderer(GameObject terrain)
    {
        MeshRenderer meshRenderer = terrain.AddComponent<MeshRenderer>();
        meshRenderer.material = terrainOptions.material;
    }
    private GameObject GenerateTrees(Vector3[] vertices, Stack<int> randomStack)
    {
        GameObject treesObject = new GameObject("Trees");
        for (int i = 0; i < trees.Length; i++)
        {
            string Resource = AssetDatabase.GetAssetPath(trees[i]);
            var a = 0;
            float randomPositionX, randomPositionY, randomPositionZ;
            Vector3 randomPosition = Vector3.zero;
            do
            {
                a++;
                int chooseRandomVertice = (int)randomStack.Pop();
                randomPositionX = vertices[chooseRandomVertice][0];
                randomPositionY = vertices[chooseRandomVertice][1];
                randomPositionZ = vertices[chooseRandomVertice][2];
                randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);
                GameObject tree = Instantiate(AssetDatabase.LoadAssetAtPath(Resource, typeof(GameObject)), randomPosition, Quaternion.identity) as GameObject;

                tree.transform.parent = treesObject.transform;
                tree.transform.Rotate(0f, Random.Range(0, 360), 0f);
            } while (a < treeDensity);

        }
        return treesObject;    
    }

    private GameObject GenerateSupplies(Vector3[] vertices, Stack<int> randomStack)
    {
        GameObject Supplies = new GameObject("Supplies");

        for (int i = 0; i < supplies.Length; i++)
        {
            string Resource = AssetDatabase.GetAssetPath(supplies[i]);
            var a = 0;
            float randomPositionX, randomPositionY, randomPositionZ;
            Vector3 randomPosition = Vector3.zero;

            do
            {
                a++;         
                int chooseRandomVertice = (int)randomStack.Pop();
                randomPositionX = vertices[chooseRandomVertice][0];
                randomPositionY = vertices[chooseRandomVertice][1];
                randomPositionZ = vertices[chooseRandomVertice][2];
                randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);

                GameObject supply = Instantiate(AssetDatabase.LoadAssetAtPath(Resource, typeof(GameObject)), randomPosition, Quaternion.identity) as GameObject;

                supply.transform.parent = Supplies.transform;

            } while (a < supplyDensity);

        }
        return Supplies;
    }
}
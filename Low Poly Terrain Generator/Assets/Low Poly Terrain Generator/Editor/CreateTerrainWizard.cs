using UnityEngine;
using UnityEditor;
using LowPolyTerrainGenerator;

public class CreateTerrainWizard : ScriptableWizard {
    [Header("Terrain")]
    public TerrainOptions terrainOptions = TerrainOptions.Default();

    [Header("Environment")]
    public EnvironmentOptions environmentOptions = EnvironmentOptions.Default();

    [MenuItem("GameObject/Generate Low Poly Terrain...")]
    static void CreateWizard() {
        DisplayWizard<CreateTerrainWizard>("Generate Terrain", "Generate new");
    }

    void OnWizardCreate() {
        TerrainGenerator.Landscape(terrainOptions, environmentOptions);
    }
}

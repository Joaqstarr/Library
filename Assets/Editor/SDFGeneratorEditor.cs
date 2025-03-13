using UnityEngine;
using UnityEditor;
using System.IO;

public class SDFTextureGenerator : EditorWindow
{
    private int resolution = 64; // Size of the 3D texture
    private float sphereRadius = 0.4f; // Base sphere SDF radius
    private float noiseScale = 10.0f; // Perlin noise scale
    private float noiseStrength = 0.2f; // Noise intensity
    private string savePath = "Assets/GeneratedSDF.asset"; // Default save location

    [MenuItem("Tools/SDF Texture Generator")]
    public static void ShowWindow()
    {
        GetWindow<SDFTextureGenerator>("SDF Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("SDF Texture Generator", EditorStyles.boldLabel);
        
        resolution = EditorGUILayout.IntField("Resolution", resolution);
        sphereRadius = EditorGUILayout.Slider("Sphere Radius", sphereRadius, 0.1f, 1.0f);
        noiseScale = EditorGUILayout.FloatField("Noise Scale", noiseScale);
        noiseStrength = EditorGUILayout.FloatField("Noise Strength", noiseStrength);
        savePath = EditorGUILayout.TextField("Save Path", savePath);

        if (GUILayout.Button("Generate SDF Texture"))
        {
            GenerateAndSaveSDFTexture();
        }
    }

    private void GenerateAndSaveSDFTexture()
    {
        Texture3D sdfTexture = GenerateSDFTexture();
        SaveTexture3D(sdfTexture, savePath);
        AssetDatabase.Refresh();
    }

    private Texture3D GenerateSDFTexture()
    {
        int size = resolution;
        Texture3D texture = new Texture3D(size, size, size, TextureFormat.RFloat, false);
        texture.wrapMode = TextureWrapMode.Clamp;

        Color[] colors = new Color[size * size * size];

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    // Convert to normalized space (-1 to 1)
                    float nx = (x / (float)(size - 1)) * 2.0f - 1.0f;
                    float ny = (y / (float)(size - 1)) * 2.0f - 1.0f;
                    float nz = (z / (float)(size - 1)) * 2.0f - 1.0f;
                    Vector3 pos = new Vector3(nx, ny, nz);

                    // Compute base sphere SDF
                    float dist = pos.magnitude - sphereRadius;

                    // Apply Perlin noise
                    float noise = PerlinNoise3D(pos.x * noiseScale, pos.y * noiseScale, pos.z * noiseScale);

                    noise /= 3.0f; // Normalize noise value
                    noise = (noise - 0.5f) * 2.0f; // Convert to -1 to 1 range

                    // Modify SDF with noise
                    dist += noise * noiseStrength;

                    // Store in texture
                    int index = x + size * (y + size * z);
                    colors[index] = new Color(dist, dist, dist, 1.0f);
                }
            }
        }

        // Assign data to texture
        texture.SetPixels(colors);
        texture.Apply();

        return texture;
    }

    private void SaveTexture3D(Texture3D texture, string path)
    {
        AssetDatabase.CreateAsset(texture, path);
        AssetDatabase.SaveAssets();
        Debug.Log("Saved SDF Texture to: " + path);
    }
    
    // Simple 3D Perlin noise function using Unity's 2D PerlinNoise
    private float PerlinNoise3D(float x, float y, float z)
    {
        float xy = Mathf.PerlinNoise(x, y);
        float yz = Mathf.PerlinNoise(y, z);
        float xz = Mathf.PerlinNoise(x, z);
        float yx = Mathf.PerlinNoise(y, x);
        float zx = Mathf.PerlinNoise(z, x);
        float zy = Mathf.PerlinNoise(z, y);

        return (xy + yz + xz + yx + zx + zy) / 6.0f;
    }
}

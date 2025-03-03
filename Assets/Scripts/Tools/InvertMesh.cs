using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

public class InvertMeshEditor : EditorWindow
{
    [MenuItem("Tools/Invert Mesh")]
    public static void ShowWindow()
    {
        GetWindow<InvertMeshEditor>("Invert Mesh");
    }

    void OnGUI()
    {
        GUILayout.Label("Invert the Mesh of the selected object", EditorStyles.boldLabel);

        if (GUILayout.Button("Copy and Invert Mesh"))
        {
            InvertSelectedMesh();
        }
    }

    void InvertSelectedMesh()
    {
        // Obtenir l'objet actuellement sélectionné
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            Debug.LogError("No object selected.");
            return;
        }

        MeshFilter meshFilter = selectedObject.GetComponent<MeshFilter>();

        if (meshFilter == null)
        {
            Debug.LogError("Selected object does not have a MeshFilter.");
            return;
        }

        Mesh originalMesh = meshFilter.sharedMesh;

        if (originalMesh != null)
        {
            // Créer une copie du mesh
            Mesh meshCopy = Instantiate(originalMesh);
            meshCopy.name = originalMesh.name + "_Inverted";

            // Inverser les triangles et les normales de la copie
            InvertMeshTriangles(meshCopy);
            InvertMeshNormals(meshCopy);

            // Appliquer la copie du mesh à l'objet sélectionné
            meshFilter.mesh = meshCopy;

            Debug.Log("Mesh copied and inverted successfully.");
        }
        else
        {
            Debug.LogError("No mesh found on the selected object.");
        }
    }

    void InvertMeshTriangles(Mesh mesh)
    {
        // Obtenir les triangles du mesh
        int[] triangles = mesh.triangles;

        // Inverser l'ordre des sommets dans chaque triangle
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int temp = triangles[i];
            triangles[i] = triangles[i + 1];
            triangles[i + 1] = temp;
        }

        // Appliquer les triangles modifiés au mesh
        mesh.triangles = triangles;

        // Recalculer les tangentes si elles sont présentes
        if (mesh.tangents.Length > 0)
        {
            mesh.RecalculateTangents();
        }

        // Recalculer les normales
        mesh.RecalculateNormals();

        // Marquer le mesh comme modifié dans l'éditeur
        EditorUtility.SetDirty(mesh);
    }

    void InvertMeshNormals(Mesh mesh)
    {
        // Inverser les normales du mesh
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }

        // Appliquer les normales inversées
        mesh.normals = normals;

        // Marquer le mesh comme modifié dans l'éditeur
        EditorUtility.SetDirty(mesh);
    }
}

#endif
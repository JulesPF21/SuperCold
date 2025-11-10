using UnityEngine;

[ExecuteAlways]
public class ModularArray : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject prefab;                     // L'asset à dupliquer

    [Header("Array Dimensions")]
    [Min(1)] public int countX = 3;               // Nombre de modules sur l'axe X (horizontal)
    [Min(1)] public int countY = 1;               // Nombre de modules sur l'axe Y (vertical)
    public Vector3 offsetX = new Vector3(4, 0, 0); // Espacement horizontal
    public Vector3 offsetY = new Vector3(0, 2, 0); // Espacement vertical

    [Header("Transform Options")]
    public Vector3 rotationOffset = Vector3.zero; // Rotation appliquée à chaque instance
    public Vector3 scaleMultiplier = Vector3.one; // Échelle des instances

    [Header("Options")]
    public bool autoUpdate = true;                // Mise à jour automatique
    public bool regenerateNow = false;            // Forcer la régénération manuelle

    // Variables internes pour détection des changements
    private GameObject lastPrefab;
    private int lastCountX;
    private int lastCountY;
    private Vector3 lastOffsetX;
    private Vector3 lastOffsetY;
    private Vector3 lastRotationOffset;
    private Vector3 lastScaleMultiplier;

    private void Update()
    {
        if (!Application.isPlaying && autoUpdate)
        {
            if (HasChanged())
            {
                GenerateArray();
                SaveState();
            }
        }

        if (regenerateNow)
        {
            regenerateNow = false;
            GenerateArray();
            SaveState();
        }
    }

    private void Start()
    {
        if (Application.isPlaying && transform.childCount == 0 && prefab != null)
        {
            GenerateArray();
            SaveState();
        }
    }

    private bool HasChanged()
    {
        return prefab != lastPrefab ||
               countX != lastCountX ||
               countY != lastCountY ||
               offsetX != lastOffsetX ||
               offsetY != lastOffsetY ||
               rotationOffset != lastRotationOffset ||
               scaleMultiplier != lastScaleMultiplier;
    }

    private void SaveState()
    {
        lastPrefab = prefab;
        lastCountX = countX;
        lastCountY = countY;
        lastOffsetX = offsetX;
        lastOffsetY = offsetY;
        lastRotationOffset = rotationOffset;
        lastScaleMultiplier = scaleMultiplier;
    }

    public void GenerateArray()
    {
        if (prefab == null)
            return;

        // Supprimer les anciens enfants
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (Application.isPlaying)
                Destroy(transform.GetChild(i).gameObject);
            else
                DestroyImmediate(transform.GetChild(i).gameObject);
        }

        // Génération du mur
        for (int y = 0; y < countY; y++)
        {
            for (int x = 0; x < countX; x++)
            {
                GameObject instance = Instantiate(prefab, transform);
                instance.SetActive(true);

                // Calcul de la position
                Vector3 position = offsetX * x + offsetY * y;
                instance.transform.localPosition = position;
                instance.transform.localRotation = Quaternion.Euler(rotationOffset);
                instance.transform.localScale = scaleMultiplier;

                instance.name = $"{prefab.name}_{y}_{x}";
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (prefab == null) return;

        Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
        Vector3 prefabSize = GetPrefabBounds(prefab);

        for (int y = 0; y < countY; y++)
        {
            for (int x = 0; x < countX; x++)
            {
                Vector3 pos = transform.position + transform.rotation * (offsetX * x + offsetY * y);
                Gizmos.matrix = Matrix4x4.TRS(pos, Quaternion.Euler(rotationOffset), Vector3.one);
                Gizmos.DrawWireCube(Vector3.zero, prefabSize);
            }
        }

        Gizmos.matrix = Matrix4x4.identity;
    }

    private Vector3 GetPrefabBounds(GameObject obj)
    {
        MeshRenderer rend = obj.GetComponentInChildren<MeshRenderer>();
        if (rend != null)
            return rend.bounds.size;
        return Vector3.one;
    }
}

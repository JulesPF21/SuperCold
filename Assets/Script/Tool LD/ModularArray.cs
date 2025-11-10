using UnityEngine;

[ExecuteAlways]
public class ModularArray : MonoBehaviour
{
    [Header("Array Settings")]
    public GameObject prefab;                  // L'asset à dupliquer
    [Min(1)] public int count = 3;             // Nombre de copies
    public Vector3 offset = new Vector3(4, 0, 0); // Espacement entre les copies

    [Header("Transform Options")]
    public Vector3 rotationOffset = Vector3.zero; // Rotation appliquée à chaque instance
    public Vector3 scaleMultiplier = Vector3.one; // Échelle optionnelle (ex: pour varier un peu les tailles)

    [Header("Options")]
    public bool autoUpdate = true;             // Mise à jour automatique
    public bool regenerateNow = false;         // Forcer la régénération

    // Variables internes
    private int lastCount;
    private Vector3 lastOffset;
    private GameObject lastPrefab;
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
               count != lastCount ||
               offset != lastOffset ||
               rotationOffset != lastRotationOffset ||
               scaleMultiplier != lastScaleMultiplier;
    }

    private void SaveState()
    {
        lastPrefab = prefab;
        lastCount = count;
        lastOffset = offset;
        lastRotationOffset = rotationOffset;
        lastScaleMultiplier = scaleMultiplier;
    }

    public void GenerateArray()
    {
        if (prefab == null)
            return;

        // Supprime les anciens enfants
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (Application.isPlaying)
                Destroy(transform.GetChild(i).gameObject);
            else
                DestroyImmediate(transform.GetChild(i).gameObject);
        }

        // Crée les nouvelles instances
        for (int i = 0; i < count; i++)
        {
            GameObject instance = Instantiate(prefab, transform);
            instance.SetActive(true);

            instance.transform.localPosition = offset * i;
            instance.transform.localRotation = Quaternion.Euler(rotationOffset);
            instance.transform.localScale = scaleMultiplier;

            instance.name = prefab.name + "_" + i;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (prefab == null) return;

        Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
        Vector3 prefabSize = GetPrefabBounds(prefab);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = transform.position + transform.rotation * (offset * i);
            Gizmos.matrix = Matrix4x4.TRS(pos, Quaternion.Euler(rotationOffset), Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, prefabSize);
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

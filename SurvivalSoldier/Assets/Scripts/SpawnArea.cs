using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SpawnArea : MonoBehaviour
{

    public Color areaColor = new Color(0, 1, 0, 0.2f);
    public List<Vector3> points = new List<Vector3>();

    public bool canSpawnRock = true;
    public bool canSpawnTree = true;
    public bool canSpawnIron = true;
    public bool canSpawnGold = true;
    public bool canSpawnDiamond = true;

    public int rockCount = 10;
    public int treeCount = 10;
    public int ironCount = 10;
    public int goldCount = 10;
    public int diamondCount = 10;

    public bool resourcesCanStack = false;

    
    void Start()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = areaColor;

        Vector3[] pointsArray = points.ToArray();
        for (int i = 0; i < pointsArray.Length - 1; i++)
        {
            Gizmos.DrawLine(pointsArray[i], pointsArray[i + 1]);
        }
        Gizmos.DrawLine(pointsArray[pointsArray.Length - 1], pointsArray[0]);
    }

    public bool IsPointInPolygon(Vector2 v)
    {
        Vector2[] p = points.ConvertAll(point => new Vector2(point.x, point.z)).ToArray();
        int j = p.Length - 1;
        bool c = false;
        for (int i = 0; i < p.Length; j = i++) c ^= p[i].y > v.y ^ p[j].y > v.y && v.x < (p[j].x - p[i].x) * (v.y - p[i].y) / (p[j].y - p[i].y) + p[i].x;
        return c;
    }

    public Vector3 GetRandomPosition(int loop = 0)
    {
        if (loop > 10)
        {
            Debug.LogError("Could not find a valid position in 10 attempts, try reducing the ammount, or increasing the area");
            return Vector3.zero;
        }
        // Generate random point within area bounds
        float minX = Mathf.Min(points.ConvertAll(point => point.x).ToArray());
        float maxX = Mathf.Max(points.ConvertAll(point => point.x).ToArray());
        float minZ = Mathf.Min(points.ConvertAll(point => point.z).ToArray());
        float maxZ = Mathf.Max(points.ConvertAll(point => point.z).ToArray());

        Vector2 randomPoint;
        float randomX;
        float randomZ;

        int attempts = 0;
        randomX = Random.Range(minX, maxX);
        randomZ = Random.Range(minZ, maxZ);
        randomPoint = new Vector2(randomX, randomZ);
        if (!IsPointInPolygon(randomPoint))
        {
            return GetRandomPosition(loop++);
        }

        float y;
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(randomX, 100, randomZ), Vector3.down, out hit))
        {
            if (hit.collider.gameObject.tag == "Resource" && !resourcesCanStack)
            {
                return GetRandomPosition(loop++);
            }
            y = hit.point.y;
        }
        else
        {
            y = 0;
        }
        if (!IsPointInPolygon(randomPoint))
        {
            y = 2;
        }

        return new Vector3(randomX, y, randomZ);
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(SpawnArea))]
public class SpawnArea3DEditor : Editor
{
    private int selectedIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpawnArea spawnArea = (SpawnArea)target;
        if (GUILayout.Button("Add Point"))
        {
            Undo.RecordObject(spawnArea, "Add Point");
            spawnArea.points.Add(Vector3.zero);
        }
    }

    private void OnSceneGUI()
    {
        SpawnArea spawnArea = (SpawnArea)target;

        for (int i = 0; i < spawnArea.points.Count; i++)
        {
            Vector3 oldPoint = spawnArea.points[i];
            Vector3 newPoint = Handles.PositionHandle(oldPoint, Quaternion.identity);
            if (oldPoint != newPoint)
            {
                Undo.RecordObject(spawnArea, "Move Point");
                spawnArea.points[i] = newPoint;
            }
        }
    }

}
#endif


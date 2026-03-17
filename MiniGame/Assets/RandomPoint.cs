using UnityEngine;
using System.Collections.Generic;

public class UniformPointsOnSphere : MonoBehaviour
{
    [Header("设置")] public int pointCount = 50;
    public GameObject pointPrefab;
    public float radiusOffset = 0.01f;

    [Header("显示设置")] public bool randomizeOrder = true; // 是否打乱生成顺序

    void Start()
    {
        GenerateUniformPoints();
    }

    void GenerateUniformPoints()
    {
        if (pointPrefab == null) return;

        float earthRadius = transform.localScale.x * 0.5f;
        float finalRadius = earthRadius + radiusOffset;

        // 黄金比例
        float phi = Mathf.PI * (3 - Mathf.Sqrt(5)); // 约 2.39996

        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < pointCount; i++)
        {
            float y = 1 - (i / (float)(pointCount - 1)) * 2; // y 从 1 到 -1
            float radiusAtY = Mathf.Sqrt(1 - y * y); // 当前高度的截面半径

            float theta = phi * i; // 黄金角

            float x = Mathf.Cos(theta) * radiusAtY;
            float z = Mathf.Sin(theta) * radiusAtY;

            Vector3 dir = new Vector3(x, y, z).normalized;
            positions.Add(dir);
        }

        // 打乱顺序（让生成的点不是按纬度一层一层的）
        if (randomizeOrder)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                int r = Random.Range(i, positions.Count);
                Vector3 temp = positions[i];
                positions[i] = positions[r];
                positions[r] = temp;
            }
        }

        // 生成点
        foreach (Vector3 dir in positions)
        {
            Vector3 pos = dir * finalRadius;

            GameObject point = Instantiate(pointPrefab, transform);
            point.transform.localPosition = pos;
            point.transform.LookAt(transform.position);
        }
    }
}
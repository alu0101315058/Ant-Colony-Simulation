using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGrid : MonoBehaviour
{
    private Mesh mesh;
    private int width = 1;
    private int height = 1;
    private float cellSize = 5f;
    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateMesh();
    }

    // Update is called once per frame
    void CreateMesh()
    {
        Vector2 quarterSize = new Vector2(width, height) * cellSize / 2;
        Debug.Log("height: " + height + " width: " + width);
        Debug.Log("quarterSize: " + quarterSize);
        int numCells = height * width;
        int numVertices = (width + 1) * (height + 1);

        vertices = new Vector3[4 * numVertices];
        uvs = new Vector2[4 * numVertices];
        triangles = new int[6 * numCells];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++) {
                int index = i * (width + 1) + j;
                vertices[index] = new Vector3(j * cellSize - quarterSize.x, quarterSize.y -  i * cellSize);
                uvs[index] = new Vector2((float)j / width, 1f - (float)i / height);
                int index2 = 6 * (index - i);
                triangles[index2 + 0] = index + 0;
                triangles[index2 + 1] = index + 1;
                triangles[index2 + 2] = index + width + 1;
                triangles[index2 + 3] = index + width + 1;
                triangles[index2 + 4] = index + 1;
                triangles[index2 + 5] = index + width + 2;
            }
        }
        for (int i = 0; i < height; i++) {
            int index = (i+1) * (width + 1) - 1;
            vertices[index] = new Vector3(quarterSize.x, quarterSize.y - i * cellSize);
            uvs[index] = new Vector2(1, 1f - (float)i / height);
        }
        for (int i = 0; i < width; i++) {
            int index = (width + 1) * height + i;
            vertices[index] = new Vector3(i * cellSize - quarterSize.x, -quarterSize.y);
            uvs[index] = new Vector2((float)i / width, 0);
        }
        int finalIndex = (width + 1) * (height + 1) - 1;
        vertices[finalIndex] = new Vector3(quarterSize.x, -quarterSize.y);
        uvs[finalIndex] = new Vector2(1, 0);

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
    }
    void CreateMesh2()
    {
        Vector2 quarterSize = new Vector2(width, height) * cellSize / 2;
        Debug.Log("height: " + height + " width: " + width);
        Debug.Log("quarterSize: " + quarterSize);
        int numCells = height * width;
        int numVertices = (width + 1) * (height + 1);

        vertices = new Vector3[4 * numVertices + numCells];
        uvs = new Vector2[4 * numVertices + numCells];
        triangles = new int[12 * numCells];

        int finalIndex = (width + 1) * (height + 1) - 1;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++) {
                int index = i * (width + 1) + j;
                vertices[index] = new Vector3(j * cellSize - quarterSize.x, quarterSize.y -  i * cellSize);
                uvs[index] = new Vector2((float)j / width, 1f - (float)i / height);
                int centerIndex = finalIndex + i * width + j;
                vertices[centerIndex] = vertices[index] + new Vector3(cellSize / 2, -cellSize / 2);
                uvs[centerIndex] = new Vector2((j + .5f) / width, 1f - (i - .5f) / height);
                int index2 = 12 * (index - i);
                triangles[index2 + 0] = index + width + 1;
                triangles[index2 + 1] = index + 0;
                triangles[index2 + 2] = centerIndex;
                triangles[index2 + 3] = index + 0;
                triangles[index2 + 4] = index + 1;
                triangles[index2 + 5] = centerIndex;
                triangles[index2 + 6] = centerIndex;
                triangles[index2 + 7] = index + 1;
                triangles[index2 + 8] = index + width + 2;
                triangles[index2 + 9] = index + width + 1;
                triangles[index2 + 10] = centerIndex;
                triangles[index2 + 11] = index + width + 2;
            }
        }
        for (int i = 0; i < height; i++) {
            int index = (i+1) * (width + 1) - 1;
            vertices[index] = new Vector3(quarterSize.x, quarterSize.y - i * cellSize);
            uvs[index] = new Vector2(1, 1f - (float)i / height);
        }
        for (int i = 0; i < width; i++) {
            int index = (width + 1) * height + i;
            vertices[index] = new Vector3(i * cellSize - quarterSize.x, -quarterSize.y);
            uvs[index] = new Vector2((float)i / width, 0);
        }
        vertices[finalIndex] = new Vector3(quarterSize.x, -quarterSize.y);
        uvs[finalIndex] = new Vector2(1, 0);

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
    }

    private int[] GetTileVertices(int i, int j) {
        int index = i * (width + 1) + j;
        int[] vertices = new int[4] {
            index,
            index + 1,
            index + width + 1,
            index + width + 2,
            //(width + 1) * (height + 1) - 1 + i * width + j,
        };
        return vertices;
    }
}

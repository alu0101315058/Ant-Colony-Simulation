using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeromoneMask : MonoBehaviour
{
    public float resolution = 1f;
    private float[,] mask;
    public Texture2D texture;
    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        int height = Mathf.RoundToInt(Screen.height * resolution);
        int width = Mathf.RoundToInt(Screen.width * resolution);
        Debug.Log("height: " + height + " width: " + width);
        mask = new float[width,height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                mask[i,j] = 0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            int x = Mathf.RoundToInt((worldPos.x + Screen.width / 2) * resolution);
            int y = Mathf.RoundToInt((worldPos.y + Screen.height / 2) * resolution);
            mask[x,y] = 1f;
        }
    }

    void CreateMesh() {
        mesh = new Mesh();
        mesh.name = "Procedural Grid";
        int height = Mathf.RoundToInt(Screen.height * resolution);
        int width = Mathf.RoundToInt(Screen.width * resolution);
        Vector3[] vertices = new Vector3[width * height];
        Vector2[] uvs = new Vector2[width * height];
        int[] triangles = new int[(width - 1) * (height - 1) * 6];
        int index = 0;
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                vertices[index] = new Vector3(x, y);
                uvs[index] = new Vector2((float)x / width, (float)y / height);
                if (x < width - 1 && y < height - 1) {
                    triangles[index * 6 + 0] = index;
                    triangles[index * 6 + 1] = index + width + 1;
                    triangles[index * 6 + 2] = index + width;
                    triangles[index * 6 + 3] = index;
                    triangles[index * 6 + 4] = index + 1;
                    triangles[index * 6 + 5] = index + width + 1;
                }
                index++;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}

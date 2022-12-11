using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureEdit : MonoBehaviour
{
    private Texture2D originalTexture;
    private Texture2D texture;
    public RenderTexture renderTexture;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        renderTexture = new RenderTexture(10, 10, 0);
        Graphics.Blit(Texture2D.blackTexture, renderTexture);
        texture = toTexture2D(renderTexture);
        Debug.Log(texture.width);
        UpdateMaterial();
    }

    private Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        var old_rt = RenderTexture.active;
        RenderTexture.active = rTex;

        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();

        RenderTexture.active = old_rt;
        return tex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log("hit: " + hit.point);
                texture.SetPixel(5 - (int)hit.point.x, 5 - (int)hit.point.y, Color.red);
                texture.Apply();
                UpdateMaterial();
            }
        }
    }

    void UpdateMaterial() {
        rend.material.SetTexture("_PreviousFrame", rend.material.GetTexture("_NewTexture"));
        rend.material.SetTexture("_NewTexture", texture);
        rend.material.SetFloat("_PreviousTime", rend.material.GetFloat("_UpdateTime"));
        rend.material.SetFloat("_UpdateTime", Time.time);
        texture = toTexture2D(renderTexture);
        texture.Apply();
    }
}

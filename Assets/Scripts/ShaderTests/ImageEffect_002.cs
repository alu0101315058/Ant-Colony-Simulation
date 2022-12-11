using UnityEngine;
using System.Collections;

public class ImageEffect_002 : MonoBehaviour {
    [Range (0,0.99f)]
    public float intensity;
    private Material ImageEffect_002_Mat;
    private RenderTexture pastFrame;

    void Start(){
        ImageEffect_002_Mat = new Material (Shader.Find("Try/ImageEffect_002"));
        GetComponent<Renderer>().material = ImageEffect_002_Mat;
        pastFrame = new RenderTexture (Screen.width, Screen.height, 16);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst){
        Debug.Log("OnRenderImage");
        ImageEffect_002_Mat.SetFloat ("_Intensity", intensity);
        ImageEffect_002_Mat.SetTexture ("_BTex", pastFrame);
        Graphics.Blit (src, dst, ImageEffect_002_Mat);
        Graphics.Blit (RenderTexture.active, pastFrame);
    }
}
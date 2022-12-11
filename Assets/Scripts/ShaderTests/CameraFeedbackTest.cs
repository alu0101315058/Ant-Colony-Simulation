using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFeedbackTest : MonoBehaviour
{
    public Material material;
    public RenderTexture renderTexture;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        if (renderTexture == null) {
            renderTexture = new RenderTexture(256, 256, 0);
            Graphics.Blit(Texture2D.blackTexture, renderTexture);
            material.SetTexture("_Feedback", renderTexture);
        }
        material.SetFloat("_SnapshotTime", -2 * material.GetFloat("_FadingTime"));
        cam.enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Snapshot();
        }
    }

    // Update is called once per frame
    public void Snapshot()
    {
        cam.enabled = true;
        cam.targetTexture = renderTexture;
        RenderTexture.active = renderTexture;
        cam.Render();
        cam.targetTexture = null;
        RenderTexture.active = null;
        material.SetFloat("_SnapshotTime", Time.time);
        cam.enabled = false;
    }
}

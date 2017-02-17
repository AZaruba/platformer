using UnityEngine;
using System.Collections;

public class CameraBehaviorScript : MonoBehaviour {

    public static int h = 320;
    public static float ratio = ((float)Camera.main.pixelHeight /
        (float)Camera.main.pixelWidth);
    public static int w = Mathf.RoundToInt(h * ratio);

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Camera.main.orthographicSize = h / 2f;
        source.filterMode = FilterMode.Point;
        RenderTexture buffer = RenderTexture.GetTemporary(w, h, -1);
        buffer.filterMode = FilterMode.Point;
        Graphics.Blit(source, buffer);
        Graphics.Blit(buffer, destination);
        RenderTexture.ReleaseTemporary(buffer);
    }
}


using UnityEngine;
public class BGImageManager : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] float damper;
    Vector4 val;
    private Vector2 smoothPos;
    private float aspect;
    private float scaleX;
    private float scaleY;
    private Vector2 pos;

    private void UpdateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, 0.03f);
        aspect = (float)Screen.width / (float)Screen.height;
        scaleX = 1f;
        scaleY = 1f;
        if (aspect > 1f)
        {
            scaleY /= aspect;
        }
        if (aspect < 1)
        {
            scaleX *= aspect;
        }
        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
    }

    // Update is called once per frame
    void Update()
    {
        pos = Input.mousePosition;
        pos.x /= Screen.width;
        pos.y /= Screen.height;
        pos *= damper;
        UpdateShader();
    }
}

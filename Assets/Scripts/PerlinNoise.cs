using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    
    [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;
    [SerializeField] private int scale = 20;
    [SerializeField] private float x_offset = 1f;
    [SerializeField] private float y_offset = 1f;

    private void Start () {
        
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenTexture();
    }

    Texture2D GenTexture() {
        Texture2D texture = new Texture2D(width, height);

        // Generate perlin noise map
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Color color = CalcColor(x,y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalcColor(int x, int y) {

        float x_norm = (float)x / width * scale + x_offset;
        float y_norm = (float)y / height * scale + y_offset;

        float col_val = Mathf.PerlinNoise(x_norm, y_norm);
        return new Color(col_val, col_val, col_val);
    }
}

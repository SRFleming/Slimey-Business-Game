using UnityEngine;

public class PerlinNoise {
    
    public static Texture2D GenerateNoiseTex(int width, int height, float scale, float x_offset, float y_offset) {
        // Creating empty texture
        Texture2D texture = new Texture2D(width, height);

        // Generate perlin noise map
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                float x_norm = (float)x / scale + x_offset;
                float y_norm = (float)y / scale + y_offset;
                float col_val = Mathf.PerlinNoise(x_norm, y_norm);
                texture.SetPixel(x, y, new Color(col_val, col_val, col_val));
            }
        }
        texture.Apply();
        return texture;
    }
}

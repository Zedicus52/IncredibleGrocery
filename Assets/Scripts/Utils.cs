using UnityEngine;

public static class Utils 
{
    public static Color SetAplha(Color color, float alpha)
    {
        Color newColor = color;
        newColor.a = alpha;
        return newColor;
    }
}

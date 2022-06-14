using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColors : MonoBehaviour
{
    TrailRenderer trailRenderer;
    Gradient gradient;
    GradientColorKey[] colorKey;
    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        gradient = trailRenderer.colorGradient;
        colorKey = gradient.colorKeys;
        float alpha = 1.0f;

        print("TEST: " + gradient.colorKeys[0].color);
        GradientColorKey[] newColors = shift(gradient.colorKeys); 
        Gradient test = new Gradient();
        test.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] {new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f)}
            );
        print("T? " + newColors[0].color);
        // gradient.SetKeys(newColors, gradient.alphaKeys);
        print("TEST2: " + test.colorKeys[0].color);
        trailRenderer.colorGradient = test;

    }

    private GradientColorKey[] shift(GradientColorKey[] array)
    {
        GradientColorKey[] newArray = new GradientColorKey[array.Length];
        newArray[0] = array[array.Length-1];
        for(var i = 0; i<array.Length-1; i++)
        {
            newArray[i+1] = array[i];
        }
        // print("newArr " + newArray[0].color);
        return newArray;
    }
}

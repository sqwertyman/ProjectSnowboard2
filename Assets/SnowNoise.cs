using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowNoise : MonoBehaviour
{
    public Shader snowfallShader;
    [Range(0.001f, 0.1f)]
    public float flakeAmount;
    [Range(0f, 1f)]
    public float flakeOpacity;

    private Material snowfallMaterial;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        snowfallMaterial = new Material(snowfallShader);
    }

    // Update is called once per frame
    void Update()
    {
        snowfallMaterial.SetFloat("_FlakeAmount", flakeAmount);
        snowfallMaterial.SetFloat("_FlakeOpacity", flakeOpacity);
        RenderTexture snow = (RenderTexture)meshRenderer.material.GetTexture("_Splat");
        RenderTexture temp = RenderTexture.GetTemporary(snow.width, snow.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(snow, temp, snowfallMaterial);
        Graphics.Blit(temp, snow);
        meshRenderer.material.SetTexture("_Splat", snow);
        RenderTexture.ReleaseTemporary(temp);
    }
}

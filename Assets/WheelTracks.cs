using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTracks : MonoBehaviour
{
    public Shader drawShader;
    public GameObject terrain;
    public Transform board;

    [Range(0, 2)]
    public float brushSize;
    [Range(0, 1)]
    public float brushStrength;

    private RenderTexture splatMap;
    private RaycastHit groundHit;
    private Material snowMaterial, drawMaterial;
    private int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");

        drawMaterial = new Material(drawShader);

        snowMaterial = terrain.GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        snowMaterial.SetTexture("_Splat", splatMap);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(board.position, -Vector3.up, out groundHit, 1f, layerMask))
        {
            drawMaterial.SetVector("_Coordinate", new Vector4(groundHit.textureCoord.x, groundHit.textureCoord.y, 0, 0));
            drawMaterial.SetFloat("_Strength", brushStrength);
            drawMaterial.SetFloat("_Size", brushSize);
            RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, temp);
            Graphics.Blit(temp, splatMap, drawMaterial);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}

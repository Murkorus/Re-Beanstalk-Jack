using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infiniteScrolling : MonoBehaviour
{
    public float speed;

    public Color colorOverlay;
    public Material mainMaterial;

    private Renderer bgRenderer;

    private void Start()
    {
        bgRenderer = GetComponent<Renderer>();
        bgRenderer.material.color = bgRenderer.material.color -= colorOverlay;
    }

    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}

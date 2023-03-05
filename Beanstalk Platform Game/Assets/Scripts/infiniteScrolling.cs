using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infiniteScrolling : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private Renderer bgRenderer;
    

    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Скорость перемещения заднего фона

    private SpriteRenderer backgroundSpriteRenderer;
    private float offset;

    void Start()
    {
        backgroundSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        backgroundSpriteRenderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}

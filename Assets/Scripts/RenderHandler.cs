using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderHandler : MonoBehaviour
{
    public enum Direction
    {
        N, // up
        S, // down
        W, // left
        E  // right
    }

    public Direction direction = Direction.E;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
            spriteRenderer.flipX = (direction == Direction.N || direction == Direction.W);
        }
    }
}

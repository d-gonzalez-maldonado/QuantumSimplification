using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SparkBehavior : MonoBehaviour
{
    public Texture2D sparkTexture;
    public SpriteRenderer render;
    private Sprite[] sprites;
    private uint i = 0;
    private System.Random rng = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>(sparkTexture.name);
    }

    // Update is called once per frame
    public void ChangeFrame()
    {
        render.sprite = sprites[i%sprites.Length];
        i++;
    }
}

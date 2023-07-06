using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    private GameObject background;
    private Renderer renderer;
    void Start()
    {
        background = GameObject.Find("Main Border");
        renderer = background.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void updateColor()
    {
        renderer.material.SetColor("_Color", new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f), 1));
    }
}

using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{

    private const float scrollX = 0;
    private const float scrollY = 0.5f;
    private Vector2 offset;
    private Renderer rend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rend = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {

        offset.y -= scrollY * Time.deltaTime;
        if (rend != null)
            rend.material.mainTextureOffset = new Vector2(scrollX, offset.y);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroller : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] float speed;
    [SerializeField] float xScroll, yScroll;

    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(xScroll, yScroll) * speed * Time.deltaTime, image.uvRect.size);
    }
}

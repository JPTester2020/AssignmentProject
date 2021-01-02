using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public static bool isColor = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Bow.isArrow = true;
            Bow.isMouseDown = false;
            Destroy(gameObject, 0.3f);
        }

        if (collision.gameObject.CompareTag("Color"))
        {
            ColorChange(collision);

            if (collision.gameObject.name == Character.nameColor) isColor = false;
            else isColor = true;
        }
    }

    private void ColorChange(Collider2D collision)
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = collision.GetComponent<SpriteRenderer>().color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bow"))
        {
            //GetComponent<PolygonCollider2D>().enabled = false;

            //Bow.isMouseDown = false;

            //Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile : MonoBehaviour
{

    private bool changed;

    void OnMouseUpAsButton()
    {
        ClickedTitle();
    }

    public void ClickedTitle()
    {
        RaycastHit2D hit;

        ToggleColor();

        //Up
        hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.75f, 0), transform.TransformDirection(Vector2.up), 1f);
        if (hit)
        {
            hit.collider.GetComponentInParent<Tile>().ToggleColor();
        }

        //Right
        hit = Physics2D.Raycast(transform.position + new Vector3(0.75f, 0, 0), transform.TransformDirection(Vector2.right), 1f);
        if (hit)
        {
            hit.collider.GetComponentInParent<Tile>().ToggleColor();
        }

        //Down
        hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.75f, 0), transform.TransformDirection(Vector2.down), 1f);
        if (hit)
        {
            hit.collider.GetComponentInParent<Tile>().ToggleColor();
        }

        //Left
        hit = Physics2D.Raycast(transform.position + new Vector3(-0.75f, 0, 0), transform.TransformDirection(Vector2.left), 1f);
        if (hit)
        {
            hit.collider.GetComponentInParent<Tile>().ToggleColor();
        }

        gameObject.GetComponentInParent<TileManager>().CompletionCheck();
    }

    public void ToggleColor ()
    {

        if (changed)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            changed = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            changed = true;
        }
    }
}

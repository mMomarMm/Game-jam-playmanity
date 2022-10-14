using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] Transform mouse, mouseStartPos;
    [SerializeField] Sprite finger, defaultSprite, outSprite;
    [SerializeField] Vector2 xEdge, yEdge;
    [SerializeField] SpriteRenderer sR;
    List<Rigidbody2D> rbs = new List<Rigidbody2D>();
    Camera cam;
    Vector2 mousePos;
    Vector3 scaleOut = Vector3.one * 30f, scaleIn = Vector3.one * 5f;
    bool hovering;

    void Start()
    {
        Cursor.visible = false;
        cam = Camera.main;

        //first in the list is the rb from the cursor and then from the mouse
        rbs.Add(GetComponent<Rigidbody2D>());
        rbs.Add(mouse.GetComponent<Rigidbody2D>());
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        isInsideBounds();
    }

    void FixedUpdate()
    {
        rbs[0].position = mousePos;

        var dir = (Vector2)mouseStartPos.position - mousePos;
        rbs[1].position -= dir.normalized * Time.deltaTime * 2;
    }

    void isInsideBounds()
    {
        //y is the negative or lowest, x is the positive or biggest
        if (Mathf.Abs(mousePos.x) >= xEdge.x || Mathf.Abs(mousePos.x) <= xEdge.y ||
        mousePos.y >= yEdge.x || mousePos.y <= yEdge.y)
        {
            transform.localScale = scaleOut;
            sR.sprite = outSprite;
            return;
        }
        transform.localScale = scaleIn;
        if (!hovering) sR.sprite = defaultSprite;
        else sR.sprite = finger;
    }
    /*void OnMouseEnter()
    {
        hovering = true;
    }
    private void OnMouseExit()
    {
        hovering = false;
    }*/

    void OnTriggerEnter2D(Collider2D other)
    {
        hovering = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        hovering = false;
    }
}
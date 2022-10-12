using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] Transform mouse;

    [SerializeField] List<Vector2> edgeColliderXY = new List<Vector2>();

    [SerializeField] Sprite finger, defaultSprite;

    [SerializeField] BoxCollider2D monitorCollider;
    [SerializeField] Vector2 xEdge, yEdge;
    List<Rigidbody2D> rbs = new List<Rigidbody2D>();
    Camera cam;
    List<Vector2> containersScales = new List<Vector2>();

    Vector2 mousePos;

    void Start()
    {
        Cursor.visible = false;
        cam = Camera.main;

        //first in the list is the rb from the cursor and then from the mouse
        rbs.Add(GetComponent<Rigidbody2D>());
        rbs.Add(mouse.GetComponent<Rigidbody2D>());

        containersScales.Add(CalculateContainerScale(rbs[0].transform, edgeColliderXY[0]));
        containersScales.Add(CalculateContainerScale(rbs[1].transform, edgeColliderXY[1]));
    }

    Vector2 CalculateContainerScale(Transform t, Vector2 ed)
    {
        if (!t.TryGetComponent(out BoxCollider2D box))
            return Vector2.zero;
        Vector2 i;
        i.x = ed.x / box.size.x;
        i.y = ed.y / box.size.y;
        return i;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (!isInsideBounds())
        {
            return;
        }

        rbs[0].position = mousePos;
        //2 = rbs object count
        /* for (int i = 0; i < 2; i++)
         {
             dir.x *= containersScales[i].x;
             dir.y *= containersScales[i].y;
             rbs[i].position += dir * Time.deltaTime;
         }*/
    }

    bool isInsideBounds()
    {
        //y is the negative or lowest, x is the positive or biggest
        if (Mathf.Abs(mousePos.x) >= xEdge.x || Mathf.Abs(mousePos.x) <= xEdge.y)
        {
            return false;
        }
        if (mousePos.y >= yEdge.x || mousePos.y <= yEdge.y)
        {
            return false;
        }

        return true;
    }
}

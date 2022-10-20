using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlasters : MonoBehaviour
{
    [SerializeField] GameObject blasterPrefab;
    [SerializeField] float timeBetween, maxNumber, minY, maxY, boundY;
    [SerializeField] Vector2 XBounds, xNoBounds;
    float currentTime;
    int currentIndex;
    void Update()
    {
        if (currentTime >= timeBetween)
        {
            Spawn();
            currentTime = 0;
            currentIndex += 1;
            if (currentIndex == maxNumber) gameObject.SetActive(false);
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
    void Spawn()
    {
        Transform b = Instantiate(blasterPrefab, transform).transform;
        Vector3 pos;
        pos.z = 0;
        pos.y = Random.Range(minY, maxY);
        Vector2 xRange;
        if (pos.y > boundY) xRange = xNoBounds;
        else xRange = XBounds;
        int dir;
        if (Random.Range(1, 0) == 0) dir = 1;
        else dir = -1;
        pos.x = Random.Range(xRange.x, xRange.y) * dir;
        b.position = pos;

        var direction = b.position -undertaleFightManager.thisScript.player.transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        b.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
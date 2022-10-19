using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlasters : MonoBehaviour
{
    [SerializeField] GameObject blasterPrefab;
    [SerializeField] List<float> times;
    float currentTime;
    int currentIndex;
    void Update()
    {
        if (currentTime >= times[currentIndex])
        {
            Spawn();
            currentTime = 0;
            currentIndex += 1;
            if (currentIndex == times.Count) gameObject.SetActive(false);
        }
        else
        {
            currentTime += times[currentIndex];
        }
    }
    void Spawn()
    { }
}
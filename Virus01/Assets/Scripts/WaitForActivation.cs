using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForActivation : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;
    [SerializeField] List<float> timeActivation;
    private void Start()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            StartCoroutine(ActivateObject(i));
        }
    }
    IEnumerator ActivateObject(int index)
    {
        yield return new WaitForSeconds(timeActivation[index]);
        objects[index].SetActive(true);
    }
}
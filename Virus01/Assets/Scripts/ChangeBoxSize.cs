using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBoxSize : MonoBehaviour
{
    [SerializeField] bool shrink;
    void Start()
    {
        undertaleFightManager.thisScript.box.TryGetComponent(out Animator a);
        if (shrink)
        {
            a.SetBool("shrinkBox", true);
            a.SetBool("enlargeBox", false);
        }
        else
        {
            a.SetBool("shrinkBox", false);
            a.SetBool("enlargeBox", true);
        }
    }
}

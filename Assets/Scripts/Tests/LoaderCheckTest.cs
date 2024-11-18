using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCheckTest : WrappedMonoBehaviour
{
    protected override void OnStart(MonoBehaviour self, List<Object> assets)
    {
        Debug.Log($"{((LoaderCheck)assets[0]).messageOnLoad} (from {self})");
    }
}

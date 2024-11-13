using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.AssetLoadingManagement;

public class AssetLoadingTest : MonoBehaviour
{
    private void Start()
    {
        var result = this.HandleAddressableAsset<LoaderCheck>();
        Debug.Log(result.messageOnLoad);
    }
}

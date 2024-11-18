using static Core.Utils.GameLogicManagement.AssetProcessing;
using Core.Utils.NamingConventions;
using System.Linq;
using UnityEngine;

public class CustomBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DeployAssets(LoadAssetsAsync<UnityEngine.Object>(AssetGroupLabel.Default), handle =>
        {
            var assets = handle.Result.OrderBy(asset => asset.name).ToList();
            // Your code here
            handle.ReleaseOnDestroy(this);
        }
        ));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

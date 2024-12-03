using Core.Utils.ExtensionMethods;
using Core.Utils.NamingConventions;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class AssetDeployer : MonoBehaviour
{
    [SerializeField] private AssetGroupLabel assetGroupLabel = AssetGroupLabel.Default;

    private void Start()
    {
        StartCoroutine(DeployAssets(LoadAssetsAsync<UnityEngine.Object>(assetGroupLabel), (handle) =>
        {
            PrepareTarget(AtFirstMakeTarget(handle.Result.OrderBy(asset => asset.name))).OnDestroyRelease(handle);
        }
        ));
    }

    protected virtual GameObject PrepareTarget(UnityEngine.Object[] assets)
    {
        return (GameObject)assets.First();
    }

    private IEnumerator<WaitUntil> DeployAssets<T>(AsyncOperationHandle<T> handle, Action<AsyncOperationHandle<T>> callback)
        where T : IList<UnityEngine.Object>
    {
        yield return new WaitUntil(() => handle.Status == AsyncOperationStatus.Succeeded);
        callback(handle);
    }

    private AsyncOperationHandle<IList<T>> LoadAssetsAsync<T>(AssetGroupLabel label)
        where T : UnityEngine.Object
    {
        return Addressables.LoadAssetsAsync<T>(label.ToString(), null, true);
    }

    private UnityEngine.Object[] AtFirstMakeTarget(IEnumerable<UnityEngine.Object> assets)
    {
        return new UnityEngine.Object[]
        {
            Instantiate((GameObject)assets.First())
        }
        .Concat(assets.Where((_, index) => index > 0))
        .ToArray();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

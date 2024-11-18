using Core.Utils.ExtensionMethods;
using Core.Utils.NamingConventions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class WrappedMonoBehaviour : MonoBehaviour
{
    [SerializeField] private AssetGroupLabel assetGroupLabel = AssetGroupLabel.Default;

    private void Start()
    {
        StartCoroutine(DeployAssets(this, LoadAssetsAsync<UnityEngine.Object>(assetGroupLabel), (associatedMonoBehaviour, handle) =>
        {
            OnStart(associatedMonoBehaviour, handle.Result.OrderBy(asset => asset.name).ToList());
            handle.ReleaseOnDestroy(associatedMonoBehaviour);
        }
        ));
    }

    protected abstract void OnStart(MonoBehaviour self, List<UnityEngine.Object> assets);

    private IEnumerator<WaitUntil> DeployAssets<T>(MonoBehaviour associatedMonoBehaviour, AsyncOperationHandle<IList<T>> handle, Action<MonoBehaviour, AsyncOperationHandle<IList<T>>> callback)
        where T : UnityEngine.Object
    {
        yield return new WaitUntil(() => handle.Status == AsyncOperationStatus.Succeeded);
        callback(associatedMonoBehaviour, handle);
    }

    private AsyncOperationHandle<IList<T>> LoadAssetsAsync<T>(AssetGroupLabel label)
        where T : UnityEngine.Object
    {
        return Addressables.LoadAssetsAsync<T>(label.ToString(), null, true);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

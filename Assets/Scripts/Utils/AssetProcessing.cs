using Core.Utils.NamingConventions;
using R3;
using R3.Triggers;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Utils.GameLogicManagement
{
    public static class AssetProcessing
    {
        public static IEnumerator<WaitUntil> DeployAssets<T>(AsyncOperationHandle<IList<T>> handle, Action<AsyncOperationHandle<IList<T>>> callback)
            where T : UnityEngine.Object
        {
            yield return new WaitUntil(() => handle.Status == AsyncOperationStatus.Succeeded);
            callback(handle);
        }

        public static AsyncOperationHandle<IList<T>> LoadAssetsAsync<T>(AssetGroupLabel label)
            where T : UnityEngine.Object
        {
            return Addressables.LoadAssetsAsync<T>(label.ToString(), null, true);
        }

        public static void ReleaseOnDestroy<T>(this AsyncOperationHandle<T> handle, MonoBehaviour monoBehaviour)
        {
            monoBehaviour.AddComponent<ObservableDestroyTrigger>().OnDestroyAsObservable().Subscribe(_ => handle.Release()).AddTo(monoBehaviour);
        }
    }
}

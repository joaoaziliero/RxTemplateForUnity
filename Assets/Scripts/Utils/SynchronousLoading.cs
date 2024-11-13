using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utils.AssetLoadingManagement
{
    public static class SynchronousLoading
    {
        public static T HandleAddressableAsset<T>(this MonoBehaviour monoBehaviour) where T : UnityEngine.Object
        {
            var handle = Addressables.LoadAssetAsync<T>(typeof(T).Name);
            handle.ReleaseOnDestroy(monoBehaviour);
            return handle.WaitForCompletion();
        }

        private static void ReleaseOnDestroy<T>(this AsyncOperationHandle<T> asyncOperationHandle, MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject
                .AddComponent<ObservableDestroyTrigger>()
                .OnDestroyAsObservable()
                .Subscribe(_ => asyncOperationHandle.Release())
                .AddTo(monoBehaviour);
        }
    }
}

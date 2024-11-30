using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Utils.ExtensionMethods
{
    public static class CustomResourceManagement
    {
        public static void OnDestroyRelease<T>(this GameObject gameObject, AsyncOperationHandle<T> handle)
        {
            gameObject.AddComponent<ObservableDestroyTrigger>()
                .OnDestroyAsObservable()
                .Subscribe(_ => handle.Release())
                .AddTo(gameObject);
        }
    }
}

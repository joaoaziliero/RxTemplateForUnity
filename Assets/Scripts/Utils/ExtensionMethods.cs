using R3;
using R3.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Utils.ExtensionMethods
{
    public static class MemoryManagement
    {
        public static void ReleaseOnDestroy<T>(this AsyncOperationHandle<T> handle, MonoBehaviour monoBehaviour)
        {
            monoBehaviour.AddComponent<ObservableDestroyTrigger>()
                .OnDestroyAsObservable()
                .Subscribe(_ => handle.Release())
                .AddTo(monoBehaviour);
        }
    }
}

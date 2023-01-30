using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SceneManagement
{
    public abstract class BaseSceneController : MonoBehaviour
    {
        public abstract UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress);
        public virtual void OnLoadComplete() {}

        public virtual async UniTask Unload()
        {
            await UniTask.Yield();
        }
        
        public virtual void OnUnloadComplete() {}
    }
}

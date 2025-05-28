using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Abhishek.Utils{
    public interface IPooledObject{
        void OnObjectReuse();
        void SetOrignalParent(Transform orignalTransform);
        void DestroyMySelfWithDelay(float delay = 0f);
        void DestroyNow();
    }
}

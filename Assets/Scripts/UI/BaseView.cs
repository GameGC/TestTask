using UnityEngine;

namespace UI
{
    public abstract class BaseView<T> : MonoBehaviour
    {
        public abstract void SetModel(T model);
    }
}
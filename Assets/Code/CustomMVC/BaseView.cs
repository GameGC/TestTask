using UnityEngine;

public abstract class BaseView<T> : MonoBehaviour
{
    public abstract void SetModel(T model);
}
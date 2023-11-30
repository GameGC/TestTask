using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseView<T> : MonoBehaviour
{
    public abstract void SetModel(T model);
}
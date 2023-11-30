using System.Collections.Generic;
using UI;
using UnityEngine;

public class LeaderBoardView : BaseView<KeyValuePair<string, int>[]>
{
    public LeaderBoardElementView prefab;
    public Transform contentParent;

    private List<LeaderBoardElementView> pool = new List<LeaderBoardElementView>(capacity:3);

    private void Awake()
    {
        while (pool.Count < pool.Capacity)
        {
            var instance = Instantiate(prefab, contentParent);
            instance.gameObject.SetActive(false);
            pool.Add(instance);
        }
    }

    public override void SetModel(KeyValuePair<string, int>[] model)
    {
        if (pool.Count < 0)
            foreach (var keyValuePair in model)
            {
                var instance = Instantiate(prefab, contentParent);
                instance.SetModel(keyValuePair);
                pool.Add(instance);
            }
        else
        {
            if (pool.Count > model.Length)
            {
                for (int i = pool.Count - 1; i >= model.Length; i--) 
                    pool[i].gameObject.SetActive(false);
            }
            else while(pool.Count < model.Length)
            {
                var instance = Instantiate(prefab, contentParent);
                pool.Add(instance);
            }

            for (int i = 0; i < model.Length; i++)
            {
                pool[i].SetModel(model[i]);
                pool[i].gameObject.SetActive(true);
            }
        }
    }
}
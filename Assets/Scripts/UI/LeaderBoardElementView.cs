using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardElementView : BaseView<KeyValuePair<string,int>>
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text scoreText;
    
    public override void SetModel(KeyValuePair<string, int> model)
    {
        nameText.text = model.Key;
        scoreText.text = model.Value.ToString();
    }
}
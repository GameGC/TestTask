using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class LeaderBoardElementView : BaseView<KeyValuePair<string,int>>
{
    public Text nameText;
    public Text scoreText;
    
    public override void SetModel(KeyValuePair<string, int> model)
    {
        nameText.text = model.Key;
        scoreText.text = model.Value.ToString();
    }
}
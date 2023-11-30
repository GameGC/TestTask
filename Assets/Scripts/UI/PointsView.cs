using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// in current case we have just one param in model,
    /// so no custom class
    /// </summary>
    public class PointsView : BaseView<int>
    {
        [SerializeField] private Text pointsText;
    
        public override void SetModel(int model)
        {
            pointsText.text = $"Points: {model}";
        }
    }
}
using UnityEngine.UI;

/// <summary>
/// in current case we have just one param in model,
/// so no custom class
/// </summary>
public class HealthView : BaseView<int>
{
    public Text healthText;
    
    public override void SetModel(int model)
    {
        healthText.text = $"Health: {model}";
    }
}
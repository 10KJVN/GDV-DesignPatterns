using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour, ILoadingScreen
{
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private Canvas _loadingScreenCanvas;
    
    public void Show()
    {
        _loadingScreenCanvas.enabled = true;
    }

    public void Hide()
    {
        _loadingScreenCanvas.enabled = false;
    }

    public void ResetSlider()
    {
        _loadingSlider.value = 0;
    }

    public Awaitable SetLoadingSlider(float value)
    {
        throw new System.NotImplementedException();
    }

    public void SetBarPercent(float percent)
    {
        _loadingSlider.value = percent;
    }
}
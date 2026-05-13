using UnityEngine;

public interface ILoadingScreen
{
    public void Show();
    public void Hide();
    public void ResetSlider();
    Awaitable SetLoadingSlider(float value);
}
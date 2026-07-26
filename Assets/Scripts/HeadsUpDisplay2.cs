using System;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay2
{
    private Button[] buttons = null;

    public delegate void ButtonPressedEvent (int index);
    public static event ButtonPressedEvent OnButtonPressed2;

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener( () => HandleButtonPress(index));
        }
    }

    public void CreateButtons(Transform canvasPos, string btnText)
    {
        GameObject buttonGO = new("CodeGeneratedButton");
        RectTransform rectTransform = buttonGO.AddComponent<RectTransform>();

        rectTransform.SetParent(canvasPos, false);
        rectTransform.sizeDelta = new Vector2(160, 30);
    }

    private void HandleButtonPress(int index) => OnButtonPressed2(index);
}
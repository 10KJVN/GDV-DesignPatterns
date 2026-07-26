using System;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay2
{
    private Button[] buttons = null;
    private GameObject buttonGO;
    private Image img;

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
        buttonGO = new("CodeGeneratedButton");
        RectTransform rectTransform = buttonGO.AddComponent<RectTransform>();

        rectTransform.SetParent(canvasPos, false);
        rectTransform.sizeDelta = new Vector2(160, 30);

        
        //image.sprite = Resources.Load<Sprite>("Resources/unity_builtin_extra/UISprite");
    }

    public void AssignSprite(Sprite target)
    {
        img = buttonGO.AddComponent<Image>();
        img.sprite = target;
        img.color = Color.red;
    }

    public void ConfigureButton()
    {
        Button btn = buttonGO.AddComponent<Button>();
        btn.targetGraphic = img;
    }

    private void HandleButtonPress(int index) => OnButtonPressed2(index);
}
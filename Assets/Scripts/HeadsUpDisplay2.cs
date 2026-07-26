using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay2 : IEntity
{
    private List<Button> buttons = new();
    private GameObject buttonGO;
    private Image img;

    private Button fireBtn;

    public delegate void ButtonPressedEvent (int index);
    public static event ButtonPressedEvent OnButtonPressed2;

    private void AssignListeners()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener( () => HandleButtonPress(index));
        }
    }
    public void OnStart()
    {
        RegisterButtonsToArray();
        AssignListeners();
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
        fireBtn = buttonGO.AddComponent<Button>();
        fireBtn.targetGraphic = img;
    }

    private void RegisterButtonsToArray()
    {
        buttons.Add(fireBtn);
    }

    //public void

    private void HandleButtonPress(int index) => OnButtonPressed2(index);


    public void OnUpdate()
    {
        throw new NotImplementedException();
    }
}
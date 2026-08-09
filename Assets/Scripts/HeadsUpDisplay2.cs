using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class takes care of creating the buttons to cast spells.
/// It assigns listeners and executes the spell on that referenced index.
/// </summary>

public class HeadsUpDisplay2 : IEntity
{
    private List<Button> buttons = new();
    private GameObject buttonGO;
    private GameObject btn2;
    private GameObject btn3;
    private Image img;
    private Image img2;

    private Button fireBtn;
    private Button iceBtn;

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
        rectTransform.sizeDelta = new Vector2(200, 40);

        btn2 = new GameObject("SecondButton");
        var rt2 = btn2.AddComponent<RectTransform>();
        
        rt2.SetParent(canvasPos, false);
        rt2.sizeDelta = new Vector2(200, 40);
        rt2.position = new Vector2(400, 40);
    }

    public void AssignSprite(Sprite target)
    {
        img = buttonGO.AddComponent<Image>();
        img.sprite = target;
        img.color = Color.red;
        
        img2 = btn2.AddComponent<Image>();
        img2.sprite = target;
        img2.color = Color.blue;
    }

    public void ConfigureButton()
    {
        fireBtn = buttonGO.AddComponent<Button>();
        fireBtn.targetGraphic = img;
        
        iceBtn = btn2.AddComponent<Button>();
        iceBtn.targetGraphic = img2;
    }

    private void RegisterButtonsToArray()
    {
        buttons.Add(fireBtn);
        buttons.Add(iceBtn);
    }

    private void HandleButtonPress(int index) => OnButtonPressed2?.Invoke(index);


    public void OnUpdate()
    {
        throw new NotImplementedException();
    }
}
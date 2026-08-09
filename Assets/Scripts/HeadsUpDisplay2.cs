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
    private GameObject btn1;
    private GameObject btn2;
    private GameObject btn3;
    
    private Image img;
    private Image img2;
    private Image img3;

    private Button fireBtn;
    private Button iceBtn;
    private Button orbitBtn;
    private List<Button> buttons = new();

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
        btn1 = new("CodeGeneratedButton");
        RectTransform rectTransform = btn1.AddComponent<RectTransform>();

        rectTransform.SetParent(canvasPos, false);
        rectTransform.sizeDelta = new Vector2(200, 80);
        rectTransform.position = new Vector2(200, 50);

        btn2 = new GameObject("SecondButton");
        var rt2 = btn2.AddComponent<RectTransform>();
        
        rt2.SetParent(canvasPos, false);
        rt2.sizeDelta = new Vector2(200, 80);
        rt2.position = new Vector2(400, 50);
        
        btn3 = new GameObject("ThirdButton");
        var rt3 = btn3.AddComponent<RectTransform>();
        rt3.SetParent(canvasPos, false);
        rt3.sizeDelta = new Vector2(200, 80);
        rt3.position = new Vector2(600, 50);
    }

    public void AssignSprite(Sprite target)
    {
        img = btn1.AddComponent<Image>();
        img.sprite = target;
        img.color = Color.red;
        
        img2 = btn2.AddComponent<Image>();
        img2.sprite = target;
        img2.color = Color.blue;
        
        img3 = btn3.AddComponent<Image>();
        img3.sprite = target;
        img3.color = Color.green;
    }

    public void ConfigureButton()
    {
        fireBtn = btn1.AddComponent<Button>();
        fireBtn.targetGraphic = img;
        
        iceBtn = btn2.AddComponent<Button>();
        iceBtn.targetGraphic = img2;
        
        orbitBtn = btn3.AddComponent<Button>();
        orbitBtn.targetGraphic = img3;
    }

    private void RegisterButtonsToArray()
    {
        buttons.Add(fireBtn);
        buttons.Add(iceBtn);
        buttons.Add(orbitBtn);
    }

    private void HandleButtonPress(int index) => OnButtonPressed2?.Invoke(index);


    public void OnUpdate()
    {
        throw new NotImplementedException();
    }
}
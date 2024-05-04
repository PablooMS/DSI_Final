using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using UnityEditor;

public class MainScript : MonoBehaviour
{
    VisualElement characterPageSelector;
    VisualElement settingsPageSelector;
    VisualElement characterPage;
    VisualElement settingsPage;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        characterPageSelector = root.Q<VisualElement>("CharacterMark");
        settingsPageSelector = root.Q<VisualElement>("SettingsMark");
        characterPage = root.Q<VisualElement>("Menu1");
        settingsPage = root.Q<VisualElement>("Menu2");

        characterPageSelector.RegisterCallback<ClickEvent>(ChangeToCharacters);
        settingsPageSelector.RegisterCallback<ClickEvent>(ChangeToSettings);
        characterPageSelector.AddManipulator(new ButtonManipulator());
        settingsPageSelector.AddManipulator(new ButtonManipulator());

        //[...]

        characterPage.style.display = DisplayStyle.Flex;
        settingsPage.style.display = DisplayStyle.None;
    }

    private void ChangeToSettings(ClickEvent e)
    {

    }
    
    private void ChangeToCharacters(ClickEvent e)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

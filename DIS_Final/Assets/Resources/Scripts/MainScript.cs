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

    bool characterActive;

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
        //TODO: Añadir los manipuladores a las instancias una vez hayamos terminado la template de personaje
        //y tengamos las templates unpackeadas

        //[...]

        characterPage.style.display = DisplayStyle.Flex;
        settingsPage.style.display = DisplayStyle.None;
        characterActive = true;
    }

    private void ChangeToSettings(ClickEvent e)
    {
        if(characterActive)
        {
            characterPage.style.display = DisplayStyle.None;
            settingsPage.style.display = DisplayStyle.Flex;
            characterPageSelector.RemoveFromClassList("marks");
            characterPageSelector.AddToClassList("unactivemarks");
            settingsPageSelector.RemoveFromClassList("unactivemarks");
            settingsPageSelector.AddToClassList("marks");
            characterActive = false;
        }
    }
    
    private void ChangeToCharacters(ClickEvent e)
    {
        if (!characterActive)
        {
            characterPage.style.display = DisplayStyle.Flex;
            settingsPage.style.display = DisplayStyle.None;
            characterPageSelector.RemoveFromClassList("unactivemarks");
            characterPageSelector.AddToClassList("marks");
            settingsPageSelector.RemoveFromClassList("marks");
            settingsPageSelector.AddToClassList("unactivemarks");
            characterActive = true;
        }
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

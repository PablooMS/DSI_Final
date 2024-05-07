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
    VisualElement equipPageSelector;
    VisualElement itemPageSelector;
    VisualElement settingsPageSelector;

    VisualElement characterPage;
    VisualElement equipPage;
    VisualElement itemPage;
    VisualElement settingsPage;

    int pageActive;

    StatDisplay atkDisplay;
    StatDisplay defDisplay;
    StatDisplay spdDisplay;
    StatDisplay matkDisplay;
    StatDisplay mdefDisplay;

    VisualElement char1;
    VisualElement char2;
    VisualElement char3;
    VisualElement char4;

    Personaje perSelec;
    List<Personaje> list_per;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        characterPageSelector = root.Q<VisualElement>("CharacterMark");
        equipPageSelector = root.Q<VisualElement>("EquipmentMark");
        itemPageSelector = root.Q<VisualElement>("ItemsMark");
        settingsPageSelector = root.Q<VisualElement>("SettingsMark");

        characterPage = root.Q<VisualElement>("Menu1");
        equipPage = root.Q<VisualElement>("Menu2");
        itemPage = root.Q<VisualElement>("Menu3");
        settingsPage = root.Q<VisualElement>("Menu4");

        atkDisplay = characterPage.Q<StatDisplay>("atkDis");
        defDisplay = characterPage.Q<StatDisplay>("defDis");
        spdDisplay = characterPage.Q<StatDisplay>("spdDis");
        matkDisplay = characterPage.Q<StatDisplay>("matkDis");
        mdefDisplay = characterPage.Q<StatDisplay>("mdefDis");

        char1 = characterPage.Q<VisualElement>("Sans");
        char2 = characterPage.Q<VisualElement>("Once");
        char3 = characterPage.Q<VisualElement>("Triton");
        char4 = characterPage.Q<VisualElement>("Yoel");

        characterPageSelector.RegisterCallback<ClickEvent>(ChangeToCharacters);
        equipPageSelector.RegisterCallback<ClickEvent>(ChangeToEquipment);
        itemPageSelector.RegisterCallback<ClickEvent>(ChangeToItems);
        settingsPageSelector.RegisterCallback<ClickEvent>(ChangeToSettings);
        characterPageSelector.AddManipulator(new ButtonManipulator());
        equipPageSelector.AddManipulator(new ButtonManipulator());
        itemPageSelector.AddManipulator(new ButtonManipulator());
        settingsPageSelector.AddManipulator(new ButtonManipulator());

        //TODO: Añadir los manipuladores a las instancias una vez hayamos terminado la template de personaje
        //y tengamos las templates unpackeadas
        InitPersonajes();

        //[...]

        characterPage.style.display = DisplayStyle.Flex;
        equipPage.style.display = DisplayStyle.None;
        itemPage.style.display = DisplayStyle.None;
        settingsPage.style.display = DisplayStyle.None;
        pageActive = 0;
    }

    private void InitPersonajes()
    {
        list_per = new List<Personaje>();
        Personaje sans = new Personaje("Sans", 1, 2, 0, 2, 0, 3, "", "");
        Personaje logcel = new Personaje("Once-ler", 34, 3, 1, 0, 3, 2, "", "");
        Personaje tritonman = new Personaje("Tritonman", 69, 2, 1, 1, 2, 0, "", "");
        Personaje yoel = new Personaje("Yoel", 20, 1, 1, 3, 2, 2, "", "");

        list_per.Add(sans);
        list_per.Add(logcel);
        list_per.Add(tritonman);
        list_per.Add(yoel); 

        char1.userData = sans;
        char2.userData = logcel;
        char3.userData = tritonman;
        char4.userData = yoel;

        char1.RegisterCallback<ClickEvent>(selecPersonaje);
        char2.RegisterCallback<ClickEvent>(selecPersonaje);
        char3.RegisterCallback<ClickEvent>(selecPersonaje);
        char4.RegisterCallback<ClickEvent>(selecPersonaje);
    }

    private void selecPersonaje(ClickEvent e)
    {
        VisualElement person = e.target as VisualElement;
        perSelec = person.userData as Personaje;

        atkDisplay.Estado = perSelec.Atk;
        defDisplay.Estado = perSelec.Def;
        spdDisplay.Estado = perSelec.Speed;
        matkDisplay.Estado = perSelec.Matk;
        mdefDisplay.Estado = perSelec.Mdef;
    }

    private void ChangeToCharacters(ClickEvent e)
    {
        if (pageActive != 0)
        {
            switch (pageActive)
            {
                case 1:
                    equipPage.style.display = DisplayStyle.None;
                    equipPageSelector.RemoveFromClassList("marks");
                    equipPageSelector.AddToClassList("unactivemarks");
                    break;
                case 2:
                    itemPage.style.display = DisplayStyle.None;
                    itemPageSelector.RemoveFromClassList("marks");
                    itemPageSelector.AddToClassList("unactivemarks");
                    break;
                case 3:
                    settingsPage.style.display = DisplayStyle.None;
                    settingsPageSelector.RemoveFromClassList("marks");
                    settingsPageSelector.AddToClassList("unactivemarks");
                    break;
                default:
                    break;
            }
            characterPage.style.display = DisplayStyle.Flex;
            characterPageSelector.RemoveFromClassList("unactivemarks");
            characterPageSelector.AddToClassList("marks");
            pageActive = 0;
        }
    }

    private void ChangeToEquipment(ClickEvent e)
    {
        if (pageActive != 1)
        {
            switch (pageActive)
            {
                case 0:
                    characterPage.style.display = DisplayStyle.None;
                    characterPageSelector.RemoveFromClassList("marks");
                    characterPageSelector.AddToClassList("unactivemarks");
                    break;
                case 2:
                    itemPage.style.display = DisplayStyle.None;
                    itemPageSelector.RemoveFromClassList("marks");
                    itemPageSelector.AddToClassList("unactivemarks");
                    break;
                case 3:
                    settingsPage.style.display = DisplayStyle.None;
                    settingsPageSelector.RemoveFromClassList("marks");
                    settingsPageSelector.AddToClassList("unactivemarks");
                    break;
                default:
                    break;
            }
            equipPage.style.display = DisplayStyle.Flex;
            equipPageSelector.RemoveFromClassList("unactivemarks");
            equipPageSelector.AddToClassList("marks");
            pageActive = 1;
        }
    }

    private void ChangeToItems(ClickEvent e)
    {
        if (pageActive != 2)
        {
            switch (pageActive)
            {
                case 0:
                    characterPage.style.display = DisplayStyle.None;
                    characterPageSelector.RemoveFromClassList("marks");
                    characterPageSelector.AddToClassList("unactivemarks");
                    break;
                case 1:
                    equipPage.style.display = DisplayStyle.None;
                    equipPageSelector.RemoveFromClassList("marks");
                    equipPageSelector.AddToClassList("unactivemarks");
                    break;
                case 3:
                    settingsPage.style.display = DisplayStyle.None;
                    settingsPageSelector.RemoveFromClassList("marks");
                    settingsPageSelector.AddToClassList("unactivemarks");
                    break;
                default:
                    break;
            }
            itemPage.style.display = DisplayStyle.Flex;
            itemPageSelector.RemoveFromClassList("unactivemarks");
            itemPageSelector.AddToClassList("marks");
            pageActive = 2;
        }
    }

    private void ChangeToSettings(ClickEvent e)
    {
        if(pageActive != 3)
        {
            switch (pageActive)
            {
                case 0:
                    characterPage.style.display = DisplayStyle.None;
                    characterPageSelector.RemoveFromClassList("marks");
                    characterPageSelector.AddToClassList("unactivemarks");
                    break;
                case 1:
                    equipPage.style.display = DisplayStyle.None;
                    equipPageSelector.RemoveFromClassList("marks");
                    equipPageSelector.AddToClassList("unactivemarks");
                    break;
                case 2:
                    itemPage.style.display = DisplayStyle.None;
                    itemPageSelector.RemoveFromClassList("marks");
                    itemPageSelector.AddToClassList("unactivemarks");
                    break;
                default:
                    break;
            }
           
            settingsPage.style.display = DisplayStyle.Flex;
            settingsPageSelector.RemoveFromClassList("unactivemarks");
            settingsPageSelector.AddToClassList("marks");
            pageActive = 3;
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using UnityEditor;
using TreeEditor;

public class MainScript : MonoBehaviour
{
    List<string> _objsA = new List<string>()
        {
            "EspadaSagrada",
            "ChanclaMaldita",
            "Nada"
        };
    List<string> _objsB = new List<string>()
        {
            "EscudoDiamante",
            "Creeper",
            "Nada"
        };


    VisualElement characterPageSelector;
    VisualElement equipPageSelector;
    VisualElement itemPageSelector;
    VisualElement settingsPageSelector;

    VisualElement characterPage;
    VisualElement equipPage;
    VisualElement itemPage;
    VisualElement settingsPage;


    VisualElement sansEquipPage;
    VisualElement onceEquipPage;
    VisualElement trironEquipPage;
    VisualElement yoelEquipPage;

    DropdownField _listaObjsASans, _listaObjsBSans;

    DropdownField _listaObjsAOnce, _listaObjsBOnce;

    DropdownField _listaObjsATriton, _listaObjsBTriton;

    DropdownField _listaObjsAYoel, _listaObjsBYoel;

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
    
    VisualElement nameAndLevelStatsDisplay;

    Label name, level;

    VisualElement char5, char6, char7, char8;
    
    VisualElement desc1, desc2, desc3, desc4;

    Personaje per1, per2, per3, per4;

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

        sansEquipPage = root.Q<VisualElement>("SansEquipDisplay");
        onceEquipPage = root.Q<VisualElement>("OnceEquipDisplay");
        trironEquipPage = root.Q<VisualElement>("TritonEquipDisplay");
        yoelEquipPage = root.Q<VisualElement>("YoelEquipDisplay");

        atkDisplay = characterPage.Q<StatDisplay>("atkDis");
        defDisplay = characterPage.Q<StatDisplay>("defDis");
        spdDisplay = characterPage.Q<StatDisplay>("spdDis");
        matkDisplay = characterPage.Q<StatDisplay>("matkDis");
        mdefDisplay = characterPage.Q<StatDisplay>("mdefDis");

        char1 = characterPage.Q<VisualElement>("Sans");
        char2 = characterPage.Q<VisualElement>("Once");
        char3 = characterPage.Q<VisualElement>("Triton");
        char4 = characterPage.Q<VisualElement>("Yoel");

        nameAndLevelStatsDisplay = characterPage.Q<VisualElement>("selectedCharacter");
        
        name = characterPage.Q<Label>("name");
        level = characterPage.Q<Label>("level");


        characterPageSelector.RegisterCallback<ClickEvent>(ChangeToCharacters);
        equipPageSelector.RegisterCallback<ClickEvent>(ChangeToEquipment);
        itemPageSelector.RegisterCallback<ClickEvent>(ChangeToItems);
        settingsPageSelector.RegisterCallback<ClickEvent>(ChangeToSettings);
        characterPageSelector.AddManipulator(new ButtonManipulator());
        equipPageSelector.AddManipulator(new ButtonManipulator());
        itemPageSelector.AddManipulator(new ButtonManipulator());
        settingsPageSelector.AddManipulator(new ButtonManipulator());




        char5 = equipPage.Q<VisualElement>("SansE");
        char6 = equipPage.Q<VisualElement>("OnceE");
        char7 = equipPage.Q<VisualElement>("TritonE");
        char8 = equipPage.Q<VisualElement>("YoelE");

        _listaObjsASans = root.Q<DropdownField>("ObjsAtkSans");
        _listaObjsBSans = root.Q<DropdownField>("ObjsDefSans");
        _listaObjsASans.choices = _objsA;
        _listaObjsBSans.choices = _objsB;
        _listaObjsASans.RegisterValueChangedCallback((value) => CambiarItemA(char5, value));
        _listaObjsBSans.RegisterValueChangedCallback((value) => CambiarItemB(char5, value));

        _listaObjsAOnce = root.Q<DropdownField>("ObjsAtkOnce");
        _listaObjsBOnce = root.Q<DropdownField>("ObjsDefOnce");
        _listaObjsAOnce.choices = _objsA;
        _listaObjsBOnce.choices = _objsB;
        _listaObjsAOnce.RegisterValueChangedCallback((value) => CambiarItemA(char6, value));
        _listaObjsBOnce.RegisterValueChangedCallback((value) => CambiarItemB(char6, value));

        _listaObjsATriton = root.Q<DropdownField>("ObjsAtkTriton");
        _listaObjsBTriton = root.Q<DropdownField>("ObjsDefTriton");
        _listaObjsATriton.choices = _objsA;
        _listaObjsBTriton.choices = _objsB;
        _listaObjsATriton.RegisterValueChangedCallback((value) => CambiarItemA(char7, value));
        _listaObjsBTriton.RegisterValueChangedCallback((value) => CambiarItemB(char7, value));

        _listaObjsAYoel = root.Q<DropdownField>("ObjsAtkYoel");
        _listaObjsBYoel = root.Q<DropdownField>("ObjsDefYoel");
        _listaObjsAYoel.choices = _objsA;
        _listaObjsBYoel.choices = _objsB;
        _listaObjsAYoel.RegisterValueChangedCallback((value) => CambiarItemA(char8, value));
        _listaObjsBYoel.RegisterValueChangedCallback((value) => CambiarItemB(char8, value));


        //TODO: A�adir los manipuladores a las instancias una vez hayamos terminado la template de personaje
        //y tengamos las templates unpackeadas
        InitPersonajes();
        UpdateDescObjects();

        
        //[...]

        characterPage.style.display = DisplayStyle.Flex;
        equipPage.style.display = DisplayStyle.None;
        itemPage.style.display = DisplayStyle.None;
        settingsPage.style.display = DisplayStyle.None;
        pageActive = 0;
    }

    private void InitPersonajes()
    {
        name.text = @"<i> </i>";
        level.text = @"<i> </i>";
        nameAndLevelStatsDisplay.Add(name);
        nameAndLevelStatsDisplay.Add(level);

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

        char5.userData = sans;
        char6.userData = logcel;
        char7.userData = tritonman;
        char8.userData = yoel;


        char1.RegisterCallback<ClickEvent>(selecPersonaje);
        char2.RegisterCallback<ClickEvent>(selecPersonaje);
        char3.RegisterCallback<ClickEvent>(selecPersonaje);
        char4.RegisterCallback<ClickEvent>(selecPersonaje);

        char5.RegisterCallback<ClickEvent>(selecPersonajeEquipment);
        char6.RegisterCallback<ClickEvent>(selecPersonajeEquipment);
        char7.RegisterCallback<ClickEvent>(selecPersonajeEquipment);
        char8.RegisterCallback<ClickEvent>(selecPersonajeEquipment);
    }

    private void UpdateDescObjects()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        desc1 = root.Q("Desc1");
        desc2 = root.Q("Desc2");
        desc3 = root.Q("Desc3");
        desc4 = root.Q("Desc4");

        per1 = char1.userData as Personaje;
        per2 = char2.userData as Personaje;
        per3 = char3.userData as Personaje;
        per4 = char4.userData as Personaje;

        //Descripcion Objetos de Sans
        Label texto1A = root.Q<Label>("Desc1A");
        Label texto1B = root.Q<Label>("Desc1B");
        string text1A, text1B;
        if (per1.EquipA == "EspadaSagrada") text1A = "EquipA: Espada proveniente de las tierras de dios";
        else if (per1.EquipA == "ChanclaMaldita") text1A = "EquipA: Chancla capaz de revertir el golpe hacia su dueño";
        else text1A = "EquipA: Nada";

        if (per1.EquipB == "EscudoDiamante") text1B = "EquipB: Escudo mas duro que el acero";
        else if (per1.EquipB == "Creeper") text1B = "EquipB: Te explota en la cara cada vez que atacas";
        else text1B = "EquipB: Nada";

        texto1A.text = @"<i>" + text1A + "</i>";
        texto1B.text = @"<i>" + text1B + "</i>";
        desc1.Add(texto1A);
        desc1.Add(texto1B);

        //Descripcion Objetos de Once-ler
        Label texto2A = root.Q<Label>("Desc2A");
        Label texto2B = root.Q<Label>("Desc2B");
        string text2A, text2B;
        if (per2.EquipA == "EspadaSagrada") text2A = "EquipA: Espada proveniente de las tierras de dios";
        else if (per2.EquipA == "ChanclaMaldita") text2A = "EquipA: Chancla capaz de revertir el golpe hacia su dueño";
        else text2A = "EquipA: Nada";

        if (per2.EquipB == "EscudoDiamante") text2B = "EquipB: Escudo mas duro que el acero";
        else if (per2.EquipB == "Creeper") text2B = "EquipB: Te explota en la cara cada vez que atacas";
        else text2B = "EquipB: Nada";

        texto2A.text = @"<i>" + text2A + "</i>";
        texto2B.text = @"<i>" + text2B + "</i>";
        desc2.Add(texto2A);
        desc2.Add(texto2B);

        //Descripcion Objetos de Tritonman
        Label texto3A = root.Q<Label>("Desc3A");
        Label texto3B = root.Q<Label>("Desc3B");
        string text3A, text3B;
        if (per3.EquipA == "EspadaSagrada") text3A = "EquipA: Espada proveniente de las tierras de dios";
        else if (per3.EquipA == "ChanclaMaldita") text3A = "EquipA: Chancla capaz de revertir el golpe hacia su dueño";
        else text3A = "EquipA: Nada";

        if (per3.EquipB == "EscudoDiamante") text3B = "EquipB: Escudo mas duro que el acero";
        else if (per3.EquipB == "Creeper") text3B = "EquipB: Te explota en la cara cada vez que atacas";
        else text3B = "EquipB: Nada";

        texto3A.text = @"<i>" + text3A + "</i>";
        texto3B.text = @"<i>" + text3B + "</i>";
        desc3.Add(texto3A);
        desc3.Add(texto3B);

        //Descripcion Objetos de Yoel
        Label texto4A = root.Q<Label>("Desc4A");
        Label texto4B = root.Q<Label>("Desc4B");
        string text4A, text4B;
        if (per4.EquipA == "EspadaSagrada") text4A = "EquipA: Espada proveniente de las tierras de dios";
        else if (per4.EquipA == "ChanclaMaldita") text4A = "EquipA: Chancla capaz de revertir el golpe hacia su dueño";
        else text4A = "EquipA: Nada";

        if (per4.EquipB == "EscudoDiamante") text4B = "EquipB: Escudo mas duro que el acero";
        else if (per4.EquipB == "Creeper") text4B = "EquipB: Te explota en la cara cada vez que atacas";
        else text4B = "EquipB: Nada";

        texto4A.text = @"<i>" + text4A + "</i>";
        texto4B.text = @"<i>" + text4B + "</i>";
        desc4.Add(texto4A);
        desc4.Add(texto4B);
    }

    private void selecPersonaje(ClickEvent e)
    {
        VisualElement person = e.target as VisualElement;
        perSelec = person.userData as Personaje;
        if (perSelec.EquipA == "EspadaSagrada")
        {
            atkDisplay.Estado = 3;
        }else if (perSelec.EquipA == "ChanclaMaldita")
        {
            atkDisplay.Estado = 0;
        }
        else
        {
            atkDisplay.Estado = perSelec.Atk;
        }

        if (perSelec.EquipB == "EscudoDiamante")
        {
            defDisplay.Estado = 3;
        }
        else if (perSelec.EquipB == "Creeper")
        {
            defDisplay.Estado = 0;
        }
        else
        {
            defDisplay.Estado = perSelec.Def;
        }
        spdDisplay.Estado = perSelec.Speed;
        matkDisplay.Estado = perSelec.Matk;
        mdefDisplay.Estado = perSelec.Mdef;

        
        name.text = @"<i>" + perSelec.Nombre + "</i>";
        level.text = @"<i>Lv. " + perSelec.Level + "</i>";
        nameAndLevelStatsDisplay.Add(name);
        nameAndLevelStatsDisplay.Add(level);

    }

    private void selecPersonajeEquipment(ClickEvent e)
    {
        VisualElement person = e.target as VisualElement;
        perSelec = person.userData as Personaje;
        Debug.Log(perSelec.Nombre);

        switch (perSelec.Nombre)
        {
            case "Sans":
                sansEquipPage.style.display = DisplayStyle.Flex;
                onceEquipPage.style.display = DisplayStyle.None;
                trironEquipPage.style.display = DisplayStyle.None;
                yoelEquipPage.style.display = DisplayStyle.None;
                break;

            case "Once-ler":
                sansEquipPage.style.display = DisplayStyle.None;
                onceEquipPage.style.display = DisplayStyle.Flex;
                trironEquipPage.style.display = DisplayStyle.None;
                yoelEquipPage.style.display = DisplayStyle.None;
                break;

            case "Tritonman":
                sansEquipPage.style.display = DisplayStyle.None;
                onceEquipPage.style.display = DisplayStyle.None;
                trironEquipPage.style.display = DisplayStyle.Flex;
                yoelEquipPage.style.display = DisplayStyle.None;
                break;

            case "Yoel":
                sansEquipPage.style.display = DisplayStyle.None;
                onceEquipPage.style.display = DisplayStyle.None;
                trironEquipPage.style.display = DisplayStyle.None;
                yoelEquipPage.style.display = DisplayStyle.Flex;
                break;

            default:
                break;
        }
        UpdateDescObjects();
    }

    private void ChangeToCharacters(ClickEvent e)
    {
        atkDisplay.Estado = 0;
        defDisplay.Estado = 0;
        spdDisplay.Estado = 0;
        matkDisplay.Estado = 0;
        mdefDisplay.Estado = 0;
        name.text = @"<i> </i>";
        level.text = @"<i> </i>";
        nameAndLevelStatsDisplay.Add(name);
        nameAndLevelStatsDisplay.Add(level);
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
            sansEquipPage.style.display = DisplayStyle.None;
            onceEquipPage.style.display = DisplayStyle.None;
            trironEquipPage.style.display = DisplayStyle.None;
            yoelEquipPage.style.display = DisplayStyle.None;
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
    void CambiarItemA(VisualElement pers, ChangeEvent<string> evt)
    {
        perSelec = pers.userData as Personaje;
        perSelec.EquipA = evt.newValue;
        UpdateDescObjects();
    }
    void CambiarItemB(VisualElement pers, ChangeEvent<string> evt)
    {
        perSelec = pers.userData as Personaje;
        perSelec.EquipB = evt.newValue;
        UpdateDescObjects();
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

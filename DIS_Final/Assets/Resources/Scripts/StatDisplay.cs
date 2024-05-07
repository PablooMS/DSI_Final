using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatDisplay : VisualElement
{
    VisualElement rojo = new VisualElement();

    VisualElement ambar = new VisualElement();

    VisualElement verde = new VisualElement();

    int estado;
    public int Estado
    {
        get { return (estado); }
        set
        {
            estado = value;
            procesChange();
        }
    }

    void procesChange()
    {
        switch (estado)
        {
            case 0:
                rojo.style.backgroundColor = new Color(0.27f, 0f, 0f);
                ambar.style.backgroundColor = new Color(0.27f, 0.27f, 0f);
                verde.style.backgroundColor = new Color(0f, 0.27f, 0f);
                break; 
            case 1:
                rojo.style.backgroundColor = Color.red;
                ambar.style.backgroundColor = new Color(0.27f, 0.27f, 0f);
                verde.style.backgroundColor = new Color(0f, 0.27f, 0f);
                break; 
            case 2:
                rojo.style.backgroundColor = Color.red;
                ambar.style.backgroundColor = Color.yellow;
                verde.style.backgroundColor = new Color(0f, 0.27f, 0f);
                break; 
            case 3:
                rojo.style.backgroundColor = Color.red;
                ambar.style.backgroundColor = Color.yellow;
                verde.style.backgroundColor = Color.green;
                break; 
            default: 
                break;
        }
    }

    public new class UxmlFactory : UxmlFactory<StatDisplay, UxmlTraits> { };

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlIntAttributeDescription myEstado = new UxmlIntAttributeDescription { name = "estado", defaultValue = 0 };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var stats = ve as StatDisplay;
            stats.Estado = myEstado.GetValueFromBag(bag, cc);
        }
    }

    public StatDisplay()
    {
        this.style.flexDirection = FlexDirection.Row;

        rojo.style.width = 50;
        rojo.style.height = 30;

        ambar.style.width = 50;
        ambar.style.height = 30;

        verde.style.width = 50;
        verde.style.height = 30;

        styleSheets.Add(Resources.Load<StyleSheet>("MainStyles"));

        rojo.AddToClassList("stats");
        ambar.AddToClassList("stats");
        verde.AddToClassList("stats");

        hierarchy.Add(rojo);
        hierarchy.Add(ambar);
        hierarchy.Add(verde);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonManipulator : MouseManipulator
{
    public ButtonManipulator()
    {

    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
        target.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseEnterEvent>(OnMouseEnter);
        target.UnregisterCallback<MouseLeaveEvent>(OnMouseLeave);
    }

    private void OnMouseEnter(MouseEnterEvent mev)
    {
        target.style.borderBottomColor = Color.cyan;
        target.style.borderLeftColor = Color.cyan;
        target.style.borderRightColor = Color.cyan;
        target.style.borderTopColor = Color.cyan;
        mev.StopPropagation();
    }

    private void OnMouseLeave(MouseLeaveEvent mev)
    {
        target.style.borderBottomColor = Color.black;
        target.style.borderLeftColor = Color.black;
        target.style.borderRightColor = Color.black;
        target.style.borderTopColor = Color.black;
        mev.StopPropagation();
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

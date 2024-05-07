using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

[System.Serializable]
public class Personaje
{
    public event Action Cambio;

    private string charName;
    public string Nombre
    {
        get { return charName; }
    }

    private int level;
    public int Level
    {
        get { return level; }
    }

    private int atk;
    public int Atk
    {
        get { return atk; }
    }

    private int def;
    public int Def
    {
        get { return def; }
    }

    private int matk;
    public int Matk
    {
        get { return matk; }
    }

    private int mdef;
    public int Mdef
    {
        get { return mdef; }
    }

    private int speed;
    public int Speed
    {
        get { return speed; }
    }

    private string equipmentA;
    public string EquipA
    {
        get { return equipmentA; }
        set
        {
            if (equipmentA != value)
            {
                equipmentA = value;
                Cambio?.Invoke();
            }
        }
    }

    private string equipmentB;
    public string EquipB
    {
        get { return equipmentB; }
        set
        {
            if (equipmentB != value)
            {
                equipmentB = value;
                Cambio?.Invoke();
            }
        }
    }

    public Personaje(string nombre, int nivel, int attk, int pdef, int mattk, int mdeff, int spd, string eA, string eB)
    {
        this.charName = nombre;
        this.level = nivel;
        this.atk = attk;
        this.def = pdef;
        this.matk = mattk;
        this.mdef = mdeff;
        this.speed = spd;
        this.equipmentA = eA;
        this.equipmentB = eB;
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

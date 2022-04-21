using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeInfo
{
    public List<Ingredients>[] length = new List<Ingredients>[4]
    {
        new List<Ingredients>() { new Ingredients("coal", 50), new Ingredients("firestone",1),new Ingredients("basalt",50) },
        new List<Ingredients>() { new Ingredients("coal", 100),  new Ingredients("firestone",2), new Ingredients("seaweeds",75) },
        new List<Ingredients>() { new Ingredients("coal", 150),  new Ingredients("firestone",3), new Ingredients("wires",150) },
        new List<Ingredients>() {  new Ingredients("coal", 200),  new Ingredients("firestone",4), new Ingredients("brick",250)  }
    };
    public List<Ingredients>[] width = new List<Ingredients>[4]
    {
        new List<Ingredients>() { new Ingredients("coal", 50), new Ingredients("firestone",1),new Ingredients("skull", 10) },
        new List<Ingredients>() { new Ingredients("coal", 100),  new Ingredients("firestone",2), new Ingredients("yetifur", 15) },
        new List<Ingredients>() { new Ingredients("coal", 150),  new Ingredients("firestone",3), new Ingredients("lead", 25 ) },
        new List<Ingredients>() {  new Ingredients("coal", 200),  new Ingredients("firestone",4), new Ingredients("plasticbottlelid", 50)  }
    };
    public List<Ingredients>[] luck = new List<Ingredients>[4]
    {
        new List<Ingredients>() { new Ingredients("coal", 50), new Ingredients("firestone",1),new Ingredients("lizardeye", 10) },
        new List<Ingredients>() { new Ingredients("coal", 100),  new Ingredients("firestone",2), new Ingredients("batwings", 15 ) },
        new List<Ingredients>() { new Ingredients("coal", 150),  new Ingredients("firestone",3), new Ingredients("goo", 20) },
        new List<Ingredients>() {  new Ingredients("coal", 200),  new Ingredients("firestone",4), new Ingredients("cloth", 25)  }
    };
    public List<Ingredients>[] strength = new List<Ingredients>[4]
    {
        new List<Ingredients>() { new Ingredients("coal", 50), new Ingredients("firestone",1),new Ingredients("golemcore", 10) },
        new List<Ingredients>() { new Ingredients("coal", 100),  new Ingredients("firestone",2), new Ingredients("fishscale", 15) },
        new List<Ingredients>() { new Ingredients("coal", 150),  new Ingredients("firestone",3), new Ingredients("ivory", 25) },
        new List<Ingredients>() {  new Ingredients("coal", 200),  new Ingredients("firestone",4), new Ingredients("beesting", 35)  }
    };
}

public class Ingredients
{
    public string name;
    public int count;

    public Ingredients(string _name, int _count)
    {
        name = _name;
        count = _count;
    }
}
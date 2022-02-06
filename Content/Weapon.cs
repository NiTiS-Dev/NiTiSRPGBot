using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NiTiS.RPGBot.Content;

public class Weapon : Item
{
    public Weapon(string id) : base(id)
    {

    }

    [JsonProperty("type")]
    public WeaponType Type { get; set; }

}
public enum WeaponType : byte
{
    //Melle 1..20
    Sword = 1,
    Spear = 2,
    Hammer = 3,
    //Distance 21.40
    Bow = 21,
    Crossbow = 22,
    //Mage 41..60
    Book = 41,
    Wand = 42,

}
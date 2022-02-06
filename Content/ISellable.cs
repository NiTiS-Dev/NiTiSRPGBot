using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Content;

public interface ISellable
{
    public int SellCost { get; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Content;

public class XPModule
{
    public UInt16 Level { get; private set; }
    public UInt64 XP { get; private set; }
    public UInt64 RequiredXP
    {
        get
        {
            return 0;
        }
    }
    public void ApplyXP(int xp)
    {

    }
}
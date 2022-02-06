using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Content;

public class RPGGuild
{
    [JsonProperty("id")]
    
    public ulong Id { get; set; }

    public RPGGuild(ulong id)
    {
        this.Id = id;
    }
}
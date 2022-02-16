namespace NiTiS.RPGBot.Content;

public class GameObject : IRegistrable<string>
{
    [JsonIgnore]
    public string ID { get; internal set; }
    public GameObject(string id)
    {
        this.ID = id;
    }
}
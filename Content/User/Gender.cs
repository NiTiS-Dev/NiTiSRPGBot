namespace NiTiS.RPGBot.Content.User;

public enum Gender : byte
{
    [EnumInfo("gender.none")] NoGender = 0,
    [EnumInfo("gender.man")] Male = 1,
    [EnumInfo("gender.female")] Female = 2,
}
namespace NiTiS.RPGBot.Modules;

public enum ErrorType : ushort
{
    [EnumInfo("error.invalid-parameters")] InvalidParameters = 0,
    [EnumInfo("error.without-hero")] HeroNotCreated = 1,
    [EnumInfo("error.no-parameters")] NoParameters = 2,
    [EnumInfo("error.registry-doesnt-exists")] RegistryDoesntExists = 3,
    [EnumInfo("error.hero-allready-created")] HeroAllreadyCreated = 4,
}
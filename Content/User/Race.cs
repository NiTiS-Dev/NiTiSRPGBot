namespace NiTiS.RPGBot.Content.User;

public enum Race : byte
{
    [EnumInfo("race.human")] Human = byte.MinValue, //Basic stats
    [EnumInfo("race.lizard")] Lizard = 1, //More luck & speed | melle & distance weapon boost
    [EnumInfo("race.elf")] Elf = 2, //Mage weapon boost
    [EnumInfo("race.half-elf")] HalfElf = 3, //Distance weapon boost
    [EnumInfo("race.cat")] Cat = 4, //Max luck & night vision
    [EnumInfo("race.fox")] Fox = 5, //Mage weapon boost | more speed
    [EnumInfo("race.dwarf")] Dwarf = 6, //Some craft abillity
    [EnumInfo("race.demon")] Demon = 7, //?Damage buff?
    [EnumInfo("race.unknown")] Unknown = byte.MaxValue,

}
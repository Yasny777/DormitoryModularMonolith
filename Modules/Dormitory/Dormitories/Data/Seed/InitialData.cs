using Dormitories.Dormitories.Models;

namespace Dormitories.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Dormitory> Dormitories =>
    [
        Dormitory.Create(Guid.NewGuid(), "Cezar", "normal", "cezar@gmail.com", "123456789",
            Address.Of("Nowoursynowska 161C", "Warszawa", "02-787")),
        Dormitory.Create(Guid.NewGuid(), "Limba", "prestige", "limba@gmail.com", "123456789",
            Address.Of("Nowoursynowska 161L", "Warszawa", "02-787")),
        Dormitory.Create(Guid.NewGuid(), "Dendryt", "normal", "dendryt@gmail.com", "123456789",
            Address.Of("Nowoursynowska 161D", "Warszawa", "02-787")),
    ];
}
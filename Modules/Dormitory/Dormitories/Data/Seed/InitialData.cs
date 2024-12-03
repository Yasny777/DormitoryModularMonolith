using Dormitories.Dormitories.Models;

namespace Dormitories.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Dormitory> Dormitories
    {
        get
        {
            var cezar = Dormitory.Create(
                Guid.Parse("3d56b55e-6004-43e5-b6ae-f3b5b16a3434"),
                "Cezar",
                "normal",
                "cezar@gmail.com",
                "123456789",
                Address.Of("Nowoursynowska 161C", "Warszawa", "02-787"));

            cezar.AddRoom("101", "cheap", 1, 300.00m);
            cezar.AddRoom("102", "normal", 2, 500.00m);
            cezar.AddRoom("103", "normal", 3, 800.00m);
            cezar.AddRoom("104", "cheap", 1, 350.00m);
            cezar.AddRoom("105", "normal", 2, 550.00m);

            var limba = Dormitory.Create(
                Guid.Parse("3c9808d4-5022-4207-809b-a1b382d9edf2"),
                "Limba",
                "prestige",
                "limba@gmail.com",
                "123456789",
                Address.Of("Nowoursynowska 161L", "Warszawa", "02-787"));

            limba.AddRoom("201", "prestige", 1, 400.00m);
            limba.AddRoom("202", "prestige", 2, 700.00m);
            limba.AddRoom("203", "prestige", 3, 1000.00m);
            limba.AddRoom("204", "normal", 1, 450.00m);
            limba.AddRoom("205", "normal", 2, 600.00m);

            var dendryt = Dormitory.Create(
                Guid.Parse("d00d672f-2dcb-4765-b16b-ca73975e76fd"),
                "Dendryt",
                "normal",
                "dendryt@gmail.com",
                "123456789",
                Address.Of("Nowoursynowska 161D", "Warszawa", "02-787"));

            dendryt.AddRoom("301", "cheap", 1, 320.00m);
            dendryt.AddRoom("302", "normal", 2, 480.00m);
            dendryt.AddRoom("303", "normal", 3, 760.00m);
            dendryt.AddRoom("304", "cheap", 1, 330.00m);
            dendryt.AddRoom("305", "normal", 2, 500.00m);

            return new[] { cezar, limba, dendryt };
        }
    }
}
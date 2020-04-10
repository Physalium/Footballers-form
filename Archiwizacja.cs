//przestrzeń nazw dla wejścia wyjścia między innymi zapisa na dysku
using System.IO;

namespace Footballers
{
    internal static class Archiwizacja
    {
        //    public static void ZapisPilkarzyDoPliku(string plik, Footballer[] pilkarze)
        //    {
        //        using (StreamWriter stream = new StreamWriter(plik))
        //        {
        //            foreach (var p in pilkarze)
        //                stream.WriteLine(p.ToFileFormat());
        //            stream.Close();
        //        }
        //    }

        //    public static Footballer[] CzytajPilkarzyZPliku(string plik)
        //    {
        //        Footballer[] pilkarze = null;
        //        if (File.Exists(plik))
        //        {
        //            var sPilkarze = File.ReadAllLines(plik);
        //            var n = sPilkarze.Length;
        //            if (n > 0)
        //            {
        //                pilkarze = new Footballer[n];
        //                for (int i = 0; i < n; i++)
        //                    pilkarze[i] = Footballer.CreateFromString(sPilkarze[i]);
        //                return pilkarze;
        //            }
        //        }
        //        return pilkarze;
        //    }
        //}
    }
}
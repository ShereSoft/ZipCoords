using System;
#if !NET20
using System.Linq;
#endif
using System.Collections.Generic;

public partial class ZipCoords
{
    static Dictionary<string, double[]> Dict = BuildDictionary();
    
    static Dictionary<string, double[]> BuildDictionary()
    {
        var dict = new Dictionary<string, double[]>();
        var zips = new[] { Zips0, Zips1, Zips2, Zips3, Zips4, Zips5, Zips6, Zips7, Zips8, Zips9 };
        var coords = new[] { Coords0, Coords1, Coords2, Coords3, Coords4, Coords5, Coords6, Coords7, Coords8, Coords9 };

        for (int i = 0; i < zips.Length; i++)
        {
            int previousZip = i * 10000;

            for (int j = 0, k = 0; j < zips[i].Length; j++)
            {
                var lat = coords[i][k++];
                var lon = coords[i][k++];
                var zip = (previousZip + zips[i][j]);

                if (lat > 0)
                {
                    dict.Add(zip.ToString().PadLeft(5, '0'), new[] { lat / 1000000.0, lon / 1000000.0 });
                }

                previousZip = zip;
            }
        }
    
        return dict;
    }

    public static double[] GetCoordinates(string zipcode)
    {
        ValidateZipCode(zipcode);

        if (Dict.TryGetValue(zipcode, out var coords))
        {
            return new[] { coords[0], coords[1] };
        }

        return null;
    }

    public static double GetMileage(string originZipcode, string destinationZipcode)
    {
        ValidateZipCode(originZipcode);
        ValidateZipCode(destinationZipcode);

        var ocoords = GetCoordinates(originZipcode);
        var dcoords = GetCoordinates(destinationZipcode);

        return GetMileDistance(ocoords[0], ocoords[1], dcoords[0], dcoords[1]);
    }

    //NEW: 
    public static double GetMileDistance(double originLatitude, double originLongitude, double destinationLatitude, double destinationLongitude)
    {
        var latdiff = Math.Abs(destinationLatitude - originLatitude);
        var londiff = Math.Abs(destinationLongitude - originLongitude);
        var la = latdiff * 69.0;
        var lo = londiff * 54.6;
        var straight = Math.Sqrt((la * la) + (lo * lo));
        var adjusted = straight * 1.15;

        return adjusted;
    }

    static void ValidateZipCode(string zipcode)
    {
        if (zipcode == null)
        {
            throw new ArgumentNullException(nameof(zipcode));
        }

#if !NET20
        if (zipcode.Length != 5 || !zipcode.All(Char.IsNumber))
        {
            throw new ArgumentException($"'{zipcode}' is not a valid ZIP code");
        }
#else
        if (zipcode.Length != 5 || !Char.IsNumber(zipcode[0]) || !Char.IsNumber(zipcode[1]) || !Char.IsNumber(zipcode[2]) || !Char.IsNumber(zipcode[3]) || !Char.IsNumber(zipcode[4]))
        {
            throw new ArgumentException($"'{zipcode}' is not a valid ZIP code");
        }
#endif
    }
}
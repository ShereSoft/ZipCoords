# ZipCoords

[![](https://img.shields.io/nuget/v/ZipCoords.svg)](https://www.nuget.org/packages/ZipCoords/)
[![](https://img.shields.io/nuget/dt/ZipCoords)](https://www.nuget.org/packages/ZipCoords/)

* Light-weight
* No library dependencies
* No external calls

### .GetCoordinates(zipcode)
```csharp
var zipcode = "90201";
var coordinates = ZipCoords.GetCoordinates(zipcode);  // returns null when there's no data

var latitude = coordinates[0];  // 33.971609
var longitude = coordinates[1];  // -118.171234
```

### .GetMileage(originZipcode, destinationZipcode)
```csharp
var originZipcode = "90201";
var destinationZipcode = "97201";
var mileage = ZipCoords.GetMileage(originZipcode, destinationZipcode);

Console.WriteLine(mileage);  // 1041.7590375429163
```

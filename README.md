# Starships

This project aims calculate how many stops are needed for resupply, given the distance in mega lights (MGLT)

## Technologies Used

1. Visual Studio 2017
2. .NET Core 2.2 
3. XUnit for Unit Tests

## Prerequisites

1. SDK 2.2 (https://dotnet.microsoft.com/download/dotnet-core/2.2)
2. dotnet CLI (already included in SDK)

## Installing

* After downloading the repo and installing the SDK, use the command prompt you like and go to the project path. Type `dotnet build` and click `Enter`. 
* After successfully compilation go to directory `src/Kneat.Worker` type `dotnet publish -c Release -r win10-x64` and click `Enter`. This example will be to `Windows 10 - 64 bits`. For other operating systems, please see this site (https://docs.microsoft.com/en-us/dotnet/core/rid-catalog)
* The files will be published in `..\src\Kneat.Worker\bin\Release\netcoreapp2.2\runtime`
* Run exe file 
* In case of error, after running exe file, there is a log file in logs folder that will be created. See for further details.

## Running the tests

This project has 4 unit tests that can be run on command line:
1. dotnet test --filter Kneat.Tests.Services.KneatServiceTest.Get_Distance_Successfully
2. dotnet test --filter Kneat.Tests.Services.External.SwapiServiceTest.Get_StarShip_Successfully
3. dotnet test --filter Kneat.Tests.Services.External.SwapiServiceTest.Get_StarShip_Timeout_Error
`this test simulate a timeout response`
4. dotnet test --filter Kneat.Tests.Services.External.SwapiServiceTest.Get_StarShip_Unexpected_Error
`this test simulate a non existing URL`

**- To simulate the test 3 is need to change 'Timeout' value in testsettings.json file. Use low values.**

**- To simulate the test 4 is need to change 'BaseUrl' value in testsettings.json file to invalid Url.**

The **testsettings.js** is in `..\src\Kneat.Tests`

In case of error, after running some test, there is a log file in logs folder that will be created. See for further details.

## Contributing

https://swapi.co/


## Author

Daniel Kakuto (kakutodaniel@hotmail.com)


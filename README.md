# poa-final-project

[![Build Status](https://travis-ci.org/eduardoafontana/poa-final-project.svg?branch=main)](https://travis-ci.org/eduardoafontana/poa-final-project)
[![BCH compliance](https://bettercodehub.com/edge/badge/eduardoafontana/poa-final-project?branch=main)](https://bettercodehub.com/)
[![Maintainability](https://api.codeclimate.com/v1/badges/a9aa816f9a6423214057/maintainability)](https://codeclimate.com/github/eduardoafontana/poa-final-project/maintainability)
[![Test Coverage](https://api.codeclimate.com/v1/badges/a9aa816f9a6423214057/test_coverage)](https://codeclimate.com/github/eduardoafontana/poa-final-project/test_coverage)


## Local tests
For run local tests with coverage do:

dotnet test Wumpus.csproj /p:CollectCoverage=true /p:IncludeTestAssembly=true /p:CoverletOutputFormat=cobertura /p:ExcludeByFile=\"**/Microsoft.NET.Test.Sdk.Program.cs\" /p:CoverletOutput='./cobertura.xml'
# MusicReco
> Application for music recommendations with useful features!

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Features](#features)

## General info
MusicReco is a console app that enables searching music recommendations based on genre, artist and year of release - it's the main feature of this project. You can also add new song to the database, which is .xml file with several examples of songs. Creating your own playlists is also possible - detailed info is available in the Features section.
MusicReco is built using Clean Architecture.
This project was created during C# course, but all code and features were created by myself.

## Technologies
* .NET Core 3.1.6
* LINQ
* Newtonsoft.Json 12.0.3
* FluentAssertions 5.10.3
* Moq 4.14.5
* XUnit 2.4.0
* Used data format: XML, JSON

According to Clean Architecture, the project consists of application and domain layer.
The first layer consists interfaces and managers with services injected by Dependency Injection,
the domain layer has all models. There are also unit tests written in accordance with AAA pattern.
## Features

* Add your favourite songs to the database
* Get music recommendation based on artist, genre or year of release
* Read details about chosen song
* Create new playlist or add songs to existing one
* Show your playlists, which are automatically saved in JSON file
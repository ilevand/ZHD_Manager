using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ZHD_Manager
{
    internal class Filler
    {

        static string[] stations =
@"Краснодар
Владивосток
Москва
Ростов
Санкт-Петербург
Адлер
Сочи
Анапа
Красноярск
Челябинск
Иркутск
Нижний Новгород".Split("\n");

        public static void FillDB(TrainDatabase train_db)
        {
            //train_db.PersonList.RemoveRange(train_db.PersonList);
            train_db.Trains.RemoveRange(train_db.Trains);
            train_db.SaveChanges();
            train_db.Stations.RemoveRange(train_db.Stations);
            train_db.SaveChanges();
            FillStations(train_db);
            FillTrains(train_db);
            FillTrainsWithWagonsAndPlaces(train_db);
        }

        private static void FillStations(TrainDatabase train_db)
        {
            train_db.Stations.AddRange(stations.Select((station) => new StationInfo { Name = station }));
            train_db.SaveChanges();
        }

        private static void FillTrains(TrainDatabase train_db)
        {
            Random rand = new Random();
            foreach (var i in Enumerable.Range(0, 50))
            {
                var first_station = stations[rand.Next(stations.Count())];
                var second_station = stations[rand.Next(stations.Count())];

                while (second_station == first_station)
                {
                    second_station = stations[rand.Next(stations.Count())];
                }
                var departure_time = TimeSpan.FromMinutes(rand.Next(0, 60 * 24 * 7));
                var trip_time = TimeSpan.FromMinutes(rand.Next(0, 60 * 24 * 3));
                var departure_date_time = DateTime.Now.Date + departure_time;
                var arrivale_date_time = departure_date_time + trip_time;

                train_db.Trains.Add(new TrainInfo
                {
                    StationOfDeparture = train_db.Stations.Where(station => station.Name == first_station).First(),
                    StationOfArrival = train_db.Stations.Where(station => station.Name == second_station).First(),
                    Departure = departure_date_time,
                    Arrival = arrivale_date_time,
                });
                train_db.SaveChanges();
            }
        }
        private static void FillTrainsWithWagonsAndPlaces(TrainDatabase train_db)
        {
            Random rand = new Random();
            foreach (var train in train_db.Trains)
            {
                var number_of_closed_wagons = rand.Next(1, 10);
                var number_of_open_wagons = rand.Next(1, 10);
                var closed_wagons = Enumerable.Range(1, number_of_closed_wagons)
                    .Select(index =>
                    {
                        var number_of_seats = 30;
                        var number_of_available_seats = rand.Next(0, number_of_seats);
                        var seats = Enumerable
                            .Repeat(true, number_of_available_seats)
                            .Concat(
                                Enumerable.Repeat(false, number_of_seats - number_of_available_seats)
                            ).ToList()
                            .OrderBy(_ => rand.Next())
                            .ToList();
                        return seats;
                    })
                    .Select(wagon => new WagonInfo
                    {
                        Places = wagon.Select(available => new PlaceInfo { IsAvailable = available }).ToList(),
                        Type = WagonInfo.WagonType.Closed
                    });

                var open_wagons = Enumerable.Range(1, number_of_open_wagons)
                    .Select(index =>
                    {
                        var number_of_seats = 30;
                        var number_of_available_seats = rand.Next(0, number_of_seats);
                        var seats = Enumerable
                            .Repeat(true, number_of_available_seats)
                            .Concat(
                                Enumerable.Repeat(false, number_of_seats - number_of_available_seats)
                            ).ToList()
                            .OrderBy(_ => rand.Next())
                            .ToList();
                        return seats;
                    })
                    .Select(wagon => new WagonInfo
                    {
                        Places = wagon.Select(available => new PlaceInfo { IsAvailable = available }).ToList(),
                        Type = WagonInfo.WagonType.Open
                    });
                train.Wagons = open_wagons.Concat(closed_wagons).ToList();
                train_db.SaveChanges();
            }
        }
    }
}
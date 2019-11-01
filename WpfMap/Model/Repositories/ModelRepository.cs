using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Model.DTO;

namespace WpfMap.Model.Repositories
{
    public class ModelRepository
    {
        public List<Country> Countries { get; private set; } = new List<Country>();
        public List<City> Cities { get; private set; } = new List<City>();
        public List<Street> Streets { get; private set; } = new List<Street>();
        public List<House> Houses { get; private set; } = new List<House>();
        public List<Room> Rooms { get; private set; } = new List<Room>();
        public List<Resident> Residents { get; private set; } = new List<Resident>();
        private static ModelRepository _repository;

        public static ModelRepository GetInstance()
        {
            if(_repository == null)
            {
                _repository = new ModelRepository();
            }
            return _repository;
        }

        private ModelRepository()
        {
            for(int i = 0; i < 10; i++)
            {
                AddCountry(Countries, 15);
            }
        }

        private void AddCountry(List<Country> countries, int citiesCount)
        {
            int no = countries.Count + 1;
            Country country = new Country
            {
                Name = "Country #" + no,
                Cities = new List<City>()
            };
            for(int i = 0; i < citiesCount; i++)
            {
                AddCity(country.Cities, 20);
            }
            country.Capital = country.Cities[0];
            countries.Add(country);
        }

        private void AddCity(List<City> cities, int streetsCount)
        {
            int no = cities.Count + 1;
            City city = new City
            {
                Name = "City #" + no,
                Area = 50,
                Streets = new List<Street>()
            };
            for (int i = 0; i < streetsCount; i++)
            {
                AddStreet(city.Streets, 10);
            }
            cities.Add(city);
            Cities.Add(city);
        }

        private void AddStreet(List<Street> streets, int housesCount)
        {
            int no = streets.Count + 1;
            Street street = new Street
            {
                Name = "Street #" + no,
                Houses = new List<House>()
            };
            for (int i = 0; i < housesCount; i++)
            {
                AddHouse(street.Houses, 5);
            }
            streets.Add(street);
            Streets.Add(street);
        }

        private void AddHouse(List<House> houses, int roomsCount)
        {
            int no = houses.Count + 1;
            House house = new House
            {
                Address = "House #" + no,           
                Rooms = new List<Room>()
            };
            for (int i = 0; i < roomsCount; i++)
            {
                AddRoom(house.Rooms, 3);
            }
            houses.Add(house);
            Houses.Add(house);
        }

        private void AddRoom(List<Room> rooms, int residentsCount)
        {
            int no = rooms.Count + 1;
            Room room = new Room
            {
                No = no,
                Area = 50,
                RoomResidents = new List<RoomResident>()
            };
            for (int i = 0; i < residentsCount; i++)
            {
                AddRoomResident(room.RoomResidents, room);
            }
            rooms.Add(room);
            Rooms.Add(room);
        }

        private void AddRoomResident(List<RoomResident> roomResidents, Room room)
        {
            int no = roomResidents.Count + 1;
            Resident resident = new Resident
            {
                Name = "Resident #" + no,
                Age = 30 + no % 10,
                Gender = Resident.Sex.MALE,
                Phone = "Phone #" + no,
                ResidenceTime = no % 10
            };
            Residents.Add(resident);

            RoomResident roomResident = new RoomResident
            {
                Resident = resident,
                Room = room
            };
            roomResidents.Add(roomResident);
        }
    }
}

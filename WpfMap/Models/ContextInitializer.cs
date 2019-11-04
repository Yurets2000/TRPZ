using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Helpers.Utils;
using WpfMap.Models.Contexts;
using WpfMap.Models.Entities;
using WpfMap.Models.Repositories;

namespace WpfMap.Models
{
    public class ContextInitializer : CreateDatabaseIfNotExists<MainContext>
    {
        private CountryRepository _countryRepository;
        private CityRepository _cityRepository;
        private StreetRepository _streetRepository;
        private HouseRepository _houseRepository;
        private RoomRepository _roomRepository;
        private RoomResidentRepository _roomResidentRepository;
        private ResidentRepository _residentRepository;
        private UserRepository _userRepository;

        protected override void Seed(MainContext context)
        {
            _countryRepository = new CountryRepository(context);
            _cityRepository = new CityRepository(context);
            _streetRepository = new StreetRepository(context);
            _houseRepository = new HouseRepository(context);
            _roomRepository = new RoomRepository(context);
            _roomResidentRepository = new RoomResidentRepository(context);
            _residentRepository = new ResidentRepository(context);
            _userRepository = new UserRepository(context);

            InitializeUsers();
            InitializeCountriesResursively();
            context.SaveChanges();
        }

        private void InitializeUsers()
        {
            List<User> users = new List<User>
            {
                new User{Login = "user", Password = "user", Admin = false},
                new User { Login = "admin", Password = "admin", Admin = true }
            };
            foreach(User user in users){
                _userRepository.Add(user);
            }
        }

        private void InitializeCountriesResursively()
        {
            List<Country> countries = new List<Country>();
            for (int i = 0; i < 1; i++)
            {
                AddCountry(countries, 5);
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
            for (int i = 0; i < citiesCount; i++)
            {
                AddCity(country, 3);
            }
            countries.Add(country);
            _countryRepository.Add(country);
        }

        private void AddCity(Country country, int streetsCount)
        {
            int no = country.Cities.Count + 1;
            City city = new City
            {
                Country = country,
                Name = "City #" + no,
                Area = 50,
                Streets = new List<Street>()
            };
            for (int i = 0; i < streetsCount; i++)
            {
                AddStreet(city, 1);
            }
            country.Cities.Add(city);
            _cityRepository.Add(city);
        }

        private void AddStreet(City city, int housesCount)
        {
            int no = city.Streets.Count + 1;
            Street street = new Street
            {
                City = city,
                Name = "Street #" + no,
                Houses = new List<House>()
            };
            for (int i = 0; i < housesCount; i++)
            {
                AddHouse(street, 1);
            }
            city.Streets.Add(street);
            _streetRepository.Add(street);
        }

        private void AddHouse(Street street, int roomsCount)
        {
            int no = street.Houses.Count + 1;
            House house = new House
            {
                Street = street,
                Address = "House #" + no,
                Rooms = new List<Room>()
            };
            for (int i = 0; i < roomsCount; i++)
            {
                AddRoom(house, 2);
            }
            street.Houses.Add(house);
            _houseRepository.Add(house);
        }

        private void AddRoom(House house, int residentsCount)
        {
            int no = house.Rooms.Count + 1;
            Room room = new Room
            {
                UID = Generator.RandomString(7),
                House = house,
                No = no,
                Area = 50,
                RoomResidents = new List<RoomResident>()
            };
            for (int i = 0; i < residentsCount; i++)
            {
                AddRoomResident(room);
            }
            house.Rooms.Add(room);
            _roomRepository.Add(room);
        }

        private void AddRoomResident(Room room)
        {
            int no = room.RoomResidents.Count + 1;
            Resident resident = new Resident
            {
                UID = Generator.RandomString(7),
                Name = "Resident #" + no,
                Age = 30 + no % 10,
                Gender = Resident.Sex.MALE,
                Phone = "Phone #" + no,
                ResidenceTime = no % 10,
                RoomResidents = new List<RoomResident>()
            };

            RoomResident roomResident = new RoomResident
            {
                Room = room,
                Resident = resident
            };
            resident.RoomResidents.Add(roomResident);
            room.RoomResidents.Add(roomResident);

            _roomResidentRepository.Add(roomResident);
            _residentRepository.Add(resident);
        }
    }
}

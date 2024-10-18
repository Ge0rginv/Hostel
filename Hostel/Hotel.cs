﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    // Hotel class
    public class Hotel : IHotel
    {
        private readonly List<IRoom> rooms;

        public Hotel(int totalRooms)
        {
            rooms = new List<IRoom>();
            InitializeRooms(totalRooms);
        }

        private void InitializeRooms(int totalRooms)
        {
            for (int i = 1; i <= totalRooms; i++)
            {
                if (i <= 10) rooms.Add(new Suite(i, 1));
                else if (i <= 15) rooms.Add(new HalfSuite(i, 1));
                else if (i <= 20) rooms.Add(new DoubleRoom(i, 2));
                else if (i <= 25) rooms.Add(new DoubleWithSofa(i, 2));
                else rooms.Add(new SingleRoom(i, 1));
            }
        }

        public bool BookRoom(RoomType type)
        {
            var room = rooms.FirstOrDefault(r => r.Type == type && !r.IsOccupied);
            if (room != null)
            {
                //room.CheckIn();
                Console.WriteLine($"Room {room.Number} of type {room.Type} booked.");
                return true;
            }
            Console.WriteLine($"No available rooms of type {type}.");
            return false;
        }

        public void CheckOut(int roomNumber)
        {
            var room = rooms.FirstOrDefault(r => r.Number == roomNumber);
            if (room != null && room.IsOccupied)
            {
                //room.CheckOut();
                Console.WriteLine($"Room {room.Number} checked out.");
            }
            else
            {
                Console.WriteLine($"Room {roomNumber} is not occupied or does not exist.");
            }
        }

        public void DisplayRoomStatus()
        {
            foreach (var room in rooms)
            {
                // исправить вывод данных
                Console.WriteLine($"Room {room.Number}: Type = {room.Type}, Price = {room.Price}, Occupied = {room.IsOccupied}");
            }
        }
    }

}
/*
 * ресепшен прием и обработка заявок от генератора
 * хранить количество заброненых и занятых
 * к перечеслению привязать стоимость
 */

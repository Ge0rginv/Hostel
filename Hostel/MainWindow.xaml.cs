﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hostel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IHotel hotel;

        public MainWindow()
        {
            InitializeComponent();
            hotel = new Hotel(30); // Инициализация гостиницы
            hotel.DisplayRoomStatus(); // Отображение статуса
        }

        private void BookRoom_Click(object sender, RoutedEventArgs e)
        {
            hotel.BookRoom(RoomType.Single); // Например, бронируем одноместный номер
            hotel.DisplayRoomStatus();
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            hotel.CheckOut(1); // Например, выселение из номера 1
            hotel.DisplayRoomStatus();
        }

        private void RoomStatusTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
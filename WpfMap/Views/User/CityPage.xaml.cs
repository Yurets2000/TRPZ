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
using System.Windows.Shapes;
using WpfMap.Models.Entities;
using WpfMap.ViewModels.User;

namespace WpfMap
{
    public partial class CityPage : Window
    {
        public CityPage(City city)
        {
            InitializeComponent();
            CityViewModel viewModel = new CityViewModel(city);
            DataContext = viewModel;
        }
    }
}

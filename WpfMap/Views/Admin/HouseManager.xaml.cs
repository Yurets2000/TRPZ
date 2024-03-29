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
using WpfMap.Models;
using WpfMap.Models.Entities;
using WpfMap.ViewModels.Admin;

namespace WpfMap
{
    public partial class HouseManager : Window
    {
        public HouseManager(Street street)
        {
            InitializeComponent();
            HouseManagerViewModel viewModel = new HouseManagerViewModel(street);
            DataContext = viewModel;
        }
    }
}

using System;
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
using WpfMap.Views.Admin;

namespace WpfMap
{
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            this.InitializeComponent();
        }

        public void SelectManager_Click(object sender, EventArgs e)
        {
            string manager = ((TextBlock)managerSelector.SelectedValue).Text;
            switch (manager)
            {
                case "Country Manager":
                    CountryManager countryManager = new CountryManager();
                    countryManager.ShowDialog();
                    break;
                case "Room-Resident Manager":
                    RoomResidentManager roomManager = new RoomResidentManager();
                    roomManager.ShowDialog();
                    break;
                case "Resident Manager":
                    ResidentManager residentManager = new ResidentManager();
                    residentManager.ShowDialog();
                    break;
            }
        }

        public void OpenUserPage_Click(object sender, EventArgs e)
        {
            UserPage userPage = new UserPage();
            userPage.ShowDialog();
        }
    }
}

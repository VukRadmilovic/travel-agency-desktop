using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Turisticka_Agencija.Windows.Admin;
using Turisticka_Agencija.Windows.Shared;

namespace Turisticka_Agencija.Help
{
    internal class HelpProvider
    {
        public static string GetHelpKey(DependencyObject obj)
        {
            return obj.GetValue(HelpKeyProperty) as string;
        }

        public static void SetHelpKey(DependencyObject obj, string value)
        {
            obj.SetValue(HelpKeyProperty, value);
        }

        public static readonly DependencyProperty HelpKeyProperty =
            DependencyProperty.RegisterAttached("HelpKey", typeof(string), typeof(HelpProvider), new PropertyMetadata("index", HelpKey));
        private static void HelpKey(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //NOOP
        }

        public static void ShowHelp(string key, LoginWindow originator)
        {
            HelpViewer hh = new HelpViewer("HelpLogin");
            hh.Show();
        }
        public static void ShowHelp(string key)
        {
            HelpViewer hh = new HelpViewer(key);
            hh.Show();
        }

        public static void ShowHelp(string key, ViewAllTripsWindow originator)
        {
            HelpViewer hh = new HelpViewer("HelpLogin");
            hh.Show();
        }
        public static void ShowHelp(string key, ViewOneTripWindow originator)
        {
            HelpViewer hh = new HelpViewer("HelpLogin");
            hh.Show();
        }
        public static void ShowHelp(string key, ViewBoughtTripsWindow originator)
        {
            HelpViewer hh = new HelpViewer("HelpLogin");
            hh.Show();
        }
        public static void ShowHelp(string key, ReportWindow originator)
        {
            HelpViewer hh = new HelpViewer("HelpLogin");
            hh.Show();
        }
        public static void ShowHelp(string key, CRUDTripWindow originator)
        {
            HelpViewer hh = new HelpViewer("HelpLogin");
            hh.Show();
        }
        public static void ShowHelp(string key, CRUDRestaurantWindow originator)
        {
            HelpViewer hh = new HelpViewer("HelpLogin");
            hh.Show();
        }
        public static void ShowHelp(string key, CRUDPlaceWindow originator)
        {
            HelpViewer hh = new HelpViewer("HelpLogin");
            hh.Show();
        }
        public static void ShowHelp(string key, CRUDAccommodationWindow originator)
        {
            HelpViewer hh = new HelpViewer("HelpLogin");
            hh.Show();
        }
    }
}

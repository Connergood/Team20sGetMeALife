﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetMeALife.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventPage : ContentPage
	{
		public EventPage ()
		{
			InitializeComponent ();
		}

        public static void LoadEvents(List<string> types, bool noRestriction = false)
        {
            // Find Me - No restriction, pull everything
            if (noRestriction)
            { return; }

            // Grab all events of types

            // Filter events by location
        }

        public static void OnEventClicked()
        {

        }
	}
}
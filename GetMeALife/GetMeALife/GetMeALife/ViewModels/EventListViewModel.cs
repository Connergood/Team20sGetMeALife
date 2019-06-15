using GetMeALibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace GetMeALife.ViewModels
{
    public class EventListViewModel
    {
        private bool _isVisible { get; set; } = true;

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                try
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsVisible"));
                }
                catch
                {
                }

            }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            try
            {

                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}

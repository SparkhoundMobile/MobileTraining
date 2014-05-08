using System;
using System.ComponentModel;

namespace Mymdb.Core.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private bool isBusy;
        public Action<bool> IsBusyChanged { get; set; }
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                if (IsBusyChanged != null)
                    IsBusyChanged(isBusy);
            }
        }

        private bool canLoadMore;
        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set
            {
                canLoadMore = value;
                OnPropertyChanged("CanLoadMore");
            }
        }
    }
}

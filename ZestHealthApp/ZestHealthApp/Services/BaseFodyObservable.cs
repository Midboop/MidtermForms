using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ZestHealthApp
{
    public abstract class BaseFodyObservable : INotifyPropertyChanged
    {
#pragma warning disable CS0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore
    }
}

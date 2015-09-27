using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace TypeSelectorSample
{
    public class MainPageViewModel : BindableBase
    {
        public ObservableCollection<BindableBase> Items { get; set; } = new ObservableCollection<BindableBase>
        {
            new RedViewModel(),
            new BlueViewModel(),
            new GreenViewModel(),
            new RedViewModel(),
            new BlueViewModel(),
            new GreenViewModel(),
            new GreenViewModel(),
            new BlueViewModel(),
            new RedViewModel(),
        };

        public MainPageViewModel()
        {
        }
    }

    public class RedViewModel : BindableBase
    {
        public RedViewModel()
        {
        }
    }

    public class BlueViewModel : BindableBase
    {
        public BlueViewModel()
        {
        }
    }

    public class GreenViewModel : BindableBase
    {
        public GreenViewModel()
        {
        }
    }
}

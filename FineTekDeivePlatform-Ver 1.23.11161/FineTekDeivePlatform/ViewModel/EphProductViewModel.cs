using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FineTekDeivePlatform.Models;
using System.Drawing;
using GalaSoft.MvvmLight.Threading;
using System.Threading;

namespace FineTekDeivePlatform.ViewModel
{
    public class EphProductViewModel : ViewModelBase, ISensor
    {
        private string _header="";
        private string _volume = "";
        private EphProduct _ephproduct = new EphProduct();
        
        public EphProductViewModel()
        {
            DispatcherHelper.Initialize();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Thread.Sleep(100);

            DispatcherHelper.UIDispatcher.Invoke(() =>
            {
                //Header = "1111" ;
            });
        }
        public string Header
        {
            get { return _header; }
            set { _header = value; RaisePropertyChanged("Header"); }
        }
        public string Volume
        {
            get { return _volume; }
            set { _volume = value; RaisePropertyChanged("Volume"); }
        }
        public void UpdateParameter(EphProduct eph)
        {
            _ephproduct = eph;
        }
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using FineTekDeivePlatform.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FineTekDeivePlatform.ViewModel
{
    public class LedDigitalViewModel : ViewModelBase
    {
        #region Fields

        private string _singleDigitalValue = "";
        private string _multiDigitalValue = "";
        private Brush _multiDigitalBrush = null;
        private Brush _multiDigitalBackgroundBrush = null;

        #endregion

        #region Properties

        public string SingleDigitalValue
        {
            get { return _singleDigitalValue; }
            set { _singleDigitalValue = value; RaisePropertyChanged("SingleDigitalValue"); }
        }

        public string MultiDigitalValue
        {
            get
            {
                return _multiDigitalValue;
            }

            set
            {
                _multiDigitalValue = value; RaisePropertyChanged("MultiDigitalValue");
            }
        }

        public Brush MultiDigitalBrush
        {
            get
            {
                return _multiDigitalBrush;
            }

            set
            {
                _multiDigitalBrush = value; RaisePropertyChanged("MultiDigitalBrush");
            }
        }

        public Brush MultiDigitalBackgroundBrush
        {
            get
            {
                return _multiDigitalBackgroundBrush;
            }

            set
            {
                _multiDigitalBackgroundBrush = value; RaisePropertyChanged("MultiDigitalBackgroundBrush");
            }
        }

        #endregion

        #region Constructors

        public LedDigitalViewModel()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        #endregion

        #region Private Methods

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Color mutiDitigalForegroundColor = ColorHelper.GetRandomColor();
            Thread.Sleep(100);
            Color mutiDigitalBackgroundColor = ColorHelper.GetRandomColor();

            DispatcherHelper.UIDispatcher.Invoke(() =>
            {
                Random r = new Random();
                SingleDigitalValue = r.Next(0, 9).ToString();
                MultiDigitalValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                MultiDigitalBrush = new SolidColorBrush(mutiDitigalForegroundColor);
                MultiDigitalBackgroundBrush = new LinearGradientBrush(new GradientStopCollection()
                {
                    new GradientStop(){ Offset = 0, Color = Colors.White},
                    new GradientStop(){ Offset = 0.5, Color = mutiDigitalBackgroundColor},
                    new GradientStop(){ Offset = 1, Color = Colors.White}
                })
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(0, 1),
                    Opacity = 0.3
                };
            });
        }

        #endregion

        #region Public Methods

        #endregion
    }
}

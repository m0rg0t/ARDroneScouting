﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

using ARDrone2Client.Common;
using ARDrone2Client.Common.Configuration;
using ARDrone2Client.Common.Configuration.Native;
using ARDrone2Client.Common.Input;
using ARDrone2Client.Common.Navigation;
using ARDrone2Client.Common.ViewModel;
using ARDrone2Client.Windows.Input;

namespace ARDrone2.Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FlyPage : ARDrone2.Sample.Common.LayoutAwarePage
    {
        private DroneClient _droneClient;
        private string _VideoSourceUrl = "ardrone://192.168.1.1";
        private DispatcherTimer _Timer;
        public FlyPage()
        {
            this.InitializeComponent();
            //if (Application.Current.Resources.ContainsKey("DroneClient"))
            //{
            //    _droneClient = (DroneClient)Application.Current.Resources["DroneClient"];
            //}
            //else
            //{
            //    _droneClient = new DroneClient();
            //}
            _droneClient = DroneClient.Instance;
            //Register joysticks
            if (_droneClient.InputProviders.Count == 0)
            {
                _droneClient.InputProviders.Add(new XBox360JoystickProvider(_droneClient));
                _droneClient.InputProviders.Add(new SoftJoystickProvider(_droneClient, RollPitchJoystick, YawGazJoystick));
            }
            this.DataContext = _droneClient;
            this.DefaultViewModel["Messages"] = _droneClient.Messages;
            _Timer = new DispatcherTimer();
            _Timer.Tick += _Timer_Tick;
            _Timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _Timer.Start();

            this.ControlMap.PointerPressedOverride += MyMap_PointerPressedOverride;
        }

        void MyMap_PointerPressedOverride(object sender, PointerRoutedEventArgs e)
        {
            Bing.Maps.Location l = new Bing.Maps.Location();
            this.ControlMap.TryPixelToLocation(e.GetCurrentPoint(this.ControlMap).Position, out l);
            Bing.Maps.Pushpin pushpin = new Bing.Maps.Pushpin();
            pushpin.SetValue(Bing.Maps.MapLayer.PositionProperty, l);
            this.ControlMap.Children.Add(pushpin);
        }

        private async void _Timer_Tick(object sender, object e)
        {
            UpdateDisplay();

            if (!_droneClient.IsActive)
            {
                await _droneClient.ConnectAsync();
                if (!_droneClient.IsActive)
                {
                    await Task.Delay(5000);
                }
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Application.Current.Suspending += OnSuspend;
            Application.Current.Resuming += OnResume;
            if (!_droneClient.IsActive)
                await _droneClient.ConnectAsync();
            UpdateDisplay();
        }
        
        private void UpdateDisplay()
        {
            int altitudeMax = _droneClient.Configuration.Control.altitude_max.Value;
            AltitudeSlider.Maximum = altitudeMax / 1000;
            AltitudeSlider.StepFrequency = AltitudeSlider.Maximum / 100;
            var active = _droneClient.IsActive;
            TakeOffLandButton.IsEnabled = active;
            TakeOffLandButton.Content = _droneClient.IsFlying ? "Land" : "Take off";

            SwitchVideoChannelButton.IsEnabled = active;
            ConfigurationButton.IsEnabled = active;
            ResetEmergencyButton.IsEnabled = active;
            IndoorOutdoorButton.IsEnabled = active;
            TakePictureButton.IsEnabled = active;
            PlayAnimationButton.IsEnabled = active;
            PlayLedAnimationButton.IsEnabled = active;
            StartVideoRecordingButton.IsEnabled = active;
            StopVideoRecordingButton.IsEnabled = active;

            //InputState.Text = _droneClient.InputState.ToString();
        }

        private void Page_OnLoaded(object sender, RoutedEventArgs e)
        {
            arDroneMediaElem.Source = new Uri(_VideoSourceUrl);
            UpdateDisplay();
        }

        private void OnResume(object sender, object e)
        {
            this.arDroneMediaElem.Source = new Uri(_VideoSourceUrl);
        }

        private async void OnSuspend(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            this.arDroneMediaElem.Source = null;
            await Task.Delay(2000);
            deferral.Complete();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Application.Current.Suspending -= OnSuspend;
            Application.Current.Resuming -= OnResume;
        }

        private void SwitchVideoChannel_Click(object sender, RoutedEventArgs e)
        {
            _droneClient.SwitchVideoChannel();
            UpdateDisplay();
        }

        private void SwitchIndoorOutdoor_Click(object sender, RoutedEventArgs e)
        {
            if (IndoorOutdoorButton.Content.Equals("Indoor"))
            {
                IndoorOutdoorButton.Content = "Outdoor";
                _droneClient.SetOutdoorConfiguration();
            }
            else
            {
                IndoorOutdoorButton.Content = "Indoor";
                _droneClient.SetIndoorConfiguration();
            }
            UpdateDisplay();
        }

        private void TakeOffLandButton_Click(object sender, RoutedEventArgs e)
        {
            if (_droneClient.IsFlying)
            {
                _droneClient.Land();
            }
            else
            {
                _droneClient.TakeOff();
            }
            UpdateDisplay();
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);
            double leftLimit = (ActualWidth / 2) - (YawGazJoystick.ActualWidth / 2);
            double rightLimit = (ActualWidth / 2) + (YawGazJoystick.ActualWidth / 2);
            double topLimit = YawGazJoystick.ActualHeight / 2;
            double bottomLimit = ActualHeight - (YawGazJoystick.ActualWidth / 2);
            if (e.Pointer.PointerDeviceType != Windows.Devices.Input.PointerDeviceType.Touch)
                return;
            Point pos = e.GetCurrentPoint(JoystickGrid).Position;
            var joy = pos.X < (ActualWidth / 2) ? RollPitchJoystick : YawGazJoystick;
            var bounds = new Rect(joy.ActualWidth / 2, joy.ActualHeight / 2, JoystickGrid.ActualWidth - joy.ActualWidth / 2, JoystickGrid.ActualHeight - joy.ActualHeight / 2);
            if (!bounds.Contains(pos))
                return;
            joy.Margin = new Thickness(pos.X - joy.ActualWidth / 2, pos.Y - joy.ActualHeight / 2, 0, 0);
            joy.VerticalAlignment = VerticalAlignment.Top;
            joy.HorizontalAlignment = HorizontalAlignment.Left;

            //if (location.Y > topLimit && location.Y < bottomLimit)
            //{
            //    JoystickControl joystick = null;
            //    if (location.X < leftLimit)
            //    {
            //        joystick = RollPitchJoystick;
            //    }
            //    else if (location.X > rightLimit)
            //    {
            //        joystick = YawGazJoystick;
            //        //YawGazJoystick.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            //        //YawGazJoystick.Margin = new Thickness(0, location.Y - joyPos.X, ActualWidth - location.X - joyPos.Y, 0);
            //    }
            //    if (joystick != null)
            //    {
            //        var joyPos = new Point(RollPitchJoystick.ActualWidth / 2, RollPitchJoystick.ActualHeight / 2);
            //        joystick.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            //        joystick.Margin = new Thickness(location.X - joyPos.X, location.Y - joyPos.Y, 0, 0);
            //    }
            //}
        }

        private void Configuration_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ConfigurationPage));
        }

        private void ResetEmergency_Click(object sender, RoutedEventArgs e)
        {
            _droneClient.ResetEmergency();
            UpdateDisplay();
        }

        private void TakePicture_Click(object sender, RoutedEventArgs e)
        {
            _droneClient.TakePicture();
            UpdateDisplay();
        }

        private void PlayAnimationButton_Click(object sender, RoutedEventArgs e)
        {
            _droneClient.PlayAnimation(ARDRONE_ANIMATION.ARDRONE_ANIMATION_FLIP_LEFT);
            UpdateDisplay();
        }

        private void PlayLedAnimationButton_Click(object sender, RoutedEventArgs e)
        {
            _droneClient.PlayLedAnimation();
            UpdateDisplay();
        }

        private void StartVideoRecordingButton_Click(object sender, RoutedEventArgs e)
        {
            _droneClient.StartRecordingVideo();
            UpdateDisplay();
        }

        private void StopVideoRecordingButton_Click(object sender, RoutedEventArgs e)
        {
            _droneClient.StopRecordingVideo();
            UpdateDisplay();
        }
    }
}

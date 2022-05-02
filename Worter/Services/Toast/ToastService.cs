using System;
using System.Timers;

namespace Worter.Services.Toast
{
    public class ToastService : IDisposable
    {
        public event Action<string, ToastLevel> OnShow;
        public event Action OnHide;
        private Timer Countdown;

        public void ShowToast(string message, ToastLevel level)
        {
            OnShow?.Invoke(message, level);
            StartCountdown();
        }

        public void ShowError(string message)
        {
            ShowToast(message, ToastLevel.Error);
        }

        public void ShowWarning(string message)
        {
            ShowToast(message, ToastLevel.Warning);
        }

        public void ShowSucces(string message)
        {
            ShowToast(message, ToastLevel.Success);
        }

        private void StartCountdown()
        {
            SetCountdown();

            if (Countdown.Enabled)
            {
                Countdown.Stop();
                Countdown.Start();
            }
            else
            {
                Countdown.Start();
            }
        }

        private void SetCountdown()
        {
            if (Countdown == null)
            {
                Countdown = new Timer(2000);
                Countdown.Elapsed += HideToast;
                Countdown.AutoReset = false;
            }
        }

        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }

        public void Dispose()
        {
            Countdown?.Dispose();
        }
    }
}

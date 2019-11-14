using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserWithDelayedTriggering
{
    public class WebBrowserWithDelayedTrigger : WebBrowser
    {
        private int _delayInMiliseconds = 1000;
        private System.Windows.Forms.Timer _internalTimer = null;
        public int DelayInMiliseconds
        {
            get
            {
                return _delayInMiliseconds;
            }

            set
            {
                _delayInMiliseconds = value;

            }
        }


        public WebBrowserWithDelayedTrigger()
        {
            ResetTimer(false);
            this.DocumentCompleted += WebBrowserWithDelayedTrigger_DocumentCompleted;
        }

   

        public delegate void FinalDocumentCompleteCallback();
        public event FinalDocumentCompleteCallback DocumentCompletedFinally;
        public void TriggerDocumentCompletedFinally()
        {
            System.Diagnostics.Debug.WriteLine("Final document completed triggered");
            DocumentCompletedFinally?.Invoke();
        }

        private void WebBrowserWithDelayedTrigger_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Vanilla document complete triggered");
            if (_delayInMiliseconds == 0 || _internalTimer != null)
            {
                if (_internalTimer.Enabled)
                {
                    System.Diagnostics.Debug.WriteLine("STOPPING the timer");
                    _internalTimer.Stop();
                    ResetTimer();
                }
                else
                    ResetTimer();
            }
            else
                ResetTimer();


        }


        public void ResetTimer(bool StartTheTimer=true)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("Resetting timer to {0} miliseconds",this.DelayInMiliseconds));
            _internalTimer = new System.Windows.Forms.Timer();
            _internalTimer.Interval = _delayInMiliseconds;
            _internalTimer.Tick += _internalTimer_Tick;

            if (StartTheTimer)
            {
                System.Diagnostics.Debug.WriteLine("Starting...");
                _internalTimer.Start();
            }
        }

        private void _internalTimer_Tick(object sender, EventArgs e)
        {
            _internalTimer.Stop();
            TriggerDocumentCompletedFinally();
        }
    }
}

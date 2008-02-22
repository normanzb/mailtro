// * ThreadHelper by Norman Xu (http://blog.eroman.org)
// * ---------------------------------------------------------------
// * ThreadHelper help you to handle thread.
// * Version: 1.0.0.4
// * Modified date: 12/19/2007
// * 
// * This piece of code is totally free! spread and use it!

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace SMTP.Utility.ThreadHelper
{
    public class ThreadHelper
    {
        const long DEFUALTTIMEOUT = 600000;

        public delegate void DelProcess(StandardParameters sparam);
        public DelProcess Process;
        private Thread threadProcess;
        private ParameterizedThreadStart threadProcStarter;

        private Thread threadMonitor;
        private ParameterizedThreadStart threadMoniStarter;

        /// <summary>
        /// Enumerate current state of thread.
        /// </summary>
        public enum ThreadHelperState {
            NotStart,
            Processing,
            Completed,
            Cancel,
            Error
        }

        /// <summary>
        /// Standard parameter for ThreadHelper
        /// </summary>
        public class StandardParameters {

            private Object lockObj;
        	
            public StandardParameters() {
                lockObj = new Object();
                this.Parameters = new Collection<object>();
                this.CancelSignal = new SMTP.Utility.ThreadHelper.SyncSignaler();
                
            }

            private object _invoke;
            /// <summary>
            /// Get or set a value indicating the GUI invoker (should be inherited from Console);
            /// </summary>
            public Object Invoker {
                get {
                    return this._invoke;
                }
                set {
                    this._invoke = value;
                }
            }

            private Collection<object> _parameters;
            /// <summary>
            /// Add or remove whatever you like.
            /// </summary>
            /// <Revision>
            /// Norman Xu 11/19/2007 Add sync lock
            /// </Revision>
            public Collection<object> Parameters {
                get {
                	Collection<object> tem;
                	lock(lockObj){
                		tem = this._parameters;
                	}
                    return tem;
                }
                set {
                	lock(lockObj){
                    	this._parameters = value;
                    }
                }
            }

            private SMTP.Utility.ThreadHelper.SyncSignaler _cancelSignal;
            /// <summary>
            /// Get or set the Cancel Signal.
            /// </summary>
            public SMTP.Utility.ThreadHelper.SyncSignaler CancelSignal
            {
                set {
                    this._cancelSignal = value;
                }
                get {
                    return this._cancelSignal;
                }
            }
        }

        /// <summary>
        /// Initialize a ThreadHelper
        /// </summary>
        /// <param name="process">A delegate or function reference which match the signature of DelProcess</param>
        public ThreadHelper(DelProcess process) {
            this.Process = process;
            Initialize();
        }

        private SMTP.Utility.ThreadHelper.SyncSignaler _cancelSignal;
        /// <summary>
        /// Get or set the Cancel Signal.
        /// </summary>
        public SMTP.Utility.ThreadHelper.SyncSignaler CancelSignal
        {
            set {
                this._cancelSignal = value;
            }
            get {
                return this._cancelSignal;
            }
        }

        private ThreadHelperState _state;
        /// <summary>
        /// Get current state of thread.
        /// </summary>
        public ThreadHelperState State {
            get {
                return this._state;
            }
            private set {
                this._state = value;
            }
        }

        public Thread BaseThread {
            get {
                return this.threadProcess;
            }
        }

        private void Initialize(){

            this.State = ThreadHelperState.NotStart;
            threadProcStarter = new ParameterizedThreadStart(this.Runner);
            threadProcess = new Thread(threadProcStarter);
            threadMoniStarter = new ParameterizedThreadStart(this.MonitorProcess);
            threadMonitor = new Thread(threadMoniStarter);

        }

        private void Runner(object param) {
            this.Process.Invoke((StandardParameters)param);
        }

        /// <summary>
        /// Start the thread.
        /// </summary>
        /// <param name="sparam">Standard sparam</param>
        public void Run(StandardParameters sparam)
        {
            if (this.Process != null)
            {
                if (sparam.CancelSignal != null)
                    this.CancelSignal=sparam.CancelSignal;
                threadProcess.Start(sparam);
                threadMonitor.Start();
                this.State = ThreadHelperState.Processing;
            }

        }

        /// <summary>
        /// Wait until thread finished, default timeout is 10 minutes
        /// </summary>
        public void Wait() {
            this.Wait(DEFUALTTIMEOUT);
        }

        /// <summary>
        /// Wait until thread finished with specified timeout.
        /// </summary>
        /// <param name="millisecond">Millisecond timeout, -1 means infinite.</param>
        public void Wait(long millisecond) {
            DateTime startDT= DateTime.Now;
            while (true) {
                Application.DoEvents();
                DateTime curDT= DateTime.Now;
                if (threadProcess.IsAlive == false)
                {
                    this.State = ThreadHelperState.Completed;
                    break;
                }
                if (millisecond != -1 && (curDT.Ticks - startDT.Ticks) >= millisecond * 10000)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Cancel the thread.
        /// </summary>
        public void Cancel() {
            if (this.CancelSignal != null)
                this.CancelSignal.Set();
            threadProcess.Join(3000);
            threadProcess.Abort();
            this.State = ThreadHelperState.Cancel;
        }

        private void MonitorProcess(object sparam) {
            //Wait until runner thread finished.
            this.Wait(-1);
            this.State = ThreadHelperState.Completed;
        }
    }
}

// * ThreadHelper by Norman Xu (http://blog.eroman.org)
// * ---------------------------------------------------------------
// * ThreadHelper help you to handle thread.
// * Version: 1.0.0.2
// * Modified date: 12/10/2007
// * 
// * This piece of code is totally free! spread and use it!

using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Utility.ThreadHelper
{
    public class SyncSignaler
    {
        private bool flag;
        private object lockObj;

        public SyncSignaler()
        {
            this.flag = false;
            lockObj = new object();
        }

        /// <summary>
        /// Set signal
        /// </summary>
        public void Set()
        {
            lock (lockObj)
            {
                flag = true;
            }
        }

        /// <summary>
        /// Get a bool indicating whether a signal is set (true).
        /// </summary>
        /// <returns>If return true, then signal is set.</returns>
        public bool Get()
        {
            bool tem;
            lock (lockObj)
            {
                tem = flag;
            }
            return tem;
        }

        /// <summary>
        /// Get the signal value and then reset signal to false
        /// </summary>
        /// <returns>If return true, then signal is set.</returns>
        public bool GetAndRelease()
        {
            bool tem;
            lock (lockObj)
            {
                tem = flag;
                flag = false;
            }
            return tem;
        }
    }
}

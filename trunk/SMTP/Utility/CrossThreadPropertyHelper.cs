// * CrossThreadPropertyHelper by Norman Xu (http://eroman.org)
// * ---------------------------------------------------------------
// * CrossThreadPropertyHelper help you to get property value cross theadly.
// * Version: 1.0.0.0
// * Modified date: 2/22/2008
// * 
// * This piece of code is totally free! spread and use it!

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace SMTP.Utility.ThreadHelper
{
    class CrossThreadPropertyHelper
    {
        private delegate void DelSetPropertyType(object _context, string _key, object _value);
        private delegate object DelGetPropertyType(object _context, string _key);
        private static DelSetPropertyType DelegateSetProperty = new DelSetPropertyType(SetPropertyWorker);
        private static DelGetPropertyType DelegateGetProperty = new DelGetPropertyType(GetPropertyWorker);

        private static void SetPropertyWorker(object _context, string _key, object _value)
        {
            PropertyInfo[] _tempProInfo = _context.GetType().GetProperties();
            int i;
            for (i = 0; i < _tempProInfo.Length; i++)
            {
                if (_tempProInfo[i].Name == _key)
                    _tempProInfo[i].SetValue(_context, _value, null);
            }
        }
        private static object GetPropertyWorker(object _context, string _key)
        {
            PropertyInfo[] _tempProInfo = _context.GetType().GetProperties();
            int i;
            for (i = 0; i < _tempProInfo.Length; i++)
            {
                if (_tempProInfo[i].Name == _key)
                    break;
            }
            return _tempProInfo[i].GetValue(_context, null);
        }

        public static object GetProperty(ISynchronizeInvoke context, string key)
        {
            return context.Invoke(DelegateGetProperty, new Object[] {context, key} );
        }

        public static void SetProperty(ISynchronizeInvoke context, string key, object value)
        {
            context.Invoke(DelegateSetProperty, new Object[] { context, key, value });
        }

        public static void BeginSetProperty(ISynchronizeInvoke context, string key, object value)
        {
            context.BeginInvoke(DelegateSetProperty, new Object[] { context, key, value });
        }
    }
}

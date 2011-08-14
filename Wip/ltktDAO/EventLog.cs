﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ltktDAO
{
    public class EventLog
    {
        private string sLogFormat;
        private string sErrorTime;

        public EventLog()
        {
            //sLogFormat used to create log files format :

            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message

            sLogFormat = DateTime.Now.ToShortDateString().ToString()+" "+DateTime.Now.ToLongTimeString().ToString()+" ==> ";
                    
            //this variable used to create log filename format "

            //for example filename : ErrorLogYYYYMMDD

            string sYear    = DateTime.Now.Year.ToString();
            string sMonth    = DateTime.Now.Month.ToString();
            string sDay    = DateTime.Now.Day.ToString();
            sErrorTime = sYear+sMonth+sDay;
        }

        public void writeLog(string sPathName, string sErrMsg)
        {
            StreamWriter sw = new StreamWriter(sPathName + sErrorTime, true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }
    }
}

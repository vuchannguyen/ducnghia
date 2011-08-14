﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
namespace ltktDAO
{
    public class Sticky
    {
        // Lấy đường dẫn cơ sở dữ liệu
        static string strPathDB = DBHelper.strPathDB;
        /// <summary>
        /// Check existed record in DB
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_article"></param>
        /// <returns></returns>
        public bool checkExisted(int _type, int _article)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            IEnumerable<tblSticky> lst = DB.tblStickies.Where(r =>r.Type == _type && r.Article == _article);

            if (lst.Count() > 0)
            { 
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        public bool insertSticky(tblSticky record)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    DB.tblStickies.InsertOnSubmit(record);

                    DB.SubmitChanges();

                    ts.Complete();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        //public bool deleteSticky(
    }
}
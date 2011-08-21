﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ltktDAO
{
    public class News
    {
        // Lấy đường dẫn cơ sở dữ liệu
        static string strPathDB = DBHelper.strPathDB;

        #region Property

        #region Get Property
        /// <summary>
        /// Lấy tên tin tức theo id
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public static string getTitle(int newsID)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            IEnumerable<tblNew> lst = from record in DB.tblNews
                                      where record.ID == newsID
                                      select record;
            if (lst.Count() > 0)
            {
                return lst.ElementAt(0).Title;
            }
            return null;
        }

        /// <summary>
        /// Lấy ra đoạn tóm tắt của tin tức
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public static string getChapeau(int newsID)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            IEnumerable<tblNew> lst = from record in DB.tblNews
                                      where record.ID == newsID
                                      select record;

            if (lst.Count() > 0)
            {
                return lst.ElementAt(0).Chapaeu;
            }

            return null;
        }

        /// <summary>
        /// Lấy nội dung tin tức theo id
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public static string getContent(int newsID)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            IEnumerable<tblNew> lst = from record in DB.tblNews
                                      where record.ID == newsID
                                      select record;

            if (lst.Count() > 0)
            {
                return lst.ElementAt(0).Contents;
            }

            return null;
        }

        /// <summary>
        /// Lấy ngày post
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public static DateTime getPosted(int newsID)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            IEnumerable<tblNew> lst = from record in DB.tblNews
                                      where record.ID == newsID
                                      select record;

            if (lst.Count() > 0)
            {
                return (DateTime)lst.ElementAt(0).Posted;
            }

            return new DateTime(1970, 1, 1);
        }

        /// <summary>
        /// Lấy tên tác giả bài viết
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public static string getAuthor(int newsID)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            IEnumerable<tblUser> lst = from author in DB.tblUsers
                      join news in DB.tblNews on author.Username equals news.Author
                      where news.ID == newsID
                      select author;

            if (lst.Count() > 0)
            {
                return lst.ElementAt(0).DisplayName;
            }
            return null;
        }

        /// <summary>
        /// Lấy ra toàn bộ tin tức theo id
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public static tblNew getNews(int newsID)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            
            IEnumerable<tblNew> news = from record in DB.tblNews
                                       where record.ID == newsID
                                       select record;
            if (news.Count() > 0)
            {
                return news.ElementAt(0);
            }
            return null;
        }


        #endregion

        #region Set Property

        /// <summary>
        /// Đặt tên cho tin tức
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static Boolean setTitle(int newsID, string strTitle)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            using (TransactionScope ts = new TransactionScope())
            {
                var record = DB.tblNews.Single(TB => TB.ID == newsID);
                record.Title = strTitle;
                DB.SubmitChanges();

                ts.Complete();
            }

            return true;
        }

        /// <summary>
        /// Xét sa pô cho tin
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="strChapeau"></param>
        /// <returns></returns>
        public static Boolean setChapeau(int newsID, string strChapeau)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            using (TransactionScope ts = new TransactionScope())
            {
                var record = DB.tblNews.Single(TB => TB.ID == newsID);
                record.Chapaeu = strChapeau;
                DB.SubmitChanges();

                ts.Complete();
            }

            return true;
        }

        /// <summary>
        /// Xét ngày đăng tin
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="timePosted"></param>
        /// <returns></returns>
        public static Boolean setPosted(int newsID, DateTime timePosted)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            using (TransactionScope ts = new TransactionScope())
            {
                var record = DB.tblNews.Single(TB => TB.ID == newsID);
                record.Posted = timePosted;
                DB.SubmitChanges();

                ts.Complete();
            }

            return true;
        }

        /// <summary>
        /// Xét tác giả (ID)
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="authorID"></param>
        /// <returns></returns>
        public static Boolean setAuthor(int newsID, string author)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            using (TransactionScope ts = new TransactionScope())
            {
                var record = DB.tblNews.Single(TB => TB.ID == newsID);
                record.Author = author;
                DB.SubmitChanges();

                ts.Complete();
            }

            return true;
        }

        /// <summary>
        /// Xét nội dung tin tức
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static Boolean setContent(int newsID, string strContent)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            using (TransactionScope ts = new TransactionScope())
            {
                var record = DB.tblNews.Single(TB => TB.ID == newsID);
                record.Contents = strContent;
                DB.SubmitChanges();

                ts.Complete();
            }

            return true;
        }

        #endregion
        #endregion

        #region Method
        /// <summary>
        /// Get Latest News by posted day
        /// </summary>
        /// <param name="_numNews"></param>
        /// <returns></returns>
        public static IEnumerable<tblNew> getLatestNewsByDate(int _numNews)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            IEnumerable<tblNew> lst = (from p in DB.tblNews
                                      orderby p.Posted descending
                                       select p).Take(_numNews);
            return lst;
        }
        /// <summary>
        /// Kiểm tra sự tồn tại của 1 tin tức
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public static bool checkNewsExists(int newsID)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            IEnumerable<tblNew> lst = from record in DB.tblNews
                                      where record.ID == newsID
                                      select record;

            if (lst.Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Lấy ra số lượng tin tức
        /// </summary>
        /// <returns>int</returns>
        public static int countNews()
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            return DB.tblNews.Count();
        }

        /// <summary>
        /// Thêm tin tức
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Boolean</returns>
        public static Boolean insertNews(tblNew record)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    DB.tblNews.InsertOnSubmit(record);

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

        /// <summary>
        /// Thêm tin tức
        /// </summary>
        /// <param name="_title"></param>
        /// <param name="_chapeau"></param>
        /// <param name="_content"></param>
        /// <returns></returns>
        public static Boolean insertNews(string _author, string _title, string _chapeau, string _content)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    tblNew record = new tblNew();
                    record.Title = _title;
                    record.Chapaeu = _chapeau;
                    record.Contents = _content;
                    record.Author = _author;
                    record.Posted = DateTime.Now;

                    DB.tblNews.InsertOnSubmit(record);

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

        /// <summary>
        /// Cập nhật tin tức
        /// </summary>
        /// <param name="recordUpdate"></param>
        /// <returns></returns>
        public static Boolean updateNews(int newsID, tblNew recordUpdate)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var news = DB.tblNews.Single(n => n.ID == newsID);
                    news.Title = recordUpdate.Title;
                    news.Chapaeu = recordUpdate.Chapaeu;
                    news.Contents = recordUpdate.Contents;
                    news.Posted = recordUpdate.Posted;
                    news.Author = recordUpdate.Author;

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

        /// <summary>
        /// Tổng số tin
        /// </summary>
        /// <returns></returns>
        public static int numberOfNews()
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            var lst = from record in DB.tblNews
                      select record;

            return lst.Count();
        }

        /// <summary>
        /// Lấy toàn bộ tin tức
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<tblNew> getAll()
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            IEnumerable<tblNew> lst = from record in DB.tblNews
                                      select record;

            return lst;
        }

        /// <summary>
        /// Lấy count email từ id start
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<tblNew> fetchEmailList(int start, int count)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);
            IEnumerable<tblNew> lst = (from record in DB.tblNews
                                       select record).Skip(start).Take(count);

            return lst;
        }

        /// <summary>
        /// Xóa một tin tức
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public static Boolean deleteNews(int _id)
        {
            LTDHDataContext DB = new LTDHDataContext(@strPathDB);

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var news = DB.tblNews.Single(n => n.ID == _id);
                    
                    DB.tblNews.DeleteOnSubmit(news);
                    
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

        #endregion
    }
}

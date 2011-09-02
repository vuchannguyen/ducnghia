﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ltktDAO
{
    public static class CommonConstants
    {
        #region Common
            /// <summary>
            /// Number record on one tab
            /// </summary>
            public const int NUMBER_RECORD_ON_TAB = 8;

            /// <summary>
            /// Number record of relative a article
            /// </summary>
            public const int NUMBER_RECORD_RELATIVE = 5;

            /// <summary>
            /// number of character string
            /// </summary>
            public const int NUMBER_OF_CHARACTER_ON_STRING = 15;

            /// <summary>
            /// Blank value
            /// </summary>
            public const string BLANK = "";

            /// <summary>
            /// path of event log
            /// </summary>
            public const string LOG_FILE_PATH = "Log\\EventLog";
            
            /// <summary>
            /// Like page
            /// </summary>
            public const string LIKE = "Like";
            
            /// <summary>
            /// Dislike page
            /// </summary>
            public const string DISLIKE = "Dislike";
           
            /// <summary>
            /// guest user
            /// </summary>
            public const string USER_GUEST = "Guest";
            
            /// <summary>
            /// now
            /// </summary>
            public const string NOW = "now";
            
            /// <summary>
            /// comma character(,)
            /// </summary>
            public const string COMMA = ",";
            
            /// <summary>
            /// one space character(" ")
            /// </summary>
            public const string SPACE = " ";
            
            /// <summary>
            /// bar character
            /// </summary>
            public const string BAR = "-";
        #endregion

        #region State
            /// <summary>
            /// state is unchec
            /// </summary>
            public const int STATE_UNCHECK = 0;
            /// <summary>
            /// state is checked
            /// </summary>
            public const int STATE_CHECKED = 1;
            /// <summary>
            /// state is bad
            /// </summary>
            public const int STATE_BAD = 2;

            /// <summary>
            /// state is non active
            /// </summary>
            public const int STATE_NON_ACTIVE = 0;

            /// <summary>
            /// state is active
            /// </summary>
            public const int STATE_ACTIVE = 1;
            /// <summary>
            /// state is warning
            /// </summary>
            public const int STATE_WARNING = 2;
            /// <summary>
            /// state is kia 3 day
            /// </summary>
            public const int STATE_KIA_3D = 31;
            /// <summary>
            /// state is kia 1 week
            /// </summary>
            public const int STATE_KIA_1W = 32;
            /// <summary>
            /// state is kia 2 week
            /// </summary>
            public const int STATE_KIA_2W = 33;
            /// <summary>
            /// state is kia 3 week
            /// </summary>
            public const int STATE_KIA_3W = 34;
            /// <summary>
            /// state is kia 1 Month
            /// </summary>
            public const int STATE_KIA_1M = 35;
            /// <summary>
            /// state is deleted
            /// </summary>
            public const int STATE_DELETED = 4;
            /// <summary>
            /// state is pending
            /// </summary>
            public const int STATE_PENDING = 10;
            /// <summary>
            /// state is sticky
            /// </summary>
            public const int STATE_STICKY = 13;
            
        #endregion 
        
        #region Article type
            /// <summary>
            /// Article has type lecture
            /// </summary>
            public const int ARTICLE_TYPE_LECTURE = 0;
            /// <summary>
            /// Article has type a practise
            /// </summary>
            public const int ARTICLE_TYPE_PRACTISE = 1;
            /// <summary>
            /// Article has type Examination
            /// </summary>
            public const int ARTICLE_TYPE_EXAM = 2;
            /// <summary>
            /// Article IT with type word
            /// </summary>
            public const int IT_TYPE_OFFICE_WORD = 0;
            /// <summary>
            /// Article IT with type Excel
            /// </summary>
            public const int IT_TYPE_OFFICE_EXCEL = 1;
            /// <summary>
            /// Article IT with type Power point
            /// </summary>
            public const int IT_TYPE_OFFICE_POWERPOINT = 2;
            /// <summary>
            /// Article IT with type power point
            /// </summary>
            public const int IT_TYPE_OFFICE_ACCESS = 3;
            /// <summary>
            /// Article IT with type simple tip
            /// </summary>
            public const int IT_TYPE_OFFICE_SIMPLE_TIP = 4;
            /// <summary>
            /// Article IT type with type advance tip
            /// </summary>
            public const int IT_TYPE_OFFICE_ADVANCE_TIP = 5;
        #endregion

        #region Messages
            /// <summary>
            /// do not find out article record on DB
            /// </summary>
            public const string ARTICLE_EMPTY_RECORD = "Hiện tại chưa có bài viết nào";
            /// <summary>
            /// error text
            /// </summary>
            public const string COMMON_ERROR_TEXT = "Đã có lỗi xảy ra, xin vui lòng thử lại sau.";
            /// <summary>
            /// Upload successful
            /// </summary>
            public const string UPLOAD_SUCCESSFUL = "Upload thành công";
        #endregion

        #region Administrator Function (AF)
            /// <summary>
            /// Bật/Tắt thông báo
            /// </summary>
            public const string AF_ANNOUCEMENT = "ANNOUCEMENT";
            /// <summary>
            /// Bật/Tắt chức năng upload
            /// </summary>
            public const string AF_UPLOAD = "UPLOAD";
            /// <summary>
            /// Bật/Tắt chức năng đăng ký
            /// </summary>
            public const string AF_REGISTRY = "REGISTRY";
            /// <summary>
            /// Bật/tắt chức năng login
            /// </summary>
            public const string AF_LOGIN = "LOGIN";
            /// <summary>
            /// Bật/Tắt chức năng download
            /// </summary>
            public const string AF_DOWNLOAD = "DOWNLOAD";
            /// <summary>
            /// Bật/Tắt chức năng Comment
            /// </summary>
            public const string AF_COMMENT = "COMMENT";
            /// <summary>
            /// Bật/Tắt chức năng Comment không cần đăng nhập
            /// </summary>
            public const string AF_COMMENT_EASY = "COMMENT_EASY";
            /// <summary>
            /// Bật/Tắt chức năng đăng ký quảng cáo
            /// </summary>
            public const string AF_ADS = "ADS";
            /// <summary>
            /// Bật/Tắt chức năng upload tại trang LTDH
            /// </summary>
            public const string AF_UPLOAD_UNI = "UPLOAD_UNI";
            /// <summary>
            /// Bật/Tắt chức năng Download tại trang LTDH
            /// </summary>
            public const string AF_DOWNLOAD_UNI = "DOWNLOAD_UNI";
            /// <summary>
            /// Bật/Tắt chức năng Upload tiếng anh
            /// </summary>
            public const string AF_UPLOAD_EL = "UPLOAD_EL";
            /// <summary>
            /// Bật/Tắt chức năng Download Tiếng anh
            /// </summary>
            public const string AF_DOWNLOAD_EL = "DOWNLOAD_EL";
            /// <summary>
            /// Bật/Tắt chức năng Upload IT
            /// </summary>
            public const string AF_UPLOAD_IT = "UPLOAD_IT";
            /// <summary>
            /// Bật/Tắt chức năng Download IT
            /// </summary>
            public const string AF_DOWNLOAD_IT = "DOWNLOAD_IT";
            /// <summary>
            /// Bật/Tắt chức năng Xem tin tức
            /// </summary>
            public const string AF_NEWS_VIEW = "NEWS_VIEW";
            /// <summary>
            /// Bật/Tắt chức năng Post tin tức
            /// </summary>
            public const string AF_NEWS_POST = "NEWS_POST";
            /// <summary>
            /// Bật/Tắt chức năng Search
            /// </summary>
            public const string AF_SEARCH = "SEARCH";
            /// <summary>
            /// Bật/Tắt chức năng Liên hệ
            /// </summary>
            public const string AF_CONTACT = "CONTACT";
            /// <summary>
            /// Bật/Tắt chế độ Underconstruction
            /// </summary>
            public const string AF_UNDERCONTRUCTION = "UNDERCONS";
            /// <summary>
            /// Bật/Tắt chức năng gửi email
            /// </summary>
            public const string AF_EMAIL_SEND = "EMAIL_SEND";
        #endregion

        #region Statistic Function (SF)
            /// <summary>
            /// Thành viên login gần nhất
            /// </summary>
            public const string SF_LATEST_LOGIN = "LATEST_LOGIN";
            /// <summary>
            /// Tổng số bài viết
            /// </summary>
            public const string SF_NUM_ARTICLE = "NUM_ARTICLE";
            /// <summary>
            /// TỔng số bài trong trang English
            /// </summary>
            public const string SF_NUM_ARTICLE_ON_EL = "NUM_ARTICLE_ON_EL";
            /// <summary>
            /// Tổng số bài trong trang IT
            /// </summary>
            public const string SF_NUM_ARTICLE_ON_IT = "NUM_ARTICLE_ON_IT";
            /// <summary>
            /// Tổng số bài trong trang LTDH
            /// </summary>
            public const string SF_NUM_ARTICLE_ON_UNI = "NUM_ARTICLE_ON_UNI";
            /// <summary>
            /// Tổng số bài trong 1 tab tại trang chủ
            /// </summary>
            public const string SF_NUM_ARTICLE_ON_TAB = "NUM_ARTICLE_ON_TAB";
            /// <summary>
            /// Tổng số comment trong ngày
            /// </summary>
            public const string SF_NUM_COMMENT_A_DAY = "NUM_COMMENT_DAY";
            /// <summary>
            /// Tổng số upload trong  ngày
            /// </summary>
            public const string SF_NUM_UPLOAD_A_DAY = "NUM_UPLOAD_DAY";
            /// <summary>
            /// Tổng số download trong 1 ngày
            /// </summary>
            public const string SF_NUM_DOWNLOAD_A_DAY = "NUM_DOWNLOAD_DAY";
            /// <summary>
            /// Tổng số upload
            /// </summary>
            public const string SF_NUM_UPLOAD = "NUM_UPLOAD";
            /// <summary>
            /// Tổng số thành viên hiện tại
            /// </summary>
            public const string SF_NUM_USER = "NUM_USER";
            /// <summary>
            /// Tổng số thành viên đăng ký mới
            /// </summary>
            public const string SF_NUM_USER_REGISTRY = "NUM_USER_REGISTRY";
        #endregion
            
        #region Control Function (CF)
            /// <summary>
            /// Lấy tiêu đề web
            /// </summary>
            public const string CF_TITLE = "TITLE";
            /// <summary>
            /// Văn bản chào mừng tại trang chủ
            /// </summary>
            public const string CF_WELCOME_TEXT = "WELCOME_TEXT";
            /// <summary>
            /// Cấu hình email
            /// </summary>
            public const string CF_EMAIL_CONFIG = "EMAIL_CONFIG";
            /// <summary>
            /// Nội dung thông báo
            /// </summary>
            public const string CF_ANNOUCEMENT = "ANNOUCEMENT";
        #endregion
        
        #region Pages Name (PAGE)
            /// <summary>
        /// error Page
        /// </summary>
        public const string PAGE_ERROR = "Error.aspx";
        /// <summary>
        /// Login page
        /// </summary>
        public const string PAGE_LOGIN = "Login.aspx";
        /// <summary>
        /// Home page
        /// </summary>
        public const string PAGE_HOME = "Home.aspx";
        /// <summary>
        /// English page
        /// </summary>
        public const string PAGE_ENGLISH = "English.aspx";
        /// <summary>
        /// Informatics page
        /// </summary>
        public const string PAGE_INFORMATICS = "Informatics.aspx";
        /// <summary>
        /// University page
        /// </summary>
        public const string PAGE_UNIVERSITY = "ContestUniversity.aspx";
    #endregion

        #region Session Constants (SES)
            /// <summary>
            /// User on session
            /// </summary>
            public const string SES_USER = "User";
            /// <summary>
            /// Article SCO on session
            /// </summary>
            public const string SES_ARTICLE_SCO = "ArticleSCO";
            /// <summary>
            /// constant of session error text
            /// </summary>
            public const string SES_ERROR = "Error";
        #endregion

        #region Request Constants (REQ)
            /// <summary>
            /// section on request
            /// </summary>
            public const string REQ_SECTION = "sec";
            /// <summary>
            /// Id of item on request
            /// </summary>
            public const string REQ_ID = "id";
            /// <summary>
            /// subject on request
            /// </summary>
            public const string REQ_SUBJECT = "sub";
            /// <summary>
            /// time on request
            /// </summary>
            public const string REQ_TIME = "time";
            /// <summary>
            /// library on request
            /// </summary>
            public const string REQ_LIBRARY = "lib";
            /// <summary>
            /// Request Action
            /// </summary>
            public const string REQ_ACTION = "action";
        #endregion 
        
        #region Section Uni,It,EL (SEC)
            /// <summary>
            /// Section university code
            /// </summary>
            public const string SEC_UNIVERSITY_CODE = "uni";
            /// <summary>
            /// Section informatics code
            /// </summary>
            public const string SEC_INFORMATICS_CODE = "it";
            /// <summary>
            /// Section english code
            /// </summary>
            public const string SEC_ENGLISH_CODE = "el";
            /// <summary>
            /// Section university code
            /// </summary>
            public const string SEC_UNIVERSITY = "Luyện thi Đại học";
            /// <summary>
            /// Section informatics code
            /// </summary>
            public const string SEC_INFORMATICS = "Tin học";
            /// <summary>
            /// Section english code
            /// </summary>
            public const string SEC_ENGLISH = "Anh văn";
        #endregion

        #region Subjects Math,Phy,Chem,Bio,Lit,His,Geo,Eng (SUB)
            /// <summary>
            /// Môn Toán
            /// </summary>
            public const string SUB_MATHEMATICS = "Môn Toán";
            /// <summary>
            /// Môn Lý
            /// </summary>
            public const string SUB_PHYSICAL = "Môn Lý";
            /// <summary>
            /// Môn Hóa
            /// </summary>
            public const string SUB_CHEMICAL = "Môn Hóa";
            /// <summary>
            /// Môn Sinh
            /// </summary>
            public const string SUB_BIOGRAPHY = "Môn Sinh";
            /// <summary>
            /// Môn Văn
            /// </summary>
            public const string SUB_LITERATURE = "Môn Văn";
            /// <summary>
            /// Môn Sử
            /// </summary>
            public const string SUB_HISTORY = "Môn Sử";
            /// <summary>
            /// Môn Địa
            /// </summary>
            public const string SUB_GEOGRAPHY = "Môn Địa";
            /// <summary>
            /// Môn Anh
            /// </summary>
            public const string SUB_ENGLISH = "Môn Anh";
            /// <summary>
            /// Mã Môn Toán
            /// </summary>
            public const string SUB_MATHEMATICS_CODE = "math";
            /// <summary>
            /// Mã môn Lý
            /// </summary>
            public const string SUB_PHYSICAL_CODE = "phy";
            /// <summary>
            /// Mã môn Hóa
            /// </summary>
            public const string SUB_CHEMICAL_CODE = "chem";
            /// <summary>
            /// Mã Môn sinh
            /// </summary>
            public const string SUB_BIOGRAPHY_CODE = "bio";
            /// <summary>
            /// Mã Môn Văn
            /// </summary>
            public const string SUB_LITERATURE_CODE = "lit";
            /// <summary>
            /// Mã môn Sử
            /// </summary>
            public const string SUB_HISTORY_CODE = "his";
            /// <summary>
            /// Mã Môn địa
            /// </summary>
            public const string SUB_GEOGRAPHY_CODE = "geo";
            /// <summary>
            /// Mã Môn Anh
            /// </summary>
            public const string SUB_ENGLISH_CODE = "el";
        #endregion

        #region Templates (TEMP)
            /// <summary>
            /// University link template
            /// </summary>
            public const string TEMP_UNI_LINK = "<a href=\"ContestUniversity.aspx?sub={0}&time={1}\">{2}</a>";
            /// <summary>
            /// 
            /// </summary>
            public const string TEMP_UNI_LABEL = "<label>{0}</label>";
        #endregion

        #region Action request (ACT)
            /// <summary>
            /// Action Click
            /// </summary>
            public const string ACT_CLICK = "click";
        #endregion

        #region Advertisement (ADS)
            /// <summary>
            /// Banner Top
            /// </summary>
            public const string ADS_TOP_BANNER = "top";
            /// <summary>
            /// Banner Top Leader
            /// </summary>
            public const string ADS_TOP_LEADER_BANNER = "topleader";
            /// <summary>
            /// Banner Top Right
            /// </summary>
            public const string ADS_TOP_RIGHT_BANNER = "topright";
            /// <summary>
            /// Banner Middle right
            /// </summary>
            public const string ADS_MIDDLE_RIGHT_BANNER = "middleright";
            /// <summary>
            /// Banner middle right
            /// </summary>
            public const string ADS_BOTTOM_RIGHT_BANNER = "bottomright";
            /// <summary>
            /// Banner Top left
            /// </summary>
            public const string ADS_TOP_LEFT_BANNER = "topleft";
            /// <summary>
            /// Banner middle left
            /// </summary>
            public const string ADS_MIDDLE_LEFT_BANNER = "middleleft";
            /// <summary>
            /// Banner bottom left
            /// </summary>
            public const string ADS_BOTTOM_LEFT_BANNER = "bottomleft";
            /// <summary>
            /// Banner bottom 1
            /// </summary>
            public const string ADS_BOTTOM_1_BANNER = "bot1";
            /// <summary>
            /// Banner bottom 2
            /// </summary>
            public const string ADS_BOTTOM_2_BANNER = "bot2";
        #endregion

        #region SQL query (SQL)
            /// <summary>
            /// insert query template, require 2 argument
            /// </summary>
            public const string SQL_INSERT_SUCCESSFUL_TEMPLATE = " Insert record {0} to Table {1} successful";
            /// <summary>
            /// insert query template, require 2 argument
            /// </summary>
            public const string SQL_INSERT_FAILED_TEMPLATE = " Insert record {0} to Table {1} failed";
            /// <summary>
            /// delete query template, require 2 argument
            /// </summary>
            public const string SQL_DELETE_SUCCESSFUL_TEMPLATE = " Delete record {0} from Table {1} successful";
            /// <summary>
            /// delete query template, require 2 argument
            /// </summary>
            public const string SQL_DELETE_FAILED_TEMPLATE = " Delete record {0} from Table {1} failed";
            /// <summary>
            /// update query template, require 2 argument
            /// </summary>
            public const string SQL_UPDATE_SUCCESSFUL_TEMPLATE = " Update record {0} to Table {1} successful";
            /// <summary>
            /// update query template, require 2 argument
            /// </summary>
            public const string SQL_UPDATE_FAILED_TEMPLATE = " Delete record {0} to Table {1} failed";
        #endregion

        #region SQL table (SQL_TABLE)
            /// <summary>
            /// Table Informatics 
            /// </summary>
            public const string SQL_TABLE_INFORMATICS = "tblInformatics";
            /// <summary>
            /// Table ContestForUniversity
            /// </summary>
            public const string SQL_TABLE_CONTEST_UNIVERSITY = "tblContestForUniversity";
            /// <summary>
            /// Table English
            /// </summary>
            public const string SQL_TABLE_ENGLISH = "tblEnglish";
            /// <summary>
            /// Table News
            /// </summary>
            public const string SQL_TABLE_NEWS = "tblNews";
            /// <summary>
            /// Table User
            /// </summary>
            public const string SQL_TABLE_USER = "tblUser";
            /// <summary>
            /// Table Contact
            /// </summary>
            public const string SQL_TABLE_CONTACT = "tblContact";
            /// <summary>
            /// Table Admin
            /// </summary>
            public const string SQL_TABLE_ADMIN = "tblAdmin";
            /// <summary>
            /// Table Permission
            /// </summary>
            public const string SQL_TABLE_PERMISSION = "tblPermission";
            /// <summary>
            /// Table Statistic
            /// </summary>
            public const string SQL_TABLE_STATISTIC = "tblStatistic";
            /// <summary>
            /// Table Sticky
            /// </summary>
            public const string SQL_TABLE_STICKY = "tblSticky";
            /// <summary>
            /// Table Advertisement
            /// </summary>
            public const string SQL_TABLE_ADVERTISEMENT = "tblAdvertisement";
        #endregion
    }
}
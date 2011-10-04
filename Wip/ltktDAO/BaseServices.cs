﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ltktDAO
{
    public class BaseServices
    {
        public string getThumbnail(string thumbnail, string location)
        {
            thumbnail = nullToBlank(thumbnail);
            location = nullToBlank(location);

            //DirectoryInfo d = new DirectoryInfo(".");
            string sPath = DBHelper.strCurrentPath;
            FileInfo f = new FileInfo(sPath + thumbnail);
            
            if (thumbnail == "" || thumbnail == null || f.Exists == false)
            {
                thumbnail = "Data/DefaultImages/" + getDefaultAppropriateThumbnail(getExtension(sPath + location));
            }
            return thumbnail;
        }

        public string subString(string str)
        {
            if (str == null) return null;
            else if (str.Length > CommonConstants.DEFAULT_NUMBER_OF_CHARACTER_ON_STRING)
            {
                return (str.Substring(0, CommonConstants.DEFAULT_NUMBER_OF_CHARACTER_ON_STRING) + "..." );
            }
            return str.Trim();
        }

        public string getExtension(string location)
        {
            FileInfo f = new FileInfo(location);
            string res = "";
            if (f.Exists)
            {
                res = f.Extension;
            }
            return res;
        }
        
        public static int random(int min, int max)
        {
            Random rd = new Random();
            int r = 0;
            r = rd.Next(min, max);
            return r;
        }
        
        public string getDefaultAppropriateThumbnail(string extension)
        {
            string strThumbnail = CommonConstants.BLANK;
            switch (extension)
            {
                case ".doc":
                case ".docx":
                    strThumbnail = "word-icon.png";
                    break;
                case ".ppt":
                case ".pptx":
                case ".pps":
                case ".ppsx":
                    strThumbnail = "powerpoint-icon.png";
                    break;
                case ".rar":
                case ".zip":
                case ".7z":
                    strThumbnail = "archive-icon.png";
                    break;
                case ".pdf":
                    strThumbnail = "pdf-icon.png";
                    break;
                default:
                    strThumbnail = "unknown_icon.png";
                    break; ;
            }

            return strThumbnail;
        }

        /// <summary>
        /// Chuyển từ DateTime sang chuỗi
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string convertDateToString(DateTime date)
        {
            string strDate = CommonConstants.BLANK;
            if (date != null)
            {
                strDate += date.ToShortTimeString();
                strDate += " ngày ";
                strDate += Convert.ToString(date.Day);
                strDate += "/";
                strDate += Convert.ToString(date.Month);
                strDate += "/";
                strDate += Convert.ToString(date.Year);
            }
            return strDate;
        }

        public static int min(int a, int b)
        {
            if (a > b)
            {
                return b;
            }
            return a;
        }
        
        public static int max(int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            return b;
        }

        public static string nullToBlank(string target)
        {
            if (target == null)
            {
                return CommonConstants.BLANK;
            }
            return target.Trim();
        }
        
        public static int convertStringToInt(string target)
        {
            int r = 0;
            try
            {
                Int32.TryParse(target, out r);
            }
            catch
            {
                return 0;
            }
            return r;
        }
        
        public static string nullToSharp(string target)
        {
            if (target == null || target == CommonConstants.BLANK)
            {
                return CommonConstants.SHARP;
            }
            return target.Trim();
        }

        public static bool isNullOrBlank(string target)
        {
            if (target != null && target != CommonConstants.BLANK)
            {
                return false;
            }
            return true;
        }
        public static bool isNumeric(string target)
        {
            if (!isNullOrBlank(target))
            {
                int u = 0;
                return Int32.TryParse(target.Trim(), out u);
            }
            return false;
        }
        public static int getYearFromString(string target)
        {
            if (isNullOrBlank(target) || target == CommonConstants.NOW)
            {
                return DateTime.Now.Year;
            }
            return Int32.Parse(target);
        }
        
        public static string createMsgByTemplate(string template, string arg1)
        {
            string msg = CommonConstants.BLANK;
            if (!isNullOrBlank(template))
            {
                msg = String.Format(template, arg1.Trim());
            }
            return msg;
        }
        
        public static string createMsgByTemplate(string template, string arg1, string arg2)
        {
            string msg = CommonConstants.BLANK;
            if (!isNullOrBlank(template))
            {
                msg = String.Format(template, arg1.Trim(), arg2.Trim());
            }
            return msg;
        }
        
        public static string createMsgByTemplate(string template, string arg1, string arg2, string arg3)
        {
            string msg = CommonConstants.BLANK;
            if (!isNullOrBlank(template))
            {
                msg = String.Format(template, arg1.Trim(), arg2.Trim(), arg3.Trim());
            }
            return msg;
        }

        public static string createMsgByTemplate(string template, string arg1, string arg2, string arg3, string arg4)
        {
            string msg = CommonConstants.BLANK;
            if (!isNullOrBlank(template))
            {
                msg = String.Format(template, arg1.Trim(), arg2.Trim(), arg3.Trim(), arg4.Trim());
            }
            return msg;
        }
     
        public static int getValueClassByCode(string code)
        {
            switch (code)
            {
                case CommonConstants.AT_EL_CLASS_10_CODE:
                    {
                        return CommonConstants.AT_EL_CLASS_10;
                    }
                case CommonConstants.AT_EL_CLASS_11_CODE:
                    {
                        return CommonConstants.AT_EL_CLASS_11;
                    }
                case CommonConstants.AT_EL_CLASS_12_CODE:
                    {
                        return CommonConstants.AT_EL_CLASS_12;
                    }
            }
            return -1;
        }
        
        public static string getNameSubjectByCode(string code)
        {
            if(isNullOrBlank(code))
            {
                return CommonConstants.BLANK;
            }
            switch (code)
            {
                case CommonConstants.SUB_MATHEMATICS_CODE:
                    {
                        return CommonConstants.SUB_MATHEMATICS;
                    }
                case CommonConstants.SUB_PHYSICAL_CODE:
                    {
                        return CommonConstants.SUB_PHYSICAL;
                    }
                case CommonConstants.SUB_CHEMICAL_CODE:
                    {
                        return CommonConstants.SUB_CHEMICAL;
                    }
                case CommonConstants.SUB_BIOGRAPHY_CODE:
                    {
                        return CommonConstants.SUB_BIOGRAPHY;
                    }
                case CommonConstants.SUB_GEOGRAPHY_CODE:
                    {
                        return CommonConstants.SUB_GEOGRAPHY;
                    }
                case CommonConstants.SUB_LITERATURE_CODE:
                    {
                        return CommonConstants.SUB_LITERATURE;
                    }
                case CommonConstants.SUB_HISTORY_CODE:
                    {
                        return CommonConstants.SUB_HISTORY;
                    }
                case CommonConstants.SUB_ENGLISH_CODE:
                    {
                        return CommonConstants.SUB_ENGLISH;
                    }
                case CommonConstants.ALL:
                    {
                        return CommonConstants.ALL;
                    }
                case CommonConstants.PARAM_EL_COMMON:
                    {
                        return CommonConstants.PARAM_EL_COMMON_NAME;
                    }
                case CommonConstants.PARAM_EL_MAJOR:
                    {
                        return CommonConstants.PARAM_EL_MAJOR_NAME;
                    }
                case CommonConstants.PARAM_EL_CERT:
                    {
                        return CommonConstants.PARAM_EL_CERT_NAME;
                    }
                default:
                    {
                        return CommonConstants.BLANK;
                    }

            }
        }

        public string createOlderLink(string linkTemplate, ArticleSCO articleSCO, int numberOlder)
        {
            string links = CommonConstants.BLANK;
            int startYear = getYearFromString(CommonConstants.NOW);
            int selectedYear = getYearFromString(articleSCO.Time);

            if (articleSCO.Section == CommonConstants.SEC_UNIVERSITY_CODE)
            {
                for (int i = startYear; i >= 2000; i--)
                {
                    if (i == selectedYear)
                    {
                        links += String.Format(CommonConstants.TEMP_LABEL_TAG, i.ToString());
                    }
                    else
                    {
                        links += String.Format(linkTemplate, articleSCO.Subject, i.ToString(), CommonConstants.PAGE_NUMBER_FIRST, i.ToString());
                    }
                    links += CommonConstants.SPACE;
                }
            }
            return links;
        }
       
        public string createRatingBar(int score, int maxScore)
        {
            string data = CommonConstants.BLANK;
            
            if (score >= 0 && maxScore >= 0 && maxScore >= score)
            {
                maxScore = maxScore / 2;
                score = score / 2;
                int lackScore = maxScore - score;
                string item = CommonConstants.BLANK;

                //build score
                for (int i = 0; i < score; i++)
                {
                    item = createMsgByTemplate(CommonConstants.TEMP_IMG_RATING, CommonConstants.PATH_ACTIVE_RATING_ICON);
                    data += item;
                }

                //build lake score
                for (int i = 0; i < lackScore; i++)
                {
                    item = createMsgByTemplate(CommonConstants.TEMP_IMG_RATING, CommonConstants.PATH_INACTIVE_RATING_ICON);
                    data += item;
                }
            }
            return data;
        }
 
        public static int getTotalPage(int totalRecord, int numOnePage)
        {
           
            int mod = totalRecord % numOnePage;
            if (mod == 0)
            {
                return totalRecord / numOnePage;
            }
            return totalRecord / numOnePage + 1;
        }
 
        public static int getRecordFrom(int currentPage, int numberOnPage)
        {
            return (currentPage - 1) * numberOnPage;
        }
  
        public static int getRecordTo(int recordFrom, int numberOnPage)
        {
            return recordFrom + numberOnPage;
        }
    
        public static string createPagingLink(string url, int currentPage, int totalPage)
        {
            string links = CommonConstants.BLANK;
            string preLink = CommonConstants.BLANK;
            string sufLink = CommonConstants.BLANK;
            string backLink = CommonConstants.BLANK;
            string nextLink = CommonConstants.BLANK;
            string firstLink = CommonConstants.BLANK;
            string lastLink = CommonConstants.BLANK;
            string curPageLink = CommonConstants.BLANK;
            if (!BaseServices.isNullOrBlank(url))
            {
                if (currentPage < 0)
                {
                    currentPage = BaseServices.convertStringToInt(CommonConstants.PAGE_NUMBER_FIRST);
                }
                else if (currentPage > totalPage)
                {
                    currentPage = totalPage;
                }
                //prefix link
                int k = max(0, currentPage - 4);
                for (int i = k; i < currentPage; i++)
                {
                    preLink += BaseServices.createMsgByTemplate(CommonConstants.TEMP_A_TAG, url
                                                                + CommonConstants.AND + CommonConstants.REQ_PAGE + CommonConstants.EQUAL + (i + 1).ToString(),
                                                                (i + 1).ToString());
                    preLink += CommonConstants.SPACE;
                    preLink += CommonConstants.SPACE;
                }
                k = min(totalPage, currentPage + 4);
                for (int i = 0; i < k; i++)
                {
                    sufLink += BaseServices.createMsgByTemplate(CommonConstants.TEMP_A_TAG, url
                                                                + CommonConstants.AND + CommonConstants.REQ_PAGE + CommonConstants.EQUAL + (i + 1).ToString(),
                                                                (i + 1).ToString());
                    sufLink += CommonConstants.SPACE;
                    sufLink += CommonConstants.SPACE;
                }
                int next = 0;
                int back = 0;
                next = currentPage + 1;
                back = currentPage - 1;
                if (next > totalPage)
                {
                    next = totalPage;
                }
                if (back < convertStringToInt(CommonConstants.PAGE_NUMBER_FIRST))
                {
                    back = convertStringToInt(CommonConstants.PAGE_NUMBER_FIRST);
                }
               
                
                if (currentPage > convertStringToInt(CommonConstants.PAGE_NUMBER_FIRST))
                {
                    backLink = BaseServices.createMsgByTemplate(CommonConstants.TEMP_A_TAG, url
                                                               + CommonConstants.AND + CommonConstants.REQ_PAGE + CommonConstants.EQUAL + back.ToString()
                                                                  , CommonConstants.BACK_BUTTON);
                    firstLink = BaseServices.createMsgByTemplate(CommonConstants.TEMP_A_TAG, url
                                                                    + CommonConstants.AND + CommonConstants.REQ_PAGE + CommonConstants.EQUAL + CommonConstants.PAGE_NUMBER_FIRST
                                                                    , CommonConstants.FIRST_BUTTON);
                }
                else
                {
                    firstLink = createMsgByTemplate(CommonConstants.TEMP_LABEL_TAG, CommonConstants.FIRST_BUTTON);
                    backLink = createMsgByTemplate(CommonConstants.TEMP_LABEL_TAG, CommonConstants.BACK_BUTTON);
                }
                if (currentPage < totalPage)
                {
                    lastLink = BaseServices.createMsgByTemplate(CommonConstants.TEMP_A_TAG, url
                                                                    + CommonConstants.AND + CommonConstants.REQ_PAGE + CommonConstants.EQUAL + totalPage
                                                                    , CommonConstants.LAST_BUTTON);
                    nextLink = BaseServices.createMsgByTemplate(CommonConstants.TEMP_A_TAG, url
                                                                + CommonConstants.AND + CommonConstants.REQ_PAGE + CommonConstants.EQUAL + next.ToString()
                                                                , CommonConstants.NEXT_BUTTON);
                }
                else
                {
                    lastLink = CommonConstants.LAST_BUTTON;
                    nextLink = CommonConstants.NEXT_BUTTON;
                }
                curPageLink = BaseServices.createMsgByTemplate(CommonConstants.TEMP_A_TAG, url
                                                                + CommonConstants.AND + CommonConstants.REQ_PAGE + CommonConstants.EQUAL + currentPage
                                                                , currentPage.ToString());
                curPageLink = BaseServices.createMsgByTemplate(CommonConstants.TEMP_STRONG_TAG, curPageLink);
                
                links += firstLink + CommonConstants.SPACE + CommonConstants.SPACE;
                links += backLink + CommonConstants.SPACE + CommonConstants.SPACE;
                if (back != currentPage)
                {
                    links += preLink + CommonConstants.SPACE + CommonConstants.SPACE;
                }
                links += curPageLink + CommonConstants.SPACE + CommonConstants.SPACE;
                if (next != currentPage)
                {
                    links += sufLink + CommonConstants.SPACE + CommonConstants.SPACE;
                }

                links += nextLink + CommonConstants.SPACE + CommonConstants.SPACE;
                if (currentPage < totalPage)
                {
                    links += CommonConstants.DOT 
                        + CommonConstants.DOT 
                        + CommonConstants.DOT;
                }
                links += lastLink;
            }
            return links;
        }

        public bool checkFileType(string filename, string fileTypeAllows)
        {
            string ext = Path.GetExtension(filename);
            ext = ext.Substring(1, ext.Length - 1);
            //string fileTypeAllows = control.getValueString(CommonConstants.CF_FILE_TYPE_ALLOW);
            char[] delimiterChars = { ';', ',' };
            string[] arrFileTypeAllows = fileTypeAllows.Split(delimiterChars);

            foreach (string fileType in arrFileTypeAllows)
            {
                if (ext.Equals(fileType))
                    return true;
            }

            return false;
        }

        public string fileNameToSave(string fileName)
        {
            if (File.Exists(fileName))
            {
                return Path.GetFileNameWithoutExtension(fileName)
                              + "_2" + Path.GetExtension(fileName);
            }

            return fileName;
        }
        
        public static bool checkSizePattern(string size, string splitter)
        {
            if (!isNullOrBlank(size) && !isNullOrBlank(splitter))
            {
                string[] items = size.Split(splitter[0]);
                if (items.Length == 2)
                {
                    int o = 0;
                    if (Int32.TryParse(items[0],out o) && Int32.TryParse(items[1],out o))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static int[] getSizeFromPattern(string pattern, string splitter)
        {
            int[] res = new int[2] { 0, 0 };
            if (checkSizePattern(pattern, splitter))
            {
                string []items = pattern.Split(splitter[0]);
                res[0] = Int32.Parse(items[0]);
                res[1] = Int32.Parse(items[1]);
            }
            return res;
        }

        
    }
    
}

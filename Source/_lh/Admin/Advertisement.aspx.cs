﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DucNghia.Admin
{
    public partial class Advertisement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AdminMaster page = (AdminMaster)Master;
            page.updateHeader("Quản lý quảng cáo");
        }
    }
}
﻿using DataLayer;
using GidaGkpWeb.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static GidaGkpWeb.Global.Enums;
using GidaGkpWeb.Infrastructure.Authentication;

namespace GidaGkpWeb.Infrastructure.Utility
{
    public abstract class BaseViewPage : WebViewPage
    {
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
    }
}
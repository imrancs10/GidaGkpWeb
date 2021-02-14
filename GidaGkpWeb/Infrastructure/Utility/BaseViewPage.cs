using DataLayer;
using GidaGkpWeb.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static GidaGkpWeb.Global.Enums;
using GidaGkpWeb.Infrastructure.Authentication;
using GidaGkpWeb.BAL;
using GidaGkpWeb.Models;

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

        public virtual List<AdminNoticeModel> GetNoticeByType(string NoticeType)
        {
            AdminDetails _details = new AdminDetails();
            return _details.GetNoticeByType(NoticeType);
        }

        public virtual int GetEnablePaymentLink()
        {
            ApplicantDetails _details = new ApplicantDetails();
            return _details.GetEnablePaymentLink(((CustomPrincipal)User).Id);
        }

        public virtual int GetEnablePrintReciptLink()
        {
            ApplicantDetails _details = new ApplicantDetails();
            return _details.GetEnablePrintReciptLink(((CustomPrincipal)User).Id);
        }
    }
}
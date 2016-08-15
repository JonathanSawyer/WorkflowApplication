using IdemWokflow.Bll;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace IdemWokflow.Web.App_Start
{
    public class TraceExceptionLogger : ExceptionLogger
    {
        ISessionFactory _sessionFactory;

        public TraceExceptionLogger(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        public override void Log(ExceptionLoggerContext context)
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    session.Save(new ErrorLog()
                        {
                            Stack             = context.ExceptionContext.Exception         != null ? context.ExceptionContext.Exception.ToString()         : "",
                            ControllerContext = context.ExceptionContext.ControllerContext != null ? context.ExceptionContext.ControllerContext.ToString() : ""
                        });
                }
            }
            catch
            {
                //We dont want to get into an infinite loop here if this fails.
            }
        }
    }
}
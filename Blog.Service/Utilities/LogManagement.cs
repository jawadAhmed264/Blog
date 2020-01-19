using Blog.Data.Models;
using System;
using ViewModel;

namespace Blog.Service.Utilities
{
    public class LogManagement
    {
        public static int LogError(LogViewModel model)
        {
            using (BlogEntities db = new BlogEntities())
            {

                try
                {

                    Logger logger = new Logger
                    {
                        Action = model.Action,
                        Controller = model.Controller,
                        Active = model.Active,
                        Exception = model.Exception,
                        CreatedBy = model.CreatedBy,
                        CreatedDate = model.CreatedDate
                    };

                    db.Loggers.Add(logger);
                    db.SaveChanges();
                    db.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return 0;
        }
    }
}

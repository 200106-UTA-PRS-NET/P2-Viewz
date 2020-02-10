﻿using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    internal class PageRepositoryRetrieving : PageRepository
    {
        protected readonly IMdToHtmlAndContentsFactory _factory;
        internal PageRepositoryRetrieving(ViewzDbContext db, IMdToHtmlAndContentsFactory factory) : base(db)
        {
            _factory = factory;
        }
        protected override string GetHTML(long pageID)
        {
            try
            {
                return base.GetHTML(pageID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                IHtmlAndContents result = _factory.GetResult(base.GetMD(pageID));
                base.SetHTML(pageID, result.PageHTML);
                base.SetDetails(pageID, result.Contents);
                return result.PageHTML;
            }
        }
    }
}
using DataAccess;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ViewzApi.Models;
using ViewzApi.Controllers;
using Xunit;
using System.Net.Http;
using System;
using DataAccess.Interfaces;

namespace XUnitTestProject1
{
    public class ViewzApiTest
    {
        private IWikiRepository repository;

        [Fact]
        public void PostSetWiki()
        {
            // Arrange
           // WikiController controller = new WikiController(repository);

            
            // Act
            //Wiki wiki = new Wiki() { Url="", PageName = "some Wiki Name",Description=""};
            //var response = controller.Post(wiki.Url, wiki);

            // Assert
            //Assert.AreEqual("http://localhost/api/products/42", response.Headers.Location.AbsoluteUri);
        }
    }
} 

using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Exceptions
{
    public class DataAccessException: Exception
    {
        public DataAccessException(): base() { }
        public DataAccessException(string message) : base(message) { }
        public DataAccessException(string message, Exception innerException): base(message, innerException) { }
    }
    public class WikiNotFound : DataAccessException
    {
        public WikiNotFound() : base() { }
        public WikiNotFound(string message) : base(message) { }
        public WikiNotFound(string message, Exception innerException) : base(message, innerException) { }
    }
    public class PageNotFound : DataAccessException
    {
        public PageNotFound() : base() { }
        public PageNotFound(string message) : base(message) { }
        public PageNotFound(string message, Exception innerException) : base(message, innerException) { }
    }
    public class WikiExists : DataAccessException
    {
        public WikiExists() : base() { }
        public WikiExists(string message) : base(message) { }
        public WikiExists(string message, Exception innerException) : base(message, innerException) { }
    }
    public class PageExists : DataAccessException
    {
        public PageExists() : base() { }
        public PageExists(string message) : base(message) { }
        public PageExists(string message, Exception innerException) : base(message, innerException) { }
    }
}

using System;
using System.Runtime.Serialization;

namespace DataAccess.Exceptions
{
    [Serializable]
    public class DataAccessException: Exception
    {
        public DataAccessException(): base() { }
        public DataAccessException(string message) : base(message) { }
        public DataAccessException(string message, Exception innerException): base(message, innerException) { }
        protected DataAccessException(SerializationInfo info, StreamingContext context): base(info, context) { }
    }
    [Serializable]
    public class WikiNotFoundException : DataAccessException
    {
        public WikiNotFoundException() : base() { }
        public WikiNotFoundException(string message) : base(message) { }
        public WikiNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        protected WikiNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class PageNotFoundException : DataAccessException
    {
        public PageNotFoundException() : base() { }
        public PageNotFoundException(string message) : base(message) { }
        public PageNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        protected PageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class WikiExistsException : DataAccessException
    {
        public WikiExistsException() : base() { }
        public WikiExistsException(string message) : base(message) { }
        public WikiExistsException(string message, Exception innerException) : base(message, innerException) { }
        protected WikiExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class PageExistsException : DataAccessException
    {
        public PageExistsException() : base() { }
        public PageExistsException(string message) : base(message) { }
        public PageExistsException(string message, Exception innerException) : base(message, innerException) { }
        protected PageExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

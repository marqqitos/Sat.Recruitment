using System;

namespace Sat.Recruitment.Api.Exceptions
{
    public class EntityExistsException : Exception
    {
        public EntityExistsException(string message) : base(message) { }
    }
}

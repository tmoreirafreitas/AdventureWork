using System;

namespace AdventureWork.Infra.CrossCutting.MiddlewareFilterNotification.Exceptions
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string message) : base(message)
        {

        }
    }
}

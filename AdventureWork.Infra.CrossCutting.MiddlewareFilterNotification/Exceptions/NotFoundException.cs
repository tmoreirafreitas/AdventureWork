namespace AdventureWork.Infra.CrossCutting.MiddlewareFilterNotification.Exceptions
{
    public class NotFoundException : UserFriendlyException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}

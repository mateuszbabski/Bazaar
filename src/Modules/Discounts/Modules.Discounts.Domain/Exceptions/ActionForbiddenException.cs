namespace Modules.Discounts.Domain.Exceptions
{
    public class ActionForbiddenException : Exception
    {
        public ActionForbiddenException() : base(message: "You dont have an access to proceed this action")
        {
            
        }
    }
}

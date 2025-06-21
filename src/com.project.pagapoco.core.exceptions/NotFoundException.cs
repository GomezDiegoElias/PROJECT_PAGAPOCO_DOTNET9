namespace com.project.pagapoco.core.exceptions
{
    public class NotFoundException : Exception
    {

        private const string DESCRIPTION = "Not Found Exception";

        public NotFoundException(string detail) : base($"{DESCRIPTION}. {detail}") { }

    }
}

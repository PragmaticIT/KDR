namespace Kdr.ServiceInterfaces
{
    public class DeleteCategoryOutput
    {
        public DeleteCategoryOutput(bool success)
        {
            Success = success;
        }

        public bool Success { get; private set; }
    }
}
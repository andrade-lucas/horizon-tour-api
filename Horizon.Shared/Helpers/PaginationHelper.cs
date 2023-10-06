namespace Horizon.Shared.Helpers
{
    public static class PaginationHelper
    {
        public static int GetOffset(int page, int pageSize)
        {
            return page * pageSize;
        }
    }
}

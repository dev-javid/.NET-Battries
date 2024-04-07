using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Common.Abstract
{
    public abstract class PagedQuery<TResponse> : IRequest<PagedResponse<TResponse>>
    {
        [DefaultValue(1)]
        [Required]
        public int Page { get; set; }

        [DefaultValue(10)]
        [Required]
        public int Limit { get; set; }
    }

    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> items, int count, int page, int limit)
        {
            Page = page;
            Limit = limit;
            Total = count;
            Items = items;
        }

        public int Page { get; }

        public int Limit { get; }

        public int Total { get; }

        public IEnumerable<T> Items { get; }
    }
}

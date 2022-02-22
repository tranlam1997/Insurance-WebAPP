using System;
using System.Linq;
using System.Collections.Generic;
using Sem3Project.Models;

namespace Sem3Project.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(List<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        internal static PagedList<User> ToPagedList(User user, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        internal static PagedList<VehicleInsurance> ToPagedList(object p, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}

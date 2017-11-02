using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;


namespace MoqEFCoreExtension
{
    /// <summary>
    /// Mock Entity Framework Core中DbContext，加载List<T>或T[]到DbSet<T>
    /// </summary>
    public static class EFSetUpData
    {
        /// <summary>
        /// 加载List<T>到DbSet
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="mockSet">Mock<DbSet>对象</param>
        /// <param name="list">实体列表</param>
        /// <returns></returns>
        public static Mock<DbSet<T>> SetUpList<T>(this Mock<DbSet<T>> mockSet, List<T> list) where T : class
        {
            var queryable = list.AsQueryable();
            mockSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetEnumerator()).Returns(new UnitTestAsyncEnumerator<T>(queryable.GetEnumerator()));
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new UnitTestAsyncQueryProvider<T>(queryable.Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return mockSet;
        }
        /// <summary>
        /// 加载数据到DbSet
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="mockSet">Mock<DbSet>对象</param>
        /// <param name="array">实体数组</param>
        /// <returns></returns>
        public static Mock<DbSet<T>> SetUpArray<T>(this Mock<DbSet<T>> mockSet, params T[] array) where T : class
        {
            var queryable = array.AsQueryable();
            mockSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetEnumerator()).Returns(new UnitTestAsyncEnumerator<T>(queryable.GetEnumerator()));
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new UnitTestAsyncQueryProvider<T>(queryable.Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return mockSet;
        }
    }
}

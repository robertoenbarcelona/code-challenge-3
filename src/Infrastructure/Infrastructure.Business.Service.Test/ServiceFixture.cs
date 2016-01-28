
namespace Infrastructure.Business.Service.Test
{
    using FakeItEasy;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Infrastructure.Core.Exceptions;
    using Infrastructure.Data.Repositories;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ServiceFixture
    {
        [TestMethod]
        public void Service_FindCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var key = A.Dummy<int>();
            sut.Find(key);

            A.CallTo(() => rep.Find(key)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_FindThrowIfInvalidOperationException()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var key = A.Dummy<int>();
            A.CallTo(() => rep.Find(key)).Throws<InvalidOperationException>();
            var sut = new DummyService(rep);
            Action a = () => sut.Find(key);

            a.ShouldThrow<NoDataFoundException>();
        }

        [TestMethod]
        public void Service_FindThrowIfGenericException()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var key = A.Dummy<int>();
            A.CallTo(() => rep.Find(key)).Throws<Exception>();
            var sut = new DummyService(rep);
            Action a = () => sut.Find(key);

            a.ShouldThrow<DistribuitedException>();
        }

        [TestMethod]
        public async Task Service_FindAsyncCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var key = A.Dummy<int>();
            var res = await sut.FindAsync(key);

            A.CallTo(() => rep.FindAsync(key)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_FindAsyncThrowIfInvalidOperationException()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var key = A.Dummy<int>();
            A.CallTo(() => rep.FindAsync(key)).Throws<InvalidOperationException>();
            var sut = new DummyService(rep);
            Func<Task> a = async () =>
            {
                await sut.FindAsync(key);
            };

            a.ShouldThrow<NoDataFoundException>();
        }

        [TestMethod]
        public void Service_FindAsyncThrowIfGenericException()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var key = A.Dummy<int>();
            A.CallTo(() => rep.FindAsync(key)).Throws<Exception>();
            var sut = new DummyService(rep);
            Func<Task> a = async () =>
            {
                await sut.FindAsync(key);
            };

            a.ShouldThrow<DistribuitedException>();
        }

        [TestMethod]
        public async Task Service_FindAsyncCancelationTokenCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var key = A.Dummy<int>();
            var token = new System.Threading.CancellationToken();
            var res = await sut.FindAsync(token, key);

            A.CallTo(() => rep.FindAsync(token, key)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_FindAsyncCancelationTokenThrowIfInvalidOperationException()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var key = A.Dummy<int>();
            var token = A.Dummy<CancellationToken>();
            A.CallTo(() => rep.FindAsync(token, key)).Throws<InvalidOperationException>();
            var sut = new DummyService(rep);
            Func<Task> a = async () =>
            {
                await sut.FindAsync(token, key);
            };

            a.ShouldThrow<NoDataFoundException>();
        }

        [TestMethod]
        public void Service_FindAsyncCancelationTokenThrowIfGenericException()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var key = A.Dummy<int>();
            var token = A.Dummy<CancellationToken>();
            A.CallTo(() => rep.FindAsync(token, key)).Throws<Exception>();
            var sut = new DummyService(rep);
            Func<Task> a = async () =>
            {
                await sut.FindAsync(token, key);
            };

            a.ShouldThrow<DistribuitedException>();
        }

        [TestMethod]
        public void Service_InsertCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var entity = new DummyEntity();
            sut.Insert(entity);

            A.CallTo(() => rep.Insert(entity)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_InsertRangeCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var entities = new DummyEntity[] { new DummyEntity(), new DummyEntity() };
            sut.InsertRange(entities);

            A.CallTo(() => rep.InsertRange(entities)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_InsertOrUpdateGraphCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var entity = new DummyEntity();
            sut.InsertOrUpdateGraph(entity);

            A.CallTo(() => rep.InsertOrUpdateGraph(entity)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_InsertGraphRangeCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var entities = new DummyEntity[] { new DummyEntity(), new DummyEntity() };
            sut.InsertGraphRange(entities);

            A.CallTo(() => rep.InsertGraphRange(entities)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_UpdateCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var entity = new DummyEntity();
            sut.Update(entity);

            A.CallTo(() => rep.Update(entity)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_DeleteByIdCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var key = A.Dummy<int>();
            sut.Delete(key);
            A.CallTo(() => rep.Delete(key)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_DeleteObjectCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var entity = new DummyEntity();
            sut.Delete(entity);

            A.CallTo(() => rep.Delete(entity)).MustHaveHappened();
        }

        [TestMethod]
        public async Task Service_DeleteAsyncCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var key = A.Dummy<int>();
            var re = await sut.DeleteAsync(key);

            A.CallTo(() => rep.DeleteAsync(CancellationToken.None, key)).MustHaveHappened();
        }

        [TestMethod]
        public async Task Service_DeleteAsyncCancelationTokenCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var key = A.Dummy<int>();
            var token = new System.Threading.CancellationToken();
            var res = await sut.DeleteAsync(token, key);

            A.CallTo(() => rep.DeleteAsync(token, key)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_SelectQueryCallRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var key = A.Dummy<int>();
            var q = A.Dummy<string>();
            var i = A.Dummy<int>();
            var res = sut.SelectQuery(q, i);

            A.CallTo(() => rep.SelectQuery(q, i)).MustHaveHappened();
        }

        [TestMethod]
        public void Service_QueryReturnIQueryFluent()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            A.CallTo(() => rep.Query()).Returns(new DummyQueryFluent());
            var sut = new DummyService(rep);
            var res = sut.Query();

            res.Should().BeAssignableTo<IQueryFluent<DummyEntity>>();
        }

        [TestMethod]
        public void Service_QueryWithIQueryObjectReturnIQueryFluent()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var query = A.Dummy<IQueryObject<DummyEntity>>();
            A.CallTo(() => rep.Query(query)).Returns(new DummyQueryFluent());
            var sut = new DummyService(rep);
            var res = sut.Query(query);

            A.CallTo(() => rep.Query(query)).MustHaveHappened();
            res.Should().BeAssignableTo<IQueryFluent<DummyEntity>>();
        }

        [TestMethod]
        public void Service_QueryWithExpressionReturnIQueryFluent()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            Expression<Func<DummyEntity, bool>> exp = x => x.Id == 1;
            var sut = new DummyService(rep);
            var res = sut.Query(exp);

            A.CallTo(() => rep.Query(exp)).MustHaveHappened();
            res.Should().BeAssignableTo<IQueryFluent<DummyEntity>>();
        }

        [TestMethod]
        public void Service_IQueryableReturnIQueryable()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var res = sut.Queryable();

            A.CallTo(() => rep.Queryable()).MustHaveHappened();
            res.Should().BeAssignableTo<IQueryable<DummyEntity>>();
        }

        [TestMethod]
        public void Service_GetRepository()
        {
            var rep = A.Fake<IRepositoryAsync<DummyEntity>>();
            var sut = new DummyService(rep);
            var res = sut.Repository;

            res.Should().BeAssignableTo<IRepositoryAsync<DummyEntity>>();
        }

    }
}

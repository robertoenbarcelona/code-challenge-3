
namespace Infrastructure.Core.Test
{
    using FakeItEasy;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using FluentAssertions;
    using System.Threading;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AsyncLazyUnitTests
    {
        /// <summary>
        /// AsyncLaz not call function if never awaited. 
        /// </summary>
        [TestMethod]
        public void AsyncLazy_NeverAwaitedDoesNotCallFunc()
        {
            //Arrange
            Func<int> func = () =>
            {
                Assert.Fail();
                return A.Dummy<int>();
            };

            //Assert
            var lazy = new AsyncLazy<int>(func);
        }

        /// <summary>
        /// AsyncLaz start calls function if awaited.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task AsyncLazy_StartCallsFunc()
        {
            var tcs = new TaskCompletionSource<int>();
            Func<int> func = () =>
            {
                tcs.SetResult(A.Dummy<int>());
                return A.Dummy<int>();
            };

            var lazy = new AsyncLazy<int>(func);
            lazy.Start();
            var res = await tcs.Task;
        }

        [TestMethod]
        public async Task AsyncLazy_AwaitReturnsFuncValue()
        {
            var expected = A.Dummy<int>();
            Func<int> func = () =>
            {
                return expected;
            };
            var lazy = new AsyncLazy<int>(func);

            var result = await lazy;
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task AsyncLazy_MultipleAwaitersOnlyInvokeFuncOnce()
        {
            //Arrange
            int invokeCount = 0;
            var expected = A.Dummy<int>();
            var mre = new ManualResetEvent(false);
            Func<int> func = () =>
            {
                Interlocked.Increment(ref invokeCount);
                mre.WaitOne();
                return expected;
            };

            var lazy = new AsyncLazy<int>(func);
            var task1 = Task.Factory.StartNew(async () => await lazy).Result;
            var task2 = Task.Factory.StartNew(async () => await lazy).Result;
            task1.IsCompleted.Should().BeFalse();
            task2.IsCompleted.Should().BeFalse();

            //Act
            mre.Set();
            var results = await Task.WhenAll(task1, task2);

            //Assert
            results.Should().NotBeEmpty().And.HaveCount(2).And.ContainInOrder(new[] { expected, expected });
            invokeCount.Should().Be(1);
        }

        [TestMethod]
        public async Task AsyncLazy_MultipleAwaitersOnlyInvokeAsyncFuncOnce()
        {

            //Arrange
            int invokeCount = 0;
            var expected = A.Dummy<int>();
            var tcs = new TaskCompletionSource<int>();
            Func<Task<int>> func = async () =>
            {
                Interlocked.Increment(ref invokeCount);
                await tcs.Task;
                return expected;
            };

            var lazy = new AsyncLazy<int>(func);
            var task1 = Task.Factory.StartNew(async () => await lazy).Result;
            var task2 = Task.Factory.StartNew(async () => await lazy).Result;
            task1.IsCompleted.Should().BeFalse();
            task2.IsCompleted.Should().BeFalse();

            //Act
            tcs.SetResult(expected);
            var results = await Task.WhenAll(task1, task2);

            //Assert
            results.Should().NotBeEmpty().And.HaveCount(2).And.ContainInOrder(new[] { expected, expected });
            invokeCount.Should().Be(1);
        }
    }
}

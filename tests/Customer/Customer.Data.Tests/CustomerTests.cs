namespace Customer.Data.Tests
{
    using System;

    using Xunit;


    // Setup und Tear down je Test


    // Setup / Tear down
    public abstract class TestSetupBase : IDisposable
    {
        protected TestSetupBase()
        {
            // Vor jedem einzelnen Test
            Console.WriteLine(
                "Setup"
            );
        }

        public virtual void Dispose()
        {
            // Nach jedem einzelnen Test
            Console.WriteLine(
                "Tear down"
            );
        }
    }

    public class CustomerTests : TestSetupBase
    {
        public CustomerTests()
        {
            Console.WriteLine(
                "Setup"
            );
        }

        public override void Dispose()
        {
            Console.WriteLine(
                "TearDown"
            );

            base.Dispose();
        }

        [Fact]
        public void Test1()
        {
            Assert.True(
                true
            );
        }

        [Fact]
        public void Test2()
        {
            Assert.True(
                true
            );
        }
    }
}

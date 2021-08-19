namespace Customer.Data.Tests
{
    using System;

    using Xunit;


    // Setup und Tear down je Testklasse


    public class FixtureTests
    {
        public class SampleFixture : IDisposable
        {
            public SampleFixture()
            {
                // Vor allen Tests in einer Klasse
                Console.WriteLine(
                    "Setup"
                );
            }

            public void Dispose()
            {
                // Nach allen Tests in einer Klasse
                Console.WriteLine(
                    "Tear down"
                );
            }
        }

        public class MyFixtureTests : IClassFixture<SampleFixture>
        {
            private readonly SampleFixture _data;

            public MyFixtureTests(SampleFixture data) => _data = data;

            [Fact]
            public void TestTask1()
            {
                Assert.True(true);
            }

            [Fact]
            public void TestTask2()
            {
                Assert.True(true);
            }
        }

        public class AnotherFixtureTests : IClassFixture<SampleFixture>
        {
            private readonly SampleFixture _data;

            public AnotherFixtureTests(SampleFixture data) => _data = data;

            [Fact]
            public void TestTask1()
            {
                Assert.True(true);
            }

            [Fact]
            public void TestTask2()
            {
                Assert.True(true);
            }
        }
    }
}

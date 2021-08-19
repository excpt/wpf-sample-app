namespace Customer.Data.Tests
{
    using System.Collections.Generic;

    using Xunit;

    public class TestData
    {
        public class TestDataGenerator
        {
            public static IEnumerable<object[]> GetNumbersFromDataGenerator()
            {
                yield return new object[]
                {
                    5,
                    1,
                    3,
                    9
                };

                yield return new object[]
                {
                    7,
                    1,
                    5,
                    3
                };
            }
        }

        public class DataTests
        {
            public static IEnumerable<object[]> GetNumbersFromDataGenerator()
            {
                yield return new object[]
                {
                    1,
                    1,
                    1,
                    1
                };

                yield return new object[]
                {
                    0,
                    1,
                    1,
                    1
                };
            }

            [Theory]
            [MemberData(
                nameof(GetNumbersFromDataGenerator)
            )]
            public void RunInternal(
                int first,
                int second,
                int third,
                int fourth
            )
            {
                Assert.True(
                    first > 0
                );

                Assert.True(
                    second > 0
                );

                Assert.True(
                    third > 0
                );

                Assert.True(
                    fourth > 0
                );
            }

            [Theory]
            [MemberData(
                nameof(TestDataGenerator.GetNumbersFromDataGenerator),
                MemberType = typeof(TestDataGenerator)
            )]
            public void RunExternal(
                int first,
                int second,
                int third,
                int fourth
            )
            {
                Assert.True(
                    first > 0
                );

                Assert.True(
                    second > 0
                );

                Assert.True(
                    third > 0
                );

                Assert.True(
                    fourth > 0
                );
            }
        }
    }
}

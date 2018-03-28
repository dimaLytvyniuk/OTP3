using Xunit;
using Laba2.MyCollections;
using System;
using System.Collections.Generic;

namespace Laba3.MyCollections.Tests
{
    public class MyCollectionTest
    {
        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(-100000)]
        public void Add_ValidInt_SuccessfulAddInt(int item)
        {
            // Arrange
            var collection = new MyCollection<int>();

            // Act
            collection.Add(item);

            // Assert
            Assert.Equal(item, collection[0]);
        }

        [Theory]
        [InlineData("MyFirstTest")]
        [InlineData("SecondItem")]
        [InlineData("dhsfdhasjsdajsdjfd")]
        public void Add_ValidString_SuccessfulAddString(string item)
        {
            // Arrange
            var collection = new MyCollection<string>();

            // Act
            collection.Add(item);

            // Assert
            Assert.Equal(item, collection[0]);
        }

        [Fact]
        public void Add_ThirtyItems_AllItemsMustbeEquaWithCreatedArray()
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<int>();
            var createdArray = new int[arrayLength];
            for (var i = 0; i < arrayLength; i++)
                createdArray[i] = random.Next(10000);

            // Act
            for (var i = 0; i < arrayLength; i++)
                collection.Add(createdArray[i]);

            // Assert
            for (var i = 0; i < arrayLength; i++)
                Assert.Equal(createdArray[i], collection[i]);
        }

        [Fact]
        public void Count_EmptyCollection_ReturnZero()
        {
            // Arrange
            var collection = new MyCollection<int>();

            // Act
            int count = collection.Count;

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void Count_AddThirtyStrings_ReturnThirty()
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<string>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(10000).ToString());

            // Act
            int countInCollection = collection.Count;

            // Assert
            Assert.Equal(count, countInCollection);
        }

        [Theory]
        [InlineData(1, 100)]
        [InlineData(6, 4000)]
        [InlineData(20, -100000)]
        public void Insert_ValidArguments_SuccessfulInsert(int index, int item)
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(10000));

            // Act
            collection.Insert(index, item);

            // Assert
            var insertedItem = collection[index];
            Assert.Equal(item, insertedItem);
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(20, -1000)]
        public void Insert_ValidArguments_OldDataNotLoosing(int index, int item)
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<int>();
            var createdArray = new int[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                createdArray[i] = random.Next(10000);
                collection.Add(createdArray[i]);
            }

            // Act
            collection.Insert(index, item);

            // Assert
            for (var i = index + 1; i < collection.Count; i++)
            {
                Assert.Equal(createdArray[i - 1], collection[i]);
            }
            for (var i = index - 1; i >= 0; i--)
            {
                Assert.Equal(createdArray[i], collection[i]);
            }
        }

        [Theory]
        [InlineData(30, "Thirty")]
        [InlineData(-1, "MinusOne")]
        [InlineData(1000, "OnneThousand")]
        public void Insert_IndexOfItemOutOfRange_ThrowsArgumentOutOfRangeException(int index, string item)
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<string>();
            for (var i = 0; i < arrayLength; i++)
            {
                collection.Add(random.Next(10000).ToString());
            }

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.Insert(index, item));
        }

        [Theory]
        [InlineData(20)]
        [InlineData(29)]
        public void GetByIndex_ValidIndex_ReturnEqulWithArrayItem(int index)
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<string>();
            var createdArray = new int[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                createdArray[i] = random.Next(10000);
                collection.Add(createdArray[i].ToString());
            }

            // Act
            string item = collection[index];

            // Assert
            Assert.Equal(createdArray[index].ToString(), item);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(-1)]
        [InlineData(1000)]
        public void GetByIndex_InvalidIndex_ThrowsOutofRangeException(int index)
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<string>();
            for (var i = 0; i < arrayLength; i++)
            {
                collection.Add(random.Next(10000).ToString());
            }

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => collection[index]);
        }

        [Theory]
        [InlineData(1, 100)]
        [InlineData(6, 4000)]
        [InlineData(20, -100000)]
        public void InsertByIndex_ValidArguments_SuccessfulInsert(int index, int item)
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(10000));

            // Act
            collection[index] = item;

            // Assert
            var insertedItem = collection[index];
            Assert.Equal(item, insertedItem);
        }

        [Theory]
        [InlineData(30, "Thirty")]
        [InlineData(-1, "MinusOne")]
        [InlineData(1000, "OnneThousand")]
        public void InsertByIndex_IndexOfItemOutOfRange_ThrowsArgumentOutOfRangeException(int index, string item)
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<string>();
            for (var i = 0; i < arrayLength; i++)
            {
                collection.Add(random.Next(10000).ToString());
            }

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => collection[index] = item);
        }

        [Fact]
        public void Clear_ThityItemsInArray_EmptyCollection()
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<string>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(10000).ToString());

            // Act
            collection.Clear();

            // Assert
            Assert.Empty(collection);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(-100000)]
        public void Contains_ItemsIsPresent_ReturnTrue(int item)
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(10000));
            collection.Add(item);

            // Assert
            Assert.True(collection.Contains(item));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(-100000)]
        public void Contains_ItemsIsNotPresent_ReturnFalse(int item)
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(50,10000));

            // Assert
            Assert.False(collection.Contains(item));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(20)]
        public void CopyTo_CorrectArguments_DestinationArrayShouldContainsFullCollection(int index)
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<string>();
            var destinationArray = new string[100];
            for (var i = 0; i < arrayLength; i++)
            {
                collection.Add(random.Next(10000).ToString());
            }
            for (var i = 0; i < index; i++)
            {
                destinationArray[i] = random.Next(10000).ToString();
            }

            // Act
            collection.CopyTo(destinationArray, index);

            // Assert
            for (int i = 0; i < arrayLength; i++)
            {
                Assert.Equal(collection[i], destinationArray[i + index]);
            }
        }

        [Fact]
        public void CopyTo_NullArgument_ThrowsArgumentNullException()
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(50, 10000));
            int[] array = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => collection.CopyTo(array, 0));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100000)]
        [InlineData(200)]
        public void CopyTo_IndexesOutOfRangeInDestionationArray_ThrowsArgumentOutOfRangeException(int index)
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<string>();
            var destinationArray = new string[100];
            for (var i = 0; i < arrayLength; i++)
            {
                collection.Add(random.Next(10000).ToString());
            }

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.CopyTo(destinationArray, index));
        }

        [Theory]
        [InlineData(80)]
        [InlineData(92)]
        [InlineData(99)]
        public void CopyTo_CollectionBiggerThanSpaceInArray_ThrowsArgumentException(int index)
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<string>();
            var destinationArray = new string[100];
            for (var i = 0; i < arrayLength; i++)
            {
                collection.Add(random.Next(10000).ToString());
            }

            // Act and Assert
            Assert.Throws<ArgumentException>(() => collection.CopyTo(destinationArray, index));
        }

        [Fact]
        public void GetEnumerator_EqualElementsWithSourceArray()
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<int>();
            var createdArray = new int[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                createdArray[i] = random.Next(10000);
                collection.Add(createdArray[i]);
            }

            // Assert
            int index = 0;
            foreach (var item in collection)
            {
                Assert.Equal(createdArray[index], item);
                index++;
            }
        }

        [Theory]
        [InlineData(1, 100)]
        [InlineData(6, 4000)]
        [InlineData(20, -100000)]
        public void IndexOf_ValidArguments_ReturnCorrectIndex(int index, int item)
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(10000));
            collection[index] = item;

            // Act
            var foundedIndex = collection.IndexOf(item);

            // Assert
            Assert.Equal(index, foundedIndex);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(4000)]
        [InlineData(-100000)]
        public void IndexOf_InValidArguments_ReturnMinusOne(int item)
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(5000, 10000));

            // Act
            var foundedIndex = collection.IndexOf(item);

            // Assert
            Assert.Equal(-1, foundedIndex);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(20)]
        public void Remove_Presented_ReturnTrue(int item)
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(10000));
            collection.Add(item);

            // Act
            var removed = collection.Remove(item);

            // Assert
            Assert.True(removed);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(4000)]
        [InlineData(-100000)]
        public void Remove_InValidArguments_ReturnFalse(int item)
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(5000, 10000));

            // Act
            var removed = collection.Remove(item);

            // Assert
            Assert.False(removed);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(20)]
        public void RemoveAt_ValidArguments_SuccessfulRemove(int index)
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(10000));

            // Act
            var item = collection[index];
            collection.RemoveAt(index);

            // Assert
            Assert.Equal(count - 1, collection.Count);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(4000)]
        [InlineData(-100000)]
        public void RemoveAt_IndexOutOfRange_ThrowsArgumentOutOfRangeException(int index)
        {
            // Arrange
            var random = new Random();
            var count = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < count; i++)
                collection.Add(random.Next(5000, 10000));

            // Assert and Act
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.RemoveAt(index));
        }

        [Fact]
        public void Sort_CorrectDescendingComparer_ColectionBecameSorted()
        {
            // Arrange
            var random = new Random();
            var arrayLength = 30;
            var collection = new MyCollection<int>();
            for (var i = 0; i < arrayLength; i++)
            {
                collection.Add(random.Next(10000));
            }

            // Act
            collection.Sort(new MyDescendingIntComparer());

            // Assert
            for (int i = 0; i < arrayLength - 1; i++)
            {
                Assert.True(collection[i] > collection[i + 1]);
            }
        }

        class MyDescendingIntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (x > y)
                    return -1;
                else if (x == y)
                    return 0;
                else
                    return 1;
            }
        }
    }
}

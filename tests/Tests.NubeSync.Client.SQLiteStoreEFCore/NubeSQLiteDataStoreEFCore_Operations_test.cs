﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NubeSync.Core;
using Xunit;

namespace Tests.NubeSync.Client.SQLiteStoreEFCore.NubeSQLiteDataStoreEFCore_Operations_test
{
    public class Always : NubeSQLiteDataStoreEFCoreTestBase
    {
        [Fact]
        public async Task Add_operations_adds()
        {
            var result = await DataStore.AddOperationsAsync(Operations);

            Assert.True(result);
            var operations = await DataStore.GetOperationsAsync();
            Assert.Equal(Operations.Length, operations.Count());
        }

        [Fact]
        public async Task Add_operations_does_not_throw_when_list_is_empty()
        {
            await DataStore.InitializeAsync();

            var result = await DataStore.AddOperationsAsync(new List<NubeOperation>().ToArray());

            Assert.True(result);
        }

        [Fact]
        public async Task Add_operations_does_not_throw_when_list_is_null()
        {
            await DataStore.InitializeAsync();

            var result = await DataStore.AddOperationsAsync(null);

            Assert.True(result);
        }

        [Fact]
        public async Task Delete_operations_deletes()
        {
            await DataStore.AddOperationsAsync(Operations);

            await DataStore.DeleteOperationsAsync(Operations);

            var operations = await DataStore.GetOperationsAsync();
            Assert.Empty(operations);
        }

        [Fact]
        public async Task Delete_operations_does_not_throw_when_list_is_empty()
        {
            await DataStore.InitializeAsync();

            var result = await DataStore.DeleteOperationsAsync(new List<NubeOperation>().ToArray());

            Assert.True(result);
        }

        [Fact]
        public async Task Delete_operations_does_not_throw_when_list_is_null()
        {
            await DataStore.InitializeAsync();

            var result = await DataStore.DeleteOperationsAsync(null);

            Assert.True(result);
        }

        [Fact]
        public async Task Get_operations_can_be_paginated()
        {
            var expectedCount = 3;
            await DataStore.AddOperationsAsync(Operations);

            var operations = await DataStore.GetOperationsAsync(expectedCount);

            Assert.True(expectedCount < Operations.Count());
            Assert.Equal(expectedCount, operations.Count());
        }

        [Fact]
        public async Task Get_operations_returns_all_operations()
        {
            await DataStore.AddOperationsAsync(Operations);

            var operations = await DataStore.GetOperationsAsync();

            Assert.Equal(Operations.Length, operations.Count());
        }
    }
}
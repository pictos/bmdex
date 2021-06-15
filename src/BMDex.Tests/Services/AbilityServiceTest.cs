using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoBogus;
using BMDex.Services;
using FakeItEasy;
using FluentAssertions;
using PokeApiNet;
using Xunit;

namespace BMDex.Tests.Services
{
    public class AbilityServiceTest
    {
        IAbilityService _abilityService;
        private PokeApiClient _pokeApiClientMock;

        public AbilityServiceTest()
        {
            _pokeApiClientMock = A.Fake<PokeApiClient>();
            _abilityService = new AbilityService(_pokeApiClientMock);
        }

        [Theory]
        [InlineData(20, 0, 20)]
        [InlineData(100, 300, 27)]
        public async Task GetAbilities(int limit, int offset, int expected)
        {
            var namedApiResourceList = new AutoFaker<NamedApiResourceList<Ability>>().Generate();
            var abilities = new AutoFaker<Ability>().Generate(expected);

            A.CallTo(() => _pokeApiClientMock
                    .GetNamedResourcePageAsync<Ability>(
                        A.Dummy<int>(),
                        A.Dummy<int>(),
                        A.Dummy<CancellationToken>()))
                .Returns(namedApiResourceList);

            A.CallTo(() => _pokeApiClientMock.GetResourceAsync(namedApiResourceList.Results))
                .Returns(abilities);

            var result = await _abilityService.GetAbilities(limit, offset);

            result.Should().NotBeNull();
            result.Count.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, "stench")]
        [InlineData(100, "stall")]
        [InlineData(253, "perish-body")]
        public async Task GetAbilityByValidId(int id, string expected)
        {
            var result = await _abilityService.GetAbilityById(id);

            result.Should().NotBeNull();
            result.Name.Should().Be(expected);
        }

        [Theory]
        [InlineData("stench", 1)]
        [InlineData("stall", 100)]
        [InlineData("perish-body", 253)]
        public async Task GetAbilityByValidName(string name, int expected)
        {
            var result = await _abilityService.GetAbilityByName(name);

            result.Should().NotBeNull();
            result.Id.Should().Be(expected);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("burgermonkeys")]
        public async Task GetAbilityByInvalidName(string name)
        {
            var result = await _abilityService.GetAbilityByName(name);

            result.Should().BeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        public async Task GetAbilityByInvalidId(int id)
        {
            var result = await _abilityService.GetAbilityById(id);
            result.Should().BeNull();
        }
    }
}
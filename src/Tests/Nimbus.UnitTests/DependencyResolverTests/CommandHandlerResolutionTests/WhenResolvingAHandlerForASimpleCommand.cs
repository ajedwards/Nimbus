﻿using System.Threading.Tasks;
using Nimbus.Handlers;
using Nimbus.UnitTests.DependencyResolverTests.CommandHandlerResolutionTests.Handlers;
using Nimbus.UnitTests.DependencyResolverTests.CommandHandlerResolutionTests.MessageContracts;
using Nimbus.UnitTests.DependencyResolverTests.TestInfrastructure;
using NUnit.Framework;
using Shouldly;

namespace Nimbus.UnitTests.DependencyResolverTests.CommandHandlerResolutionTests
{
    [TestFixture]
    public class WhenResolvingAHandlerForASimpleCommand : TestForAllDependencyResolvers
    {
        protected override async Task When()
        {
        }

        [Test]
        [TestCaseSource("TestCases")]
        public async Task TheHandlerTypeShouldBeCorrect(AllDependencyResolversTestContext context)
        {
            await Given(context);
            await When();

            using (var scope = Subject.CreateChildScope())
            {
                var componentName = typeof (BrokerTestCommandHandler).FullName;
                var handler = scope.Resolve<IHandleCommand<FooCommand>>(componentName);
                handler.ShouldBeTypeOf<BrokerTestCommandHandler>();
            }
        }
    }
}
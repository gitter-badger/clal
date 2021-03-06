﻿using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherModel.UnitTest
{
    public class CmdApplicationConfigurationServiceTests
    {
        [Theory, AutoMoqData]
        public void NullForCtorArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationConfigurationService).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void CtorParametersAreInitialized(IFixture fixture)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationConfigurationService));
        }

        [Theory, AutoMoqData]
        public void ExecuteSaveCmdApplicationConfigurationCommandWithNullThrowsException(CmdApplicationConfigurationService sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.Execute(null));
        }

        [Theory, AutoMoqData]
        public void ExecuteSaveCmdApplicationConfigurationWithExistingNameRaisesRejectedEvent(
            [Frozen]Mock<ICmdApplicationConfigurationRepository> repository,
            SaveCmdApplicationConfigurationCommand command,
            TestDomainEventHandler<CmdApplicationConfigurationSaveRejected> testHandler,
            CmdApplicationConfigurationService sut)
        {
            repository
                .Setup(a => a.CheckIfConfigurationWithSameNameExists(command.ApplicationConfiguration))
                .Returns(true);
            DomainEvents.Subscribe(testHandler);
            sut.Execute(command);
            Assert.True(testHandler.EventHandlerInvoked);
        }
    }
}

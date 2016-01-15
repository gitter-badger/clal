﻿using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherUI.ViewModel;
using GalaSoft.MvvmLight;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest.ViewModel
{
    public class CmdApplicationViewModelTests
    {
        [Theory, AutoData]
        public void SutIsViewModelBase(IFixture fixture, Name name)
        {
            fixture.Inject<ParameterMeta>(ParameterMeta.Create<IParameter>(name));
            var sut = fixture.Create<CmdApplicationViewModel>();
            Assert.IsAssignableFrom<ViewModelBase>(sut);
        }

        [Theory, AutoData]
        public void CtorWithNullArgumentsThrowsException(IFixture fixture, Name name)
        {
            fixture.Inject<ParameterMeta>(ParameterMeta.Create<IParameter>(name));
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationViewModel).GetConstructors(System.Reflection.BindingFlags.Public));
        }

        [Theory, AutoData]
        public void CtorParametersAreInitialized(IFixture fixture, Name name)
        {
            fixture.Inject<ParameterMeta>(ParameterMeta.Create<IParameter>(name));
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationViewModel).GetConstructors(System.Reflection.BindingFlags.Public));
        }
    }
}
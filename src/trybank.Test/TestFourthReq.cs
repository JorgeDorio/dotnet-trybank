using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestFourthReq
{
    [Theory(DisplayName = "Deve transefir um valor com uma conta logada")]
    [InlineData(50, 20)]
    public void TestTransferSucess(int balance, int value)
    {        
			Trybank tb = new Trybank();

			tb.RegisterAccount(11,11,11);
			tb.RegisterAccount(22,22,22);
			tb.Login(11,11,11);
			tb.Bank[tb.loggedUser,3] = balance;

			tb.Transfer(22,22, value);

			tb.Bank[tb.loggedUser,3].Should().Be(balance - value);
			tb.Bank[1,3].Should().Be(value);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0)]
    public void TestTransferWithoutLogin(int value)
    {        
			Trybank tb = new Trybank();

			Action act = () => tb.Transfer(1, 1, value);

			act.Should().Throw<AccessViolationException>().WithMessage("Usuário já está logado");
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(30, 50)]
    public void TestTransferWithoutBalance(int balance, int value)
    {        
			Trybank tb = new Trybank();

			tb.RegisterAccount(11,11,11);
			tb.RegisterAccount(22,22,22);
			tb.Login(11,11,11);
			tb.Bank[tb.loggedUser,3] = balance;

			Action act = () => tb.Transfer(1, 1, value);
			
			act.Should().Throw<InvalidOperationException>().WithMessage("Saldo insuficiente");
    }
}

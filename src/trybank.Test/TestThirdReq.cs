using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestThirdReq
{
    [Theory(DisplayName = "Deve devolver o saldo em uma conta logada")]
    [InlineData(50)]
    public void TestCheckBalanceSucess(int balance)
    {        
			Trybank tb = new Trybank();

			tb.Logged = true;
			tb.loggedUser = 0;
			tb.Bank[0,3] = balance;

			int amount = tb.CheckBalance();

			amount.Should().Be(balance);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0)]
    public void TestCheckBalanceWithoutLogin(int balance)
    {        
			Trybank tb = new Trybank();

			Action act = () => tb.CheckBalance();

			act.Should().Throw<AccessViolationException>().WithMessage("Usuário já está logado");
    }

    [Theory(DisplayName = "Deve depositar um saldo em uma conta logada")]
    [InlineData(50)]
    public void TestDepositSucess(int value)
    {        
			Trybank tb = new Trybank();

			tb.Logged = true;
			tb.loggedUser = 0;
			tb.Deposit(value);

			tb.Bank[0,3].Should().Be(value);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0)]
    public void TestDepositWithoutLogin(int value)
    {        
			Trybank tb = new Trybank();

			Action act = () => tb.Deposit(value);

			act.Should().Throw<AccessViolationException>().WithMessage("Usuário já está logado");
    }

    [Theory(DisplayName = "Deve sacar um valor em uma conta logada")]
    [InlineData(50, 30)]
    public void TestWithdrawSucess(int balance, int value)
    {        
			Trybank tb = new Trybank();

			tb.Logged = true;
			tb.loggedUser = 0;
			tb.Bank[0,3] = balance;
			tb.Withdraw(value);

			tb.Bank[0,3].Should().Be(balance - value);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0)]
    public void TestWithdrawWithoutLogin(int value)
    {        
			Trybank tb = new Trybank();

			Action act = () => tb.Withdraw(value);

			act.Should().Throw<AccessViolationException>().WithMessage("Usuário já está logado");
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(10, 30)]
    public void TestWithdrawWithoutBalance(int balance, int value)
    {        
			Trybank tb = new Trybank();

			tb.Logged = true;
			tb.loggedUser = 0;
			tb.Bank[0,3] = balance;
			Action act = () => tb.Withdraw(value);

			act.Should().Throw<InvalidOperationException>().WithMessage("Saldo insuficiente");
    }
}

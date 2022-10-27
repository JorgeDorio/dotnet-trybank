using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestFirstReq
{
	[Theory(DisplayName = "Deve cadastrar contas com sucesso!")]
	[InlineData(1234, 001, 1234567)]
	public void TestRegisterAccountSucess(int number, int agency, int pass)
	{        
		Trybank trybank = new Trybank();

		trybank.RegisterAccount(number, agency,pass);

		trybank.Bank[0,0].Should().Be(number);
		trybank.Bank[0,1].Should().Be(agency);
		trybank.Bank[0,2].Should().Be(pass);
	}

	[Theory(DisplayName = "Deve retornar ArgumentException ao cadastrar contas que já existem")]
	[InlineData(1234, 001, 1234567)]
	public void TestRegisterAccountException(int number, int agency, int pass)
	{        
		Trybank trybank = new Trybank();

		trybank.RegisterAccount(number, agency,pass);

		Action act = () => trybank.RegisterAccount(number, agency,pass);

		act.Should().Throw<ArgumentException>().WithMessage("A conta já está sendo usada!");
	}
}

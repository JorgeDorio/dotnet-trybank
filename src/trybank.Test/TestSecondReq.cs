using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestSecondReq
{
    [Theory(DisplayName = "Deve logar em uma conta!")]
    [InlineData(1, 2, 3)]
    public void TestLoginSucess(int number, int agency, int pass)
    {        
			Trybank trybank = new Trybank();
			
			trybank.RegisterAccount(number, agency, pass);

			trybank.Login(number, agency, pass);

			trybank.Logged.Should().Be(true);
			trybank.loggedUser.Should().Be(0);
    }

    [Theory(DisplayName = "Deve retornar exceção ao tentar logar em conta já logada")]
    [InlineData(0, 0, 0)]
    public void TestLoginExceptionLogged(int number, int agency, int pass)
    {        
			Trybank trybank = new Trybank();

			trybank.Logged = true;

			Action act = () => trybank.Login(number, agency, pass);

			act.Should().Throw<AccessViolationException>().WithMessage("Usuário já está logado");
    }

    [Theory(DisplayName = "Deve retornar exceção ao errar a senha")]
    [InlineData(1, 2, 3)]
    public void TestLoginExceptionWrongPass(int number, int agency, int pass)
    {        
			Trybank trybank = new Trybank();

			trybank.RegisterAccount(number, agency, pass);

			Action act = () => trybank.Login(number, agency, pass + 1);

			act.Should().Throw<ArgumentException>().WithMessage("Senha incorreta");
    }

    [Theory(DisplayName = "Deve retornar exceção ao digitar conta que não existe")]
    [InlineData(0, 0, 0)]
    public void TestLoginExceptionNotFounded(int number, int agency, int pass)
    {        
			Trybank trybank = new Trybank();

			trybank.RegisterAccount(number, agency, pass);

			Action act = () => trybank.Login(number + 1, agency + 1, pass);

			act.Should().Throw<ArgumentException>().WithMessage("Agência + Conta não encontrada");
    }

    [Theory(DisplayName = "Deve sair de uma conta!")]
    [InlineData(0, 0, 0)]
    public void TestLogoutSucess(int number, int agency, int pass)
    {        
			Trybank trybank = new Trybank();

			trybank.Logged = true;

			trybank.Logout();

			trybank.Logged.Should().Be(false);
			trybank.loggedUser.Should().Be(-99);
    }

    [Theory(DisplayName = "Deve retornar exceção ao sair quando não está logado")]

    [InlineData(0, 0, 0)]
    public void TestLogoutExceptionNotLogged(int number, int agency, int pass)
    {        
			Trybank trybank = new Trybank();

			trybank.Logout();
			
			Action act = () => trybank.Logout();

			act.Should().Throw<AccessViolationException>().WithMessage("Usuário não está logado");
    }

}

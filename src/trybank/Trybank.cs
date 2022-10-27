namespace trybank;

public class Trybank
{
    public bool Logged;
    public int loggedUser;
    
    //0 -> Número da conta
    //1 -> Agência
    //2 -> Senha
    //3 -> Saldo
    public int[,] Bank;
    public int registeredAccounts;
    private int maxAccounts = 50;
    public Trybank()
    {
        loggedUser = -99;
        registeredAccounts = 0;
        Logged = false;
        Bank = new int[maxAccounts, 4];
    }

    public void RegisterAccount(int number, int agency, int pass)
    {
			for(int i = 0; i < registeredAccounts; i++)
			{
				if(Bank[i,0] == number && Bank[i,1] == agency)
				{
					throw new ArgumentException("A conta já está sendo usada!");
				};
			}

			Bank[registeredAccounts, 0] = number;
			Bank[registeredAccounts, 1] = agency;
			Bank[registeredAccounts, 2] = pass;

			registeredAccounts++;
    }

    public void Login(int number, int agency, int pass)
    {
			if(Logged) throw new AccessViolationException("Usuário já está logado");

			for(int i = 0; i < registeredAccounts; i++)
			{
				if(Bank[i,0] == number && Bank[i,1] == agency && Bank[i,2] == pass)
				{
					Logged = true;
					loggedUser = i;
				} else if (Bank[i,0] == number && Bank[i,1] == agency && Bank[i,2] != pass)
				{
					throw new ArgumentException("Senha incorreta");
				} else
				{
					throw new ArgumentException("Agência + Conta não encontrada");
				};
			}
    }

    public void Logout()
    {
			if(!Logged) throw new AccessViolationException("Usuário não está logado");
			
			Logged = false;
			loggedUser = -99;
    }

    public int CheckBalance()
    {
        throw new NotImplementedException();   
    }

    public void Transfer(int destinationNumber, int destinationAgency, int value)
    {
        throw new NotImplementedException();
    }

    public void Deposit(int value)
    {
        throw new NotImplementedException();
    }

    public void Withdraw(int value)
    {
        throw new NotImplementedException();
    }
}

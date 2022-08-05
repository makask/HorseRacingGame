using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRacing
{
    class Player
    {
        private int money;
        private string chosenHorse;
        Random random = new Random();
        private int bet;
        public bool active;
        private string status;
        private string plusMinus;
        private int transaction;
        private string name;

        public Player()
        {

        }

        public int getMoney()
        {
            return money;
        }

        public void setMoney(int money)
        {
            this.money = money;
        }

        public string chooseAIHorse(string[] horses, int index)
        {
            chosenHorse = horses[index];
            return chosenHorse;
        }

        public void setChosenHorse(string horse)
        {
            chosenHorse = horse;
        }

        public string getChosenHorse()
        {
            return chosenHorse;
        }

        public void setBet(int bet)
        {
            if (bet > money)
            {
                this.bet = money;
            }
            else
            {
                this.bet = bet;
            }

        }

        public int getBet()
        {
            return bet;
        }

        public void makeBet()
        {
            if (bet > money)
            {
                bet = money;
            }
            
            money -= bet;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName() {
            return name;
        }

        public void addMoney(int amount)
        {
            money += amount;
        }

        public bool isActive()
        {
            if (money <= 0)
            {
                this.active = false;
            }
            else
            {
                this.active = true;
            }
            return active;
        }

        public void setStatus(string status)
        {
            this.status = status;
        }

        public string getStatus()
        {
            return status;
        }

        public void setPlusMinus(string plusMinus)
        {
            this.plusMinus = plusMinus;
        }

        public string getPlusMinus()
        {
            return plusMinus;
        }

        public void setTransaction(int transaction)
        {
            this.transaction = transaction;
        }

        public int getTransaction()
        {
            return transaction;
        }
    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseRacing
{
    public partial class Form1 : Form
    {
        int horse1Left, horse2Left, horse3Left, horse4Left, horse5Left, horse6Left;
        int bank = 0;
        int gameState = 1;
        int minSpeed = 5;
        int maxSpeed = 20;
        int aiMinBet = 200;
        int aiMaxBet = 500;

        String res = "";
        List<String> result = new List<String>();
        string[] horses = { "Horse 1", "Horse 2", "Horse 3", "Horse 4", "Horse 5", "Horse 6" };
        Random random = new Random();


        Player humanPlayer = new Player();
        Player player1 = new Player();
        Player player2 = new Player();
        Player player3 = new Player();
        Player player4 = new Player();
        Player player5 = new Player();

        // Game Start
        public Form1()
        {
            InitializeComponent();
            btn_Sum.Enabled = false;
            button_Start.Enabled = false;
            button2.Enabled = false;
            btn_1.Enabled = false;
            btn_2.Enabled = false;
            btn_3.Enabled = false;
            btn_4.Enabled = false;
            btn_5.Enabled = false;
            btn_6.Enabled = false;

            humanPlayer.setMoney(1000);
            player1.setMoney(1000);
            player2.setMoney(1000);
            player3.setMoney(1000);
            player4.setMoney(1000);
            player5.setMoney(1000);

            humanPlayer.setStatus("Active");
            player1.setStatus("Active");
            player2.setStatus("Active");
            player3.setStatus("Active");
            player4.setStatus("Active");
            player5.setStatus("Active");

            humanPlayer.setName("Human Player");
            player1.setName("Crazy Charlie");
            player2.setName("Mad Maria");
            player3.setName("Positive Pete");
            player4.setName("Grumpy Georg");
            player5.setName("Sexy John");

            label14.Text = Convert.ToString(humanPlayer.getMoney());
            label17.Text = Convert.ToString(player1.getMoney());
            label22.Text = Convert.ToString(player2.getMoney());
            label27.Text = Convert.ToString(player3.getMoney());
            label32.Text = Convert.ToString(player4.getMoney());
            label37.Text = Convert.ToString(player5.getMoney());

            label15.Text = humanPlayer.getStatus();
            label16.Text = player1.getStatus();
            label21.Text = player2.getStatus();
            label26.Text = player3.getStatus();
            label31.Text = player4.getStatus();
            label36.Text = player5.getStatus();

            label4.Text = "Press button Choose Horse";

        }

        // Choose horse button
        private void button1_Click(object sender, EventArgs e)
        {
            // if gamestate -2 Game Over human and AIs dead, Draw
            // if gamestate -1 Game Over human player Dead
            // if gamestate 0 Game Won human alive all AIs dead 
            // if gamestate 1 Game Continues human alive some AIs alive
            //Disable button "Choose Horse"
            if (gameState == -2) {
                reset();
                button1.Text = "Choose horse";
            }
            if (gameState == -1) {
                reset();
                button1.Text = "Choose horse";
            }

            if (gameState == 0) {
                reset();
                button1.Text = "Choose horse";
            }

            
            button1.Enabled = false;

            //Enable horse buttons 1-6
            btn_1.Enabled = true;
            btn_2.Enabled = true;
            btn_3.Enabled = true;
            btn_4.Enabled = true;
            btn_5.Enabled = true;
            btn_6.Enabled = true;

            //Enable button Start race
            btn_Sum.Enabled = true;

            //Reset Horse Position
            pictureBox_horse1.Left = label10.Left - 77;
            pictureBox_horse2.Left = label10.Left - 77;
            pictureBox_horse3.Left = label10.Left - 77;
            pictureBox_horse4.Left = label10.Left - 77;
            pictureBox_horse5.Left = label10.Left - 77;
            pictureBox_horse6.Left = label10.Left - 77;
            label8.Text = "";

            label4.Text = "Choose horse 1-6 and place desired amount of money, then press Accept!";

            emptyFields();

            result.Clear();
        }

        // Button Accept
        private void btn_Sum_Click(object sender, EventArgs e)
        {
            // if gamestate -2 Game Over human and AIs dead, Draw
            // if gamestate -1 Game Over human player Dead
            // if gamestate 0 Game Won human alive all AIs dead 
            // if gamestate 1 Game Continues human alive some AIs alive 

            if (gameState == 1) {
                button1.Enabled = false;
                btn_Sum.Enabled = false;

                btn_1.Enabled = false;
                btn_2.Enabled = false;
                btn_3.Enabled = false;
                btn_4.Enabled = false;
                btn_5.Enabled = false;
                btn_6.Enabled = false;

                button_Start.Enabled = true;

                label4.Text = "Press button Start Race";

                if (humanPlayer.isActive()) {
                    try
                    {
                        humanPlayer.setBet(Convert.ToInt32(textBox_Sum.Text));
                    }
                    catch
                    {
                        MessageBox.Show("Enter number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        humanPlayer.setBet(0);
                        humanPlayer.setChosenHorse("");
                    }
                    label12.Text = Convert.ToString(humanPlayer.getBet());
                    humanPlayer.makeBet();
                    label14.Text = Convert.ToString(humanPlayer.getMoney());
                    btn_Sum.Enabled = false;
                    textBox_Sum.Text = "";
                    AIplayersHorseSelection();
                    AIplayersBetSelection();
                    showAIplayerSelections();
                    showAICredits();
                    collectBets();
                }
            }
             
        }

        // Start race button
        private void button_Start_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;
            timer4.Enabled = true;
            timer5.Enabled = true;
            timer6.Enabled = true;
            timer7.Enabled = true;
            button_Start.Enabled = false;
        }

        // Button to calculate race results
        private void button2_Click(object sender, EventArgs e)
        {
            // if gamestate -1 Game Over human player Dead
            // if gamestate 0 Game Won human alive all AIs dead 
            // if gamestate 1 Game Continues human alive some AIs alive
            button2.Enabled = false;
            button1.Enabled = true;

            getRaceResults();

            showTransaction();

            label41.Text = Convert.ToString(bank);

            gameState = gameStatus();

            if (gameState == -2)
            {
                label15.Text = Convert.ToString(humanPlayer.getStatus());
                label16.Text = Convert.ToString(player1.getStatus());
                label21.Text = Convert.ToString(player2.getStatus());
                label26.Text = Convert.ToString(player3.getStatus());
                label31.Text = Convert.ToString(player4.getStatus());
                label36.Text = Convert.ToString(player5.getStatus());
                label4.Text = "DRAW, all players eliminated! Play again ?";
                button1.Text = "Play again";
            }

            if (gameState == -1) {
                label15.Text = Convert.ToString(humanPlayer.getStatus());
                label4.Text = "LOST! All funds depleted! Play again ?";
                button1.Text = "Play again";
            }

            if (gameState == 0) {
                label15.Text = Convert.ToString(humanPlayer.getStatus());
                label16.Text = Convert.ToString(player1.getStatus());
                label21.Text = Convert.ToString(player2.getStatus());
                label26.Text = Convert.ToString(player3.getStatus());
                label31.Text = Convert.ToString(player4.getStatus());
                label36.Text = Convert.ToString(player5.getStatus());
                label4.Text = "WON! All opponents eliminated! Playe again ?";
                button1.Text = "Play again";
            }

            if (gameState == 1) {
                label15.Text = Convert.ToString(humanPlayer.getStatus());
                label16.Text = Convert.ToString(player1.getStatus());
                label21.Text = Convert.ToString(player2.getStatus());
                label26.Text = Convert.ToString(player3.getStatus());
                label31.Text = Convert.ToString(player4.getStatus());
                label36.Text = Convert.ToString(player5.getStatus());
            }

            label15.Text = humanPlayer.getStatus();
            label16.Text = player1.getStatus();
            label21.Text = player2.getStatus();
            label26.Text = player3.getStatus();
            label31.Text = player4.getStatus();
            label36.Text = player5.getStatus();

            label8.Text = printLeaderBoard();
           
        }


        private void Form1_Load(object sender, EventArgs e) {
            horse1Left = pictureBox_horse1.Left;
            horse2Left = pictureBox_horse2.Left;
            horse3Left = pictureBox_horse3.Left;
            horse4Left = pictureBox_horse4.Left;
            horse5Left = pictureBox_horse5.Left;
            horse6Left = pictureBox_horse6.Left;
        }

        private void AIplayersHorseSelection() {

            List<Player> aiPlayers = new List<Player>();
            aiPlayers.Add(player1);
            aiPlayers.Add(player2);
            aiPlayers.Add(player3);
            aiPlayers.Add(player4);
            aiPlayers.Add(player5);

            foreach (Player player in aiPlayers) {
                if (player.isActive())
                {
                    player.setChosenHorse(player.chooseAIHorse(horses, random.Next(0, 6)));
                }
                else {
                    player.setChosenHorse("");
                }
            }
            
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void AIplayersBetSelection() {
            List<Player> aiPlayers = new List<Player>();
            aiPlayers.Add(player1);
            aiPlayers.Add(player2);
            aiPlayers.Add(player3);
            aiPlayers.Add(player4);
            aiPlayers.Add(player5);

            foreach (Player player in aiPlayers)
            {
                if (player.isActive())
                {
                    player.setBet(random.Next(aiMinBet, aiMaxBet));
                    player.makeBet();
                }
                else
                {
                    player.setBet(0);
                }
            }

        }

        private void showAIplayerSelections() {

            label19.Text = Convert.ToString(player1.getBet());
            label24.Text = Convert.ToString(player2.getBet());
            label29.Text = Convert.ToString(player3.getBet());
            label34.Text = Convert.ToString(player4.getBet());
            label39.Text = Convert.ToString(player5.getBet());

            label20.Text = player1.getChosenHorse();
            label25.Text = player2.getChosenHorse();
            label30.Text = player3.getChosenHorse();
            label35.Text = player4.getChosenHorse();
            label40.Text = player5.getChosenHorse();
        }



        private void showAICredits() {
            label17.Text = Convert.ToString(player1.getMoney());
            label22.Text = Convert.ToString(player2.getMoney());
            label27.Text = Convert.ToString(player3.getMoney());
            label32.Text = Convert.ToString(player4.getMoney());
            label37.Text = Convert.ToString(player5.getMoney());
        }

        private void collectBets() {

            List<Player> players = new List<Player>();
            players.Add(humanPlayer);
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);
            players.Add(player5);

            foreach (Player player in players) {
                if (player.isActive()) {
                    bank += player.getBet();
                }
            }

            label41.Text = Convert.ToString(bank);
            players.Clear();
        }

        private void getRaceResults() {

            Player[] players = { humanPlayer, player1, player2, player3, player4, player5 };
            String[] resultArray = result.ToArray();

            string firstPosition = resultArray[0];
            string secondPosition = resultArray[1];
            string thirdPosition = resultArray[2];

            int firstCounter = 0;
            int secondCounter = 0;
            int thirdCounter = 0;

            foreach (Player player in players) {
                if (player.getChosenHorse() == firstPosition)
                {
                    firstCounter++;

                }
                else if (player.getChosenHorse() == secondPosition)
                {
                    secondCounter++;
                }
                else if (player.getChosenHorse() == thirdPosition) {
                    thirdCounter++;
                }
            }

            int firstPrizeTotal = (int)(bank * 0.5);
            int secondPrizeTotal = (int)(bank * 0.3);
            int thirdPrizeTotal = (int)(bank * 0.2);

            int firstPrize = 0;
            int secondPrize = 0;
            int thirdPrize = 0;

            if (firstCounter > 0)
            {
                firstPrize = (int)(firstPrizeTotal / firstCounter);
                bank -= firstPrizeTotal;
            }

            if (secondCounter > 0) {
                secondPrize = (int)(secondPrizeTotal / secondCounter);
                bank -= secondPrizeTotal;
            }

            if (thirdCounter > 0) {
                thirdPrize = (int)(thirdPrizeTotal / thirdCounter);
                bank -= thirdPrizeTotal;
            }


            foreach (Player player in players) {
                if (player.getChosenHorse() == firstPosition)
                {
                    player.setPlusMinus("+");
                    player.setTransaction(firstPrize);
                    player.addMoney(firstPrize);

                }
                else if (player.getChosenHorse() == secondPosition)
                {
                    player.setPlusMinus("+");
                    player.setTransaction(secondPrize);
                    player.addMoney(secondPrize);
                }
                else if (player.getChosenHorse() == thirdPosition)
                {
                    player.setPlusMinus("+");
                    player.setTransaction(thirdPrize);
                    player.addMoney(thirdPrize);
                }
                else {
                    player.setPlusMinus("-");
                    player.setTransaction(player.getBet());
                }

            }

        }

        private void printWinners() {

            Player[] players = { humanPlayer, player1, player2, player3, player4, player5 };
            String[] resultArray = result.ToArray();

            string firstPosition = resultArray[0];
            string secondPosition = resultArray[1];
            string thirdPosition = resultArray[2];

            label4.Text = "1st place: " + firstPosition +
                "\n" + "2nd place: " + secondPosition +
                "\n" + "3rd place: " + thirdPosition;
        }


        private void emptyFields() {
            // Set labels for chosen horse empty
            label11.Text = "";
            label20.Text = "";
            label25.Text = "";
            label30.Text = "";
            label35.Text = "";
            label40.Text = "";

            // Set labels for chose sum empty
            label12.Text = "";
            label19.Text = "";
            label24.Text = "";
            label29.Text = "";
            label34.Text = "";
            label39.Text = "";

            // Set label for +/- money empty
            label13.Text = "";
            label18.Text = "";
            label23.Text = "";
            label28.Text = "";
            label33.Text = "";
            label38.Text = "";
        }

        private void showTransaction() {
            label13.Text = humanPlayer.getPlusMinus() + " " + humanPlayer.getTransaction();
            label18.Text = player1.getPlusMinus() + " " + player1.getTransaction();
            label23.Text = player2.getPlusMinus() + " " + player2.getTransaction();
            label28.Text = player3.getPlusMinus() + " " + player3.getTransaction();
            label33.Text = player4.getPlusMinus() + " " + player4.getTransaction();
            label38.Text = player5.getPlusMinus() + " " + player5.getTransaction();

            // Show players money
            label14.Text = Convert.ToString(humanPlayer.getMoney());
            label17.Text = Convert.ToString(player1.getMoney());
            label22.Text = Convert.ToString(player2.getMoney());
            label27.Text = Convert.ToString(player3.getMoney());
            label32.Text = Convert.ToString(player4.getMoney());
            label37.Text = Convert.ToString(player5.getMoney());
        }

        private int gameStatus() {
            List<Player> players = new List<Player>();
            players.Add(humanPlayer);
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);
            players.Add(player5);

            int result = 0;
            // Draw , all players dead, Game Over
            if (isEveryOneDead()) {
                setStatus();
                result = -2;
            }
            // Game Over
            if (!isHumanActive() && (!isAllAIsDead() || areSomeAIsAlive())) {
                setStatus();
                result = -1;
            }
            // Game Won
            if (isHumanActiveAndAIsDead()) {
                setStatus();
                result = 0;
            }
            // Game Continues
            if (isHumanActive() && (areSomeAIsAlive() || !isAllAIsDead())) {
                setStatus();
                result = 1;
            } 
            return result;
        }

        // Is human player alive + 
        private bool isHumanActive() {
            return humanPlayer.isActive();
        }

        private bool isEveryOneDead() {
            List<Player> players = new List<Player>();
            players.Add(humanPlayer);
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);
            players.Add(player5);
            int counter = 0;
            foreach (Player player in players) {
                if (player.isActive() == false) {
                    counter++;
                }
            }
            bool result = false;
            if (counter == 6)
            {
                result = true;
            }
            return result;
        }

        // Is all AIs dead
        private bool isAllAIsDead() {
            List<Player> aiPlayers = new List<Player>();
            int counter = 0;
            aiPlayers.Add(player1);
            aiPlayers.Add(player2);
            aiPlayers.Add(player3);
            aiPlayers.Add(player4);
            aiPlayers.Add(player5);

            foreach (Player player in aiPlayers)
            {
                if (!player.isActive())
                {
                    counter++;
                }
            }

            if (counter == 5)
            {
                return true;
            }
            else {
                return false;
            }

        }

        // Is human player alive and all AI players dead Game Won +
        private bool isHumanActiveAndAIsDead() {

            int counter = 0;
            List<Player> aiPlayers = new List<Player>();
            aiPlayers.Add(player1);
            aiPlayers.Add(player2);
            aiPlayers.Add(player3);
            aiPlayers.Add(player4);
            aiPlayers.Add(player5);

            foreach (Player player in aiPlayers) {
                if (!player.isActive()) {
                    counter++;
                }
            }

            aiPlayers.Clear();

            if (humanPlayer.isActive() && counter == 5)
            {
                return true;
            }
            else {
                return false;
            }
        }
        // Check if human is active and at least some AIs Active = Game continues +
        private bool isHumanActiveAndSomeAIsAlive() {
            int counter = 0;
            List<Player> aiPlayers = new List<Player>();
            aiPlayers.Add(player1);
            aiPlayers.Add(player2);
            aiPlayers.Add(player3);
            aiPlayers.Add(player4);
            aiPlayers.Add(player5);

            foreach (Player player in aiPlayers)
            {
                if (player.isActive())
                {
                    counter++;
                }
                
            }

            aiPlayers.Clear();

            if (humanPlayer.isActive() && counter > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool areSomeAIsAlive() {
            List<Player> aiPlayers = new List<Player>();
            aiPlayers.Add(player1);
            aiPlayers.Add(player2);
            aiPlayers.Add(player3);
            aiPlayers.Add(player4);
            aiPlayers.Add(player5);

            int counter = 0;

            foreach (Player player in aiPlayers)
            {
                if (player.isActive())
                {
                    counter++;
                }
            }
            if (counter > 0)
            {
                return true;
            }
            else {
                return false;
            }

        }

        private void setStatus() {
            List<Player> players = new List<Player>();
            players.Add(humanPlayer);
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);
            players.Add(player5);

            foreach (Player player in players) {
                if (player.isActive())
                {
                    player.setStatus("Active");
                }
                else {
                    player.setStatus("Eliminated");
                }
            }
        }
        
        private void reset() {
            btn_Sum.Enabled = false;
            button_Start.Enabled = false;
            button2.Enabled = false;
            btn_1.Enabled = false;
            btn_2.Enabled = false;
            btn_3.Enabled = false;
            btn_4.Enabled = false;
            btn_5.Enabled = false;
            btn_6.Enabled = false;

            humanPlayer.setMoney(1000);
            player1.setMoney(1000);
            player2.setMoney(1000);
            player3.setMoney(1000);
            player4.setMoney(1000);
            player5.setMoney(1000);

            humanPlayer.setStatus("Active");
            player1.setStatus("Active");
            player2.setStatus("Active");
            player3.setStatus("Active");
            player4.setStatus("Active");
            player5.setStatus("Active");

            humanPlayer.setName("Human Player");
            player1.setName("Crazy Charlie");
            player2.setName("Mad Maria");
            player3.setName("Positive Pete");
            player4.setName("Grumpy Georg");
            player5.setName("Sexy John");

            label14.Text = Convert.ToString(humanPlayer.getMoney());
            label17.Text = Convert.ToString(player1.getMoney());
            label22.Text = Convert.ToString(player2.getMoney());
            label27.Text = Convert.ToString(player3.getMoney());
            label32.Text = Convert.ToString(player4.getMoney());
            label37.Text = Convert.ToString(player5.getMoney());

            label15.Text = humanPlayer.getStatus();
            label16.Text = player1.getStatus();
            label21.Text = player2.getStatus();
            label26.Text = player3.getStatus();
            label31.Text = player4.getStatus();
            label36.Text = player5.getStatus();

            label4.Text = "Press button Choose Horse";
            bank = 0;
            label41.Text = Convert.ToString(bank);
            gameState = 1;
        }

    /*********************Timers*****************************/
        private void timer1_Tick(object sender, EventArgs e)
        {
            int widthHorse1 = pictureBox_horse1.Width;

            int finish = lbl_Finish.Left;

            pictureBox_horse1.Left += random.Next(minSpeed, maxSpeed);

            if (pictureBox_horse1.Left > pictureBox_horse2.Left + 5
                && pictureBox_horse1.Left > pictureBox_horse3.Left + 5
                && pictureBox_horse1.Left > pictureBox_horse4.Left + 5
                && pictureBox_horse1.Left > pictureBox_horse5.Left + 5
                && pictureBox_horse1.Left > pictureBox_horse6.Left + 5)
            {
                label4.Text = "Horse number 1 takes the lead!";

            }


            if (widthHorse1 + pictureBox_horse1.Left >= finish)
            {
                result.Add("Horse 1");
                timer1.Enabled = false;
            }

        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            int widthHorse2 = pictureBox_horse2.Width;
            int finish = lbl_Finish.Left;
            pictureBox_horse2.Left += random.Next(minSpeed, maxSpeed);

            if (pictureBox_horse2.Left > pictureBox_horse1.Left + 5
                && pictureBox_horse2.Left > pictureBox_horse3.Left + 5
                && pictureBox_horse2.Left > pictureBox_horse4.Left + 5
                && pictureBox_horse2.Left > pictureBox_horse5.Left + 5
                && pictureBox_horse2.Left > pictureBox_horse6.Left + 5)
            {
                label4.Text = "Horse number 2 takes the lead!";

            }

            if (widthHorse2 + pictureBox_horse2.Left >= finish)
            {
                result.Add("Horse 2");
                timer2.Enabled = false;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            int widthHorse3 = pictureBox_horse3.Width;
            int finish = lbl_Finish.Left;
            pictureBox_horse3.Left += random.Next(minSpeed, maxSpeed);

            if (pictureBox_horse3.Left > pictureBox_horse1.Left + 5
                && pictureBox_horse3.Left > pictureBox_horse2.Left + 5
                && pictureBox_horse3.Left > pictureBox_horse4.Left + 5
                && pictureBox_horse3.Left > pictureBox_horse5.Left + 5
                && pictureBox_horse3.Left > pictureBox_horse6.Left + 5)
            {
                label4.Text = "Horse number 3 takes the lead!";

            }

            if (widthHorse3 + pictureBox_horse3.Left >= finish)
            {               
                result.Add("Horse 3");
                res += "H3 ";
                timer3.Enabled = false;
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            int widthHorse4 = pictureBox_horse4.Width;
            int finish = lbl_Finish.Left;
            pictureBox_horse4.Left += random.Next(minSpeed, maxSpeed);

            if (pictureBox_horse4.Left > pictureBox_horse1.Left + 5
                && pictureBox_horse4.Left > pictureBox_horse2.Left + 5
                && pictureBox_horse4.Left > pictureBox_horse3.Left + 5
                && pictureBox_horse4.Left > pictureBox_horse5.Left + 5
                && pictureBox_horse4.Left > pictureBox_horse6.Left + 5)
            {
                label4.Text = "Horse number 4 takes the lead!";

            }

            if (widthHorse4 + pictureBox_horse4.Left >= finish)
            {               
                result.Add("Horse 4");                         
                timer4.Enabled = false;
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            int widthHorse5 = pictureBox_horse5.Width;
            int finish = lbl_Finish.Left;
            pictureBox_horse5.Left += random.Next(minSpeed, maxSpeed);

            if (pictureBox_horse5.Left > pictureBox_horse1.Left + 5
                && pictureBox_horse5.Left > pictureBox_horse2.Left + 5
                && pictureBox_horse5.Left > pictureBox_horse3.Left + 5
                && pictureBox_horse5.Left > pictureBox_horse4.Left + 5
                && pictureBox_horse5.Left > pictureBox_horse6.Left + 5)
            {
                label4.Text = "Horse number 5 takes the lead!";
            }

            if (widthHorse5 + pictureBox_horse5.Left >= finish)
            {                
                result.Add("Horse 5");                            
                timer5.Enabled = false;
            }
        }


        private void timer6_Tick(object sender, EventArgs e)
        {
            int widthHorse6 = pictureBox_horse6.Width;
            int finish = lbl_Finish.Left;
            pictureBox_horse6.Left += random.Next(minSpeed, maxSpeed);

            if (pictureBox_horse6.Left > pictureBox_horse1.Left + 5
                && pictureBox_horse6.Left > pictureBox_horse2.Left + 5
                && pictureBox_horse6.Left > pictureBox_horse3.Left + 5
                && pictureBox_horse6.Left > pictureBox_horse4.Left + 5
                && pictureBox_horse6.Left > pictureBox_horse5.Left + 5)
            {
                label4.Text = "Horse number 6 takes the lead!";

            }

            if (widthHorse6 + pictureBox_horse6.Left >= finish)
            {                
                result.Add("Horse 6");
                res += "H6 ";                
                timer6.Enabled = false;
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            label8.Text = String.Join("\n", result);
            
            lbl_Horse1.Text = Convert.ToString(result.IndexOf("Horse 1") + 1);
            lbl_Horse2.Text = Convert.ToString(result.IndexOf("Horse 2") + 1);
            lbl_Horse3.Text = Convert.ToString(result.IndexOf("Horse 3") + 1);
            lbl_Horse4.Text = Convert.ToString(result.IndexOf("Horse 4") + 1);
            lbl_Horse5.Text = Convert.ToString(result.IndexOf("Horse 5") + 1);
            lbl_Horse6.Text = Convert.ToString(result.IndexOf("Horse 6") + 1);

            if (result.Count == 6)
            {
                button2.Enabled = true;
                timer7.Enabled = false;
                printWinners();

            }
        }
        
        private string printLeaderBoard() {

            string result = "";

            Player[] players = { humanPlayer, player1, player3, player4, player5 };

            Array.Sort(players, (y,x) => x.getMoney().CompareTo(y.getMoney()));
            
            foreach (Player player in players) {
                result += "\n " + player.getName() + " " + player.getMoney();
            }

            return result;

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            humanPlayer.setChosenHorse("Horse 1");
            label11.Text = humanPlayer.getChosenHorse();
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            humanPlayer.setChosenHorse("Horse 2");
            label11.Text = humanPlayer.getChosenHorse();
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            humanPlayer.setChosenHorse("Horse 3");
            label11.Text = humanPlayer.getChosenHorse();
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            humanPlayer.setChosenHorse("Horse 4");
            label11.Text = humanPlayer.getChosenHorse();
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            humanPlayer.setChosenHorse("Horse 5");
            label11.Text = humanPlayer.getChosenHorse();
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            humanPlayer.setChosenHorse("Horse 6");
            label11.Text = humanPlayer.getChosenHorse();
        }
    }

    
}

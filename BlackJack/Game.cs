using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackJack
{
    class Game
    {
        private Random rGen = new Random();
        private int intIndexOfCardOnTop = 0;
        private List<string[]> playerHand = new List<string[]>();
        private List<string[]> computerHand = new List<string[]>();
        private string[][] strCards = new string[][] {
            new string[] { "Ace of Spades", "11" },
            new string[] { "Two of Spades", "2" },
            new string[] { "Three of Spades", "3" },
            new string[] { "Four of Spades", "4" },
            new string[] { "Five of Spades", "5" },
            new string[] { "Six of Spades", "6" },
            new string[] { "Seven of Spades", "7" },
            new string[] { "Eight of Spades", "8" },
            new string[] { "Nine of Spades", "9" },
            new string[] { "Ten of Spades", "10" },
            new string[] { "Jack of Spades", "10" },
            new string[] { "Queen of Spades", "10" },
            new string[] { "King of Spades", "10" },

            new string[] { "Ace of Hearts", "11" },
            new string[] { "Two of Hearts", "2" },
            new string[] { "Three of Hearts", "3" },
            new string[] { "Four of Hearts", "4" },
            new string[] { "Five of Hearts", "5" },
            new string[] { "Six of Hearts", "6" },
            new string[] { "Seven of Hearts", "7" },
            new string[] { "Eight of Hearts", "8" },
            new string[] { "Nine of Hearts", "9" },
            new string[] { "Ten of Hearts", "10" },
            new string[] { "Jack of Hearts", "10" },
            new string[] { "Queen of Hearts", "10" },
            new string[] { "King of Hearts", "10" },

            new string[] { "Ace of Clubs", "11" },
            new string[] { "Two of Clubs", "2" },
            new string[] { "Three of Clubs", "3" },
            new string[] { "Four of Clubs", "4" },
            new string[] { "Five of Clubs", "5" },
            new string[] { "Six of Clubs", "6" },
            new string[] { "Seven of Clubs", "7" },
            new string[] { "Eight of Clubs", "8" },
            new string[] { "Nine of Clubs", "9" },
            new string[] { "Ten of Clubs", "10" },
            new string[] { "Jack of Clubs", "10" },
            new string[] { "Queen of Clubs", "10" },
            new string[] { "King of Clubs", "10" },

            new string[] { "Ace of Diamonds", "11" },
            new string[] { "Two of Diamonds", "2" },
            new string[] { "Three of Diamonds", "3" },
            new string[] { "Four of Diamonds", "4" },
            new string[] { "Five of Diamonds", "5" },
            new string[] { "Six of Diamonds", "6" },
            new string[] { "Seven of Diamonds", "7" },
            new string[] { "Eight of Diamonds", "8" },
            new string[] { "Nine of Diamonds", "9" },
            new string[] { "Ten of Diamonds", "10" },
            new string[] { "Jack of Diamonds", "10" },
            new string[] { "Queen of Diamonds", "10" },
            new string[] { "King of Diamonds", "10" }
        };

        public void Startup()
        {
            Console.WriteLine("Welcome to Black Jack!");
            Shuffle();
            PlayRound();
        }

        private void PlayRound()
        {
            playerHand = new List<string[]>();
            computerHand = new List<string[]>();
            if (intIndexOfCardOnTop > 42)
            {
                Shuffle();
            }

            Console.WriteLine("Now let me draw cards for both of us");

            for (int i = 0; i < 2; i++)
            {
                playerHand.Add(DrawCard());
                computerHand.Add(DrawCard());
            }
            Console.WriteLine($"Your cards are: {playerHand[0][0]} and {playerHand[1][0]}");
            Console.WriteLine($"Your dealer's cards are: {computerHand[0][0]} and a face down card");
            if (CheckHandForWin(playerHand))
            {
                if (CheckHandForWin(computerHand))
                {
                    Console.WriteLine("I'm sorry, but both you and the dealer had a Black Jack, so it's a tie");
                }
                else
                {
                    Console.WriteLine("Congratulations!! You won with a Black Jack");
                }
            }
            else
            {
                if (CheckHandForWin(computerHand))
                {
                    Console.WriteLine("I'm sorry, but the dealer won with a Black Jack!");
                }
                else
                {
                    bool blnUserWon = false;
                    bool blnUserStood = false;
                    bool blnUserBusted = false;
                    int intHitCount = 0;

                    while (!blnUserWon && !blnUserStood && !blnUserBusted)
                    {
                        Console.WriteLine("Do you hit or stand?");
                        string strAction = Console.ReadLine();
                        switch (strAction.Trim().ToLower())
                        {
                            case "hit":
                                if (intHitCount == 3)
                                {
                                    Console.WriteLine("I'm sorry, but you have exceeded the maximum hit amount!");
                                }
                                else
                                {
                                    intHitCount++;
                                    playerHand.Add(DrawCard());
                                    Console.WriteLine($"You just drew a {playerHand[playerHand.Count - 1][0]}");
                                    if (CheckHandForWin(playerHand))
                                    {
                                        Console.WriteLine("Congratulations, you have won the game!!");
                                        blnUserWon = true;
                                    }
                                    if (CheckHandForBust(playerHand))
                                    {
                                        blnUserBusted = true;
                                        Console.WriteLine("I'm sorry, but your hand is busted, let me check the dealer's hand and get back to you");
                                    }
                                }
                                break;
                            case "stand":
                                blnUserStood = true;
                                Console.WriteLine("A wise move, hopefully. Let me check the dealer's hand and get back to you");
                                break;
                            default:
                                Console.WriteLine("Please enter a correct action from below:");
                                break;
                        }
                    }

                    if (blnUserBusted || blnUserStood)
                    {
                        Console.WriteLine($"The dealer's face down card is a {computerHand[1][0]}");
                        while (GetHandValue(computerHand) < 17)
                        {
                            computerHand.Add(DrawCard());
                            Console.WriteLine($"The dealer just drew a {computerHand[computerHand.Count - 1][0]}");
                        }
                        bool blnComputerBusted = CheckHandForBust(computerHand);
                        if (blnComputerBusted && blnUserBusted)
                        {
                            Console.WriteLine("I'm sorry, but both you and the dealer has busted, so it's a tie!");
                        }
                        else if (CheckHandForWin(computerHand))
                        {
                            Console.WriteLine("I'm sorry, but the dealer got a hand value of 21 and you lost!");
                        }
                        else if (blnUserBusted)
                        {
                            Console.WriteLine("I'm sorry, but you have busted and the dealer hasn't, so the dealer wins!");
                        }
                        else if (blnComputerBusted)
                        {
                            Console.WriteLine("Congratulations, the computer has busted and you haven't, so you win!");
                        }
                        else
                        {
                            Console.WriteLine("Both you and the dealer hasn't won or busted");
                            int intPlayerHandValue = GetHandValue(playerHand);
                            int intComputerHandValue = GetHandValue(computerHand);
                            if (intPlayerHandValue == intComputerHandValue)
                            {
                                Console.WriteLine("AND you both have the same hand value, so it's a tie!!");
                            }
                            else if (intPlayerHandValue > intComputerHandValue)
                            {
                                Console.WriteLine("BUT you were closer to 21 than the dealer, so you win!!");
                            }
                            else
                            {
                                Console.WriteLine("BUT the dealer was closer to 21, so you lost!!");
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Do you want to play again?");
            string strInput = Console.ReadLine();
            if (!String.IsNullOrEmpty(strInput.Trim()) && strInput.Trim().ToLower()[0] == 'y')
            {
                PlayRound();
            }
        }

        private void Shuffle()
        {
            Console.WriteLine("Please wait a second until we shuffle the deck for you!");
            List<string[]> cardList = strCards.ToList();
            int cardLength = strCards.Length;
            for (int i = 0; i < cardLength; i++)
            {
                int cardIndex = rGen.Next(cardList.Count);
                strCards[i] = cardList[cardIndex];
                cardList.RemoveAt(cardIndex);
            }
            intIndexOfCardOnTop = 0;
            Thread.Sleep(1000);
            Console.WriteLine("Done!");
        }

        private string[] DrawCard()
        {
            return strCards[intIndexOfCardOnTop++];
        }

        private int GetHandValue(List<string[]> hand)
        {
            int intNumOfAces = 0;
            int intTotal = 0;
            foreach (string[] card in hand)
            {
                int cardValue = Convert.ToInt32(card[1]);
                if (cardValue == 11)
                {
                    intNumOfAces++;
                    continue;
                }
                intTotal += cardValue;
            }
            if (intTotal <= 10 && intNumOfAces >= 1)
            {
                intTotal += 11;
                intNumOfAces--;
            }

            intTotal += intNumOfAces;
            return intTotal;
        }

        private bool CheckHandForWin(List<string[]> hand)
        {
            return GetHandValue(hand) == 21;
        }
        private bool CheckHandForBust(List<string[]> hand)
        {
            return GetHandValue(hand) > 21;
        }
    }
}

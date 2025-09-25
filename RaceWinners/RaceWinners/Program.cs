using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RaceWinners;

public class Program
{
    static async Task Main(string[] args)
    {
        DataService ds = new DataService();

        // Asynchronously retrieve the group (class) data
        var data = await ds.GetGroupRanksAsync();

        for (int i = 0; i < data.Count; i++)
        {
            // Combine the ranks to print as a list
            var ranks = String.Join(", ", data[i].Ranks);

            Console.WriteLine($"{data[i].Name} - [{ranks}]");
        }
        int OAScore = 0;
        int OBScore = 0;
        int OCScore = 0;
        int ODScore = 0;

 

        foreach (var groups in data) // goes through each group
        {
            foreach (var ranks in groups.Ranks) // finds the specific rank in the group and if it is less than 20 it adds to the overall score of the class
            {
                if (ranks <= 20) // only considers the top 20 ranks
                {
                    int scoreToAdd = 21 - ranks; //every rank is higher than 0, so 21-1 = 20 points, which means rank number one will get 20 points and the worse the rank the less the score
                    // Adds the score to the appropriate class based on the group name

                    switch (groups.Name) // 'switch' is a control flow statement that executes code blocks based on the value of an expression (here groups.Name)
                    {
                        case "Class A":
                            OAScore += scoreToAdd;
                            break;
                        case "Class B":
                            OBScore += scoreToAdd;
                            break;
                        case "Class C":
                            OCScore += scoreToAdd;
                            break;
                        case "Class D":
                            ODScore += scoreToAdd;
                            break;
                    }
                }
            }
        }


        Console.WriteLine($"\nOverall Scores:\nClass A: {OAScore}\nClass B: {OBScore}\nClass C: {OCScore}\nClass D: {ODScore}");

        int[] scores = { OAScore, OBScore, OCScore, ODScore };
        String[] classes = { "Class A", "Class B", "Class C", "Class D" };

        int winnerScore = scores[0];
        String winningClass = classes[0];

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] > winnerScore)
            {
                winnerScore = scores[i];
                winningClass = classes[i];
            }
        }
        
        //Determine first, second, third and fourth place
        for (int i = 0; i < scores.Length - 1; i++)
        {
            for (int j = i + 1; j < scores.Length; j++)
            {
                if (scores[i] < scores[j])
                {
                    // Swap scores
                    int tempScore = scores[i];
                    scores[i] = scores[j];
                    scores[j] = tempScore;
                    // Swap corresponding classes
                    string tempClass = classes[i];
                    classes[i] = classes[j];
                    classes[j] = tempClass;
                }
            }
        }


        Console.WriteLine($"\n{winningClass} won with {winnerScore} points!!!");
        //print who came in first, second, third and fourth place
        Console.WriteLine($"\nFinal Standings:\n1st Place: {classes[0]} with {scores[0]} points\n2nd Place: {classes[1]} with {scores[1]} points\n3rd Place: {classes[2]} with {scores[2]} points\n4th Place: {classes[3]} with {scores[3]} points");

    }
}

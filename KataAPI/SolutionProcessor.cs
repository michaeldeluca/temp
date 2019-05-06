using System;
using System.IO;
using KataAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KataAPI
{
    public class SolutionProcessor
    {
        public void CreateSolution(Calculation calculation)
        {
            using (StreamWriter file = File.CreateText(@"solution.json"))
            {
                JsonSerializer serializer = new JsonSerializer {Formatting = Formatting.Indented};
                serializer.Serialize(file, calculation);
            }
        }

       

        public string SolutionIsCorrect(string json)
        {
            var results = JsonConvert.DeserializeObject<Solution>(json);
            //Console.WriteLine(inputCalculation);
            return $"solution:\n{results.SolutionNumber}";
        }
    }

    public class Solution
    {
        public string Id { get; set; }
        public string SolutionNumber { get; set; }
    }
}
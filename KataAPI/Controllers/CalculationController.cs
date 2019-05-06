using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KataAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        [HttpGet]
        public IActionResult Addition(string numberOne, string numberTwo)
        {
            if (!int.TryParse(numberOne, out _) || !int.TryParse(numberTwo, out _))
                return BadRequest("Please ensure to enter a valid integer!");

            var calculator = new Calculation(Convert.ToInt32(numberOne), Convert.ToInt32(numberTwo), "+");
            return Ok($"{calculator.Answer}");
        }

        [HttpGet]
        public IActionResult Subtraction(string numberOne, string numberTwo)
        {
            if (!int.TryParse(numberOne, out _) || !int.TryParse(numberTwo, out _))
                return BadRequest("Please ensure to enter a valid integer!");

            var calculator = new Calculation(Convert.ToInt32(numberOne), Convert.ToInt32(numberTwo), "-");
            return Ok($"{calculator.Answer}");
        }

        [HttpGet]
        public IActionResult Division(string numberOne, string numberTwo)
        {
            if (!int.TryParse(numberOne, out _) || !int.TryParse(numberTwo, out _))
                return BadRequest("Please ensure to enter a valid integer!");

            var calculator = new Calculation(Convert.ToInt32(numberOne), Convert.ToInt32(numberTwo), "/");
            return Ok(calculator.Answer);
        }

        [HttpGet]
        public IActionResult Multiplication(string numberOne, string numberTwo)
        {
            if (!int.TryParse(numberOne, out _) || !int.TryParse(numberTwo, out _))
                return BadRequest("Please ensure to enter a valid integer!");

            var calculator = new Calculation(Convert.ToInt32(numberOne), Convert.ToInt32(numberTwo), "*");
            return Ok(calculator.Answer);
        }

        [HttpGet]
        public IActionResult EchoNumber(string number)
        {
            if (number == null || !int.TryParse(number, out _))
                return Ok("Please ensure to enter a valid integer!");

            return Ok($"{number}");
        }

        [HttpGet]
        public IActionResult GetCalculationProblem()
        {
            var ops = new[] {"+", "-", "*", "/"};
            var randOne = new Random();
            var randTwo = new Random();
            var randOp = new Random();
            var numOne = randOne.Next(0, 46340);
            var numTwo = randTwo.Next(0, 46340);
            var op = randOp.Next(0, 3);

            var calculator = new Calculation(numOne, numTwo, ops[op]);
            var solutionProcessor = new SolutionProcessor();
            solutionProcessor.CreateSolution(calculator);
            return Ok($"{calculator.Guid}\n{calculator.NumberOne} {calculator.Op} {calculator.NumberTwo}");
        }

        [HttpPost]
        public IActionResult SolveCalculation([FromBody] string inputJson)
        {
            var results = JsonConvert.DeserializeObject<string>(inputJson);
           return Ok($"{results}");
          

        }
    }

    public class Calculation
    {
        public Calculation(int numOne, int numTwo, string op)
        {
            NumberOne = numOne;
            NumberTwo = numTwo;
            Op = op;

            Guid = Guid.NewGuid();
            Answer = Calculate().ToString();
        }

        [JsonProperty(PropertyName = "Id")]
        public Guid Guid { get; }
        [JsonProperty(PropertyName = "SolutionNumber")]
        public string Answer { get; }
        [JsonIgnore]
        public int NumberOne { get; }
        [JsonIgnore]
        public int NumberTwo { get; }
        [JsonIgnore]
        public string Op { get; }

        private int? Calculate()
        {
            switch (Op)
            {
                case "+":
                    return NumberOne + NumberTwo;
                case "-":
                    return NumberOne - NumberTwo;
                case "/":
                    return NumberOne / NumberTwo;
                case "*":
                    return NumberOne * NumberTwo;
                default: return null;
            }
        }
    }
}
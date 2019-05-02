using System;
using Microsoft.AspNetCore.Mvc;

namespace KataAPI.Controllers
{
    public class Calculation
    {
        public int? Answer { get; }

        public Calculation(string valueOne, string valueTwo, string op)
        {
            if (
                (valueOne == null || valueTwo == null) ||
                (!int.TryParse(valueOne, out var itemOne) ||
                 !int.TryParse(valueTwo, out var itemTwo))
            )
                Answer = null;
            
            else Answer = Calculate(Convert.ToInt32(valueOne), op, Convert.ToInt32(valueTwo));
        }

        private int? Calculate(int numOne, string op, int numTwo)
        {
            switch (op)
            {
                case "+":
                    return numOne + numTwo;
                case "-":
                    return numOne - numTwo;
                case "/":
                    return numOne / numTwo;
                case "*":
                    return numOne * numTwo;
                default: return null;
            }
        }

    }

    [Route("api/[controller]/addition")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string valueOne, string valueTwo)
        {

            var calculator = new Calculation(valueOne, "+", valueTwo);

            if (calculator.Answer==null)

                return BadRequest("Please ensure to enter a valid integer!");
            
            return Ok($"{calculator.Answer}");
        }
    }
}
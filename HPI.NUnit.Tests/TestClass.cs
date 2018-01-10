using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPI.BusinessEntities;
using HPI.ValidationRuleEngine;
namespace HPI.NUnit.Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            List<Rule> ruleInputs = new List<Rule>()
            {
                new Rule()
                {
                    Operator = System.Linq.Expressions.ExpressionType.AndAlso.ToString("g"),
                    RuleIdentifier="A",
                    Rules = new List<Rule>()
                    {
                        new Rule(){ MemberName = "ProductCode",Inputs = new List<object> { "A" }, Operator = "StartsWith"},
                        new Rule(){
                            Operator = "And",
                            Rules = new List<Rule>(){
                                new Rule(){ MemberName = "Price", TargetValue = "0", Operator = System.Linq.Expressions.ExpressionType.GreaterThan.ToString("g")},
                                new Rule(){ MemberName = "Price", TargetValue = "100",Operator= System.Linq.Expressions.ExpressionType.LessThan.ToString("g")}
                            }
                        }
                    }
                },
                new Rule()
                {
                    Operator = System.Linq.Expressions.ExpressionType.AndAlso.ToString("g"),
                    RuleIdentifier="B",
                    Rules = new List<Rule>()
                    {
                        new Rule(){ MemberName = "ProductCode",Inputs = new List<object> { "B" }, Operator = "StartsWith"},
                        new Rule(){
                            Operator = "And",
                            Rules = new List<Rule>(){
                                new Rule(){ MemberName = "Price", TargetValue = "100", Operator = System.Linq.Expressions.ExpressionType.GreaterThanOrEqual.ToString("g")},
                                new Rule(){ MemberName = "Price", TargetValue = "1000",Operator= System.Linq.Expressions.ExpressionType.LessThan.ToString("g")}
                            }
                        }
                    }
                },
                new Rule()
                {
                    Operator = System.Linq.Expressions.ExpressionType.AndAlso.ToString("g"),
                    RuleIdentifier="C",
                    Rules = new List<Rule>()
                    {
                        new Rule(){ MemberName = "ProductCode",Inputs = new List<object> { "C" }, Operator = "StartsWith"},
                        new Rule(){
                            Operator = "And",
                            Rules = new List<Rule>(){
                                new Rule(){ MemberName = "Price", TargetValue = "1000", Operator = System.Linq.Expressions.ExpressionType.GreaterThanOrEqual.ToString("g")}
                            }
                        }
                    }
                },
                new Rule()
                {
                    Operator = System.Linq.Expressions.ExpressionType.NotEqual.ToString("g"),
                    RuleIdentifier="Blank",
                    MemberName = "ProductCode",
                    TargetValue = string.Empty  
                } 
            };
            Product prod = new Product { ProductCode = "C001", Price = 999 };
            string[] ruleIentifier = { prod.ProductCode.Substring(0, 1), "Blank" };
            var validationRules = ruleInputs.Where(p => ruleIentifier.Contains(p.RuleIdentifier)).ToList();
            RuleEngine engine = new RuleEngine();
            var childPropCheck = engine.CompileRules<Product>(validationRules);
            bool passes = childPropCheck(prod);
            Assert.IsTrue(passes);
          //  Assert.Pass("Your first passing test");
        }
    }
}

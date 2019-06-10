using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ptncafe.GraphQLTest.Api.Infractstructure
{
    public static class StringExtensions
    {
        public static string FirstCharToLower(this string input)
        {
            switch (input)
            {
                case null: return null;
                case "": return"";
                default: return input.First().ToString().ToLower() + input.Substring(1);
            }
        }
    }
}

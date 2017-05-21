using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CrossCutting.Helper
{
    public class BussinessException : Exception
    {
        private string _code;
        public BussinessException(string code, string message) : base(message)
        {
            _code = code;
        }

        public String Code { get { return _code; } }
    }
}

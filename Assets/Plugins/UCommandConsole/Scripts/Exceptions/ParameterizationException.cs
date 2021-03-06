﻿using System;

namespace UCommandConsole.Exceptions
{
    public class ParameterizationException : InputFormatException
    {
        public ParameterizationException()
            : base() { }

        public ParameterizationException(string message)
            : base(message) { }

        public ParameterizationException(string message, Exception innertException)
            : base(message, innertException) { }
    }
}
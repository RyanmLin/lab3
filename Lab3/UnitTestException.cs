﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class UnitTestException : Exception
    {
        private string line_;
        private int idx_;
        private int results_;
        private int expected_;

        public UnitTestException(ref string line, int idx, int results, int expected, string message) :
          base(message)
        {
            line_ = line;
            idx_ = idx;
            results_ = results;
            expected_ = expected;
        }
    }
}

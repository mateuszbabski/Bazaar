﻿using Shared.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            //email regex validation
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidEmailException();
            }

            Value = value;
        }
        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string value) => new(value);
    }
}

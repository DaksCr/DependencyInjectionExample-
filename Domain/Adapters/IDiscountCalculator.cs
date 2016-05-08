﻿using Sapiens.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens.Domain.Adapters
{
    public interface IDiscountCalculator
    {
        IEnumerable<Discount> Calculate(Cart cart);
    }
}
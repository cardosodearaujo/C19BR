﻿using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Domain.Objects
{
    public class ListOfCases : Header
    {
        public ListOfCases() : base()
        {
            results = new List<Case>();
        }
        public IList<Case> results { get; set; }
    }
}

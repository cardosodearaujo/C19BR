﻿using System;
using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Domain.Objects.World
{
    public class Case
    {
        public Case()
        {
            coordinates = new Coordinates();
            today = new Today();
            latest_data = new LatestData();
            timeline = new List<Day>();
        }

        public Coordinates coordinates { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public long? population { get; set; }
        public DateTime updated_at { get; set; }
        public Today today { get; set; }
        public LatestData latest_data { get; set; }
        public IList<Day> timeline { get; set; }
    }
}

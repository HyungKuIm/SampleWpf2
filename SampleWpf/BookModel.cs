﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWpf
{
    public class BookModel : BindableBase
    {
        public string Title { get; set; }
        public string FileName { get; set; }
    }
}

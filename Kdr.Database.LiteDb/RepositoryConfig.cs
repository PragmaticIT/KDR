﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdr.Database.LiteDb
{
    public class RepositoryConfig : IRepositoryConfig
    {
        public string DbFilePath { get ; set; }
    }
}

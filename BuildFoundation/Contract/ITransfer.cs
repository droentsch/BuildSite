using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildFoundation.Contract
{
    public interface ITransfer
    {
        public void Copy();
        public List<String> Files { get; set; }
        public void Delete();
    }
}

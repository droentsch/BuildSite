using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildFoundation.Contract;

namespace BuildFoundation
{
    public class Ftp : ITransfer
    {
        #region ITransfer
        public void Copy()
        {
            throw new NotImplementedException();
        }

        public List<string> Files
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region private methods
        #endregion
        #region private properties
        #endregion
    }
}

﻿namespace Snippets3.Encryption
{
    using NServiceBus;

    public class Usage
    {

        public Usage()
        {
            #region EncryptionServiceSimple
            Configure.With()
                .RijndaelEncryptionService();
            #endregion
        }


    }
}

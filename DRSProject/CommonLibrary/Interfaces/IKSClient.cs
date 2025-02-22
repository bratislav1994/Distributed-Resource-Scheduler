///////////////////////////////////////////////////////////
//  IKSClient.cs
//  Implementation of the Interface IKSClient
//  Generated by Enterprise Architect
//  Created on:      27-dec-2016 11.40.26
//  Original author: Gogic
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ServiceModel;

namespace CommonLibrary.Interfaces
{
    [ServiceContract]
    public interface IKSClient
    {
        [OperationContract]
        void Update(UpdateInfo update, string username);

        [OperationContract]
        void DeleteService(string username);
    }
}